using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IObserver
    {
        void OnNotify(GameObject entity, EventMessage eventMsg, List<object> data = null);
    }
}