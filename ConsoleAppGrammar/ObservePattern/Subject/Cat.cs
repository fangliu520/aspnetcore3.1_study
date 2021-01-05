using ConsoleAppGrammar.ObservePattern.Observe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ObservePattern.Subject
{
    public class Cat
    {
        public void Miao()
        {
            new Baby().Cry();
            new Dog().Wang();
            new Mouse().Run();
        }

        private List<IObserve> observes = new List<IObserve>();
        public void AddObserve(IObserve observe)
        {
            observes.Add(observe);
        }
        public void RemoveObserve(IObserve observe)
        {
            observes.Remove(observe);
        }
        public void MiaoObserve()
        {
            foreach (var item in observes)
            {
                item.Action();
            }
        }

        public event Action catMiaoEvent;
        public void MiaoEvent()
        {
            if (catMiaoEvent != null)
            {
                catMiaoEvent.Invoke();
            }
        }
    }
}
