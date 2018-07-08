using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using System;

public class HashtableEvent : UnityEvent<Hashtable> { }

public class EventManager : MonoBehaviour
{

    private static EventManager eventManager;
    private Dictionary<string, HashtableEvent> eventDictionary;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("Need an active EventManager on a GameObject in your scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, HashtableEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<Hashtable> listener)
    {
        HashtableEvent unityEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.AddListener(listener);
        }
        else
        {
            unityEvent = new HashtableEvent();
            unityEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, unityEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<Hashtable> listener)
    {
        if (eventManager == null) return;
        HashtableEvent unityEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, Hashtable hashtable)
    {
        HashtableEvent unityEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out unityEvent))
        {
            unityEvent.Invoke(hashtable);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        TriggerEvent(eventName, null);
    }

    public static T ReadKeyFromHashtable<T>(Hashtable h, string key)
    {
        if (h != null && h.ContainsKey(key))
        {
            return (T)h[key];
        }
        throw new ArgumentException("You have fed the wrong hashtable into this event");
    }
}
