using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {

    static Dictionary<System.Type, System.Delegate> dict = new Dictionary<System.Type, System.Delegate>();

    public delegate void EventDelegate<T>(T a) where T : GameEvent;

    public static void AddListener<T>(EventDelegate<T> del) where T : GameEvent { //registerObserver
        if(dict.ContainsKey(typeof(T))) {
            dict[typeof(T)] = System.Delegate.Combine(dict[typeof(T)], del);
        } else {
            dict.Add(typeof(T), del);
        }
    }

    public static void RemoveListener<T>(EventDelegate<T> del) where T : GameEvent { //unregisterObserver
        dict[typeof(T)] = System.Delegate.Remove(dict[typeof(T)], del);
    }

    public static void Raise(GameEvent e) { //notify
        if(dict.ContainsKey(e.GetType())) {
            dict[e.GetType()].DynamicInvoke(e);
        }
    }
}
