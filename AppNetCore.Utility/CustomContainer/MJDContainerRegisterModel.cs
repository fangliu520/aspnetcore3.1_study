﻿/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       MJDContainerRegisterModel
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Utility.CustomContainer
   * 文 件 名：       MJDContainerRegisterModel
   * 创建时间：       2020/11/18 11:52:14
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
using System.Text;

namespace AppNetCore.Utility.CustomContainer
{
    public class MJDContainerRegisterModel
    {
        public Type TargetType { get; set; }
        public LifetimeType Lifetime { get; set; }

        public object SingletonIntance { get; set; }

    }
    public enum LifetimeType
    {
        Transient,
        Singleton,
        Scope
    }
}