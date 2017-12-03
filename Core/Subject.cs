using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Subject
    {
        private List<IObserver> observers;

        public Subject()
        {
            observers = new List<IObserver>();
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public string GetObserversInfoRegistered(MonoBehaviour thisObject)
        {
            string obsMsg = "IObservers in "+thisObject.ToString()+": "+observers.Count+" observers\n";
            foreach(IObserver observer in observers)
            {
                obsMsg += observer.ToString()+", ";
            }
            return obsMsg;
        }

        public void Notify(GameObject entity, EventMessage eventMsg, List<object> data = null)
        {
            for(int i=0; i<observers.Count; i++)
            {
                observers[i].OnNotify(entity, eventMsg, data);
            }
        }
    }
}
