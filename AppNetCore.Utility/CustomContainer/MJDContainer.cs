/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       MJDContainer
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Utility.CustomContainer
   * 文 件 名：       MJDContainer
   * 创建时间：       2020/11/17 14:04:30
   * 作    者：       LIUFANG
   * 说   明：
   * 类型                    命外规则                     说明
   * 命名空间  namespace     Pascal           以.分隔，其中每一个限定词均为Pascal命名方式 如ExcelQuicker.Work
   * 类 class                Pascal           每一个逻辑断点首字母大写 如public class MyHome
   * 接口 interface          IPascal          每一个逻辑断点首字母大写,总是以I前缀开始，后接Pascal命名 如public interface IBankAccount 
   * 方法 method             Pascal           每一个逻辑断点首字母大写如private void SetMember(string)
   * 枚举类型 enum           Pascal           每一个逻辑断点首字母大写
   * 委托 delegate        Pascal           每一个逻辑断点首字母大写局部变量方法的
   * 参数                    Camel            首字母小写，之后Pascal命名如string myName   
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using AppNetCore.Utility.CustomAop;

namespace AppNetCore.Utility.CustomContainer
{
    public class MJDContainer : IMJDContainer
    {
        private Dictionary<string, MJDContainerRegisterModel> MJDContainerDictionary = new Dictionary<string, MJDContainerRegisterModel>();
        private Dictionary<string, object[]> MJDContainerValueDictionary = new Dictionary<string, object[]>();
        private Dictionary<string, object> MJDContainerScopDictionary = new Dictionary<string, object>();
        private string GetKey(string fullName, string shortName = null) => $"{fullName}__{shortName}";

        public IMJDContainer CreateChildContainer()
        {
            return new MJDContainer(this.MJDContainerDictionary, this.MJDContainerValueDictionary, new Dictionary<string, object>());
        }
        public MJDContainer() { }
        private MJDContainer(Dictionary<string, MJDContainerRegisterModel> mjdContainerDictionary, Dictionary<string, object[]> mjdContainerValueDictionary, Dictionary<string, object> mjdContainerScopDictionary)
        {
            this.MJDContainerDictionary = mjdContainerDictionary;
            this.MJDContainerValueDictionary = mjdContainerValueDictionary;
            this.MJDContainerScopDictionary = mjdContainerScopDictionary;
        }
        public void Register<TFrom, TTo>(string shortName = null, object[] paraList = null, LifetimeType lifetimeType = LifetimeType.Transient) where TTo : TFrom
        {
            this.MJDContainerDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), new MJDContainerRegisterModel()
            {
                TargetType = typeof(TTo),
                Lifetime = lifetimeType
            });

            if (paraList != null && paraList.Length > 0)
                this.MJDContainerValueDictionary.Add(this.GetKey(typeof(TFrom).FullName, shortName), paraList);
        }

        public TFrom Resolve<TFrom>(string shortName = null)
        {
            return (TFrom)this.ResolveObject(typeof(TFrom), shortName);
        }

        /// <summary>
        /// 递归实现多层级构造
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <returns></returns>
        private object ResolveObject(Type abstractType, string shortName = null, LifetimeType lifetimeType = LifetimeType.Transient)
        {
            string key = this.GetKey(abstractType.FullName, shortName); //abstractType.FullName;
            var model = this.MJDContainerDictionary[key];
            #region 生命周期管理
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("nothing");
                    break;
                case LifetimeType.Singleton:
                    if (model.SingletonIntance == null)
                    {
                        break;
                    }
                    else
                    {
                        return model.SingletonIntance;
                    }
                case LifetimeType.Scope:
                    if (this.MJDContainerScopDictionary.ContainsKey(key))
                    {
                        return this.MJDContainerScopDictionary[key];
                    }
                    else
                        break;
                default:
                    break;
            }
            #endregion
            Type type = model.TargetType;
            #region 选择合适的参数
            ConstructorInfo ctor = null;
            //标记特性
            ctor = type.GetConstructors().FirstOrDefault(c => c.IsDefined(typeof(MJDContainerAttribute), true));
            if (ctor == null)
            {
                //参数个数最多
                ctor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();
            }
            //  var ctor = type.GetConstructors()[0]; 
            #endregion
            #region 准备构造函数参数

            List<object> paraList = new List<object>();

            object[] paraConstant = MJDContainerValueDictionary.ContainsKey(key) ? MJDContainerValueDictionary[key] : null;
            int iIndex = 0;
            foreach (var para in ctor.GetParameters())
            {
                if (para.IsDefined(typeof(MJDParameterConstantAttribute), true))
                {
                    paraList.Add(paraConstant[iIndex++]);
                }
                else
                {
                    Type paraType = para.ParameterType;
                    //string paraKey = paraType.FullName;
                    //Type paraTargetType = this.MJDContainerDictionary[paraKey];
                    //object paraInstance = Activator.CreateInstance(paraTargetType);
                    string paraShortName = GetShortName(para);
                    object paraInstance = this.ResolveObject(paraType, paraShortName);
                    paraList.Add(paraInstance);
                }

            }
            #endregion

            object oInstance = Activator.CreateInstance(type, paraList.ToArray());

            #region 属性注入
            foreach (var prop in type.GetProperties().Where(p => p.IsDefined(typeof(MJDPropertyinjectionAttribute), true)))
            {
                Type propType = prop.PropertyType;
                string paraShortName = GetShortName(prop);
                object propInstance = this.ResolveObject(propType, paraShortName);
                prop.SetValue(oInstance, propInstance);

            }
            #endregion

            #region 方法注入
            foreach (var method in type.GetMethods().Where(m => m.IsDefined(typeof(MJDMethodinjectionAttribute), true)))
            {
                List<object> paraInjectionList = new List<object>();
                foreach (var para in method.GetParameters())
                {
                    Type paraType = para.ParameterType;
                    string paraShortName = GetShortName(para);
                    object paraInstance = this.ResolveObject(paraType, paraShortName);
                    paraInjectionList.Add(paraInstance);
                }
                method.Invoke(oInstance, paraInjectionList.ToArray());
            }
            #endregion

            #region 生命周期管理
            switch (model.Lifetime)
            {
                case LifetimeType.Transient:
                    Console.WriteLine("nothing");
                    break;
                case LifetimeType.Singleton:
                    model.SingletonIntance = oInstance;
                    break;
                case LifetimeType.Scope:
                    this.MJDContainerScopDictionary[key] = oInstance;
                    break;
                default:
                    break;
            }
            #endregion
            return oInstance.AOP(abstractType) ;
          //  return oInstance;
        }


        private string GetShortName(ICustomAttributeProvider provider)
        {
            if (provider.IsDefined(typeof(MJDParameterShortNameAttribute), true))
            {
                var attribute = (MJDParameterShortNameAttribute)(provider.GetCustomAttributes(typeof(MJDParameterShortNameAttribute), true)[0]);
                return attribute.ShortName;
            }
            return null;
        }
        ///针对构造函数参数获取别名
        //private string GetShortName(ParameterInfo parameterInfo)
        //{
        //    if (parameterInfo.IsDefined(typeof(MJDParameterShortNameAttribute), true))
        //    {
        //        return parameterInfo.GetCustomAttribute<MJDParameterShortNameAttribute>().ShortName;
        //    }
        //    return null;
        //}
    }
}
