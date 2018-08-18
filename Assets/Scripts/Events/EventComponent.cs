using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComponent : MonoBehaviour {

    private Dictionary<Type, List<Action<object>>> listeners = new Dictionary<Type, List<Action<object>>>();

    public void RegisterListener<T>(Action<object> callback) where T : InstanceEvent
    {
        Type type = typeof(T);

        if (!this.listeners.ContainsKey(type)) {
            this.listeners.Add(type, new List<Action<object>>());
        }

        this.listeners[type].Add(callback);
    }

    public void RemoveListener()
    {
        // Pretend this is implemented
    }

    public void CreateEvent<T>(object data) where T : InstanceEvent
    {
        Type type = typeof(T);

        if (!this.listeners.ContainsKey(type))
            return;
        
        List<Action<object>> callbacks = this.listeners[type];
        for (int i = 0; i < callbacks.Count; i++) {
            callbacks[i]?.Invoke(data);
        }
    }
}
