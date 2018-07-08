using System.Collections;
using System;
using UnityEngine;

public class EventUtils : MonoBehaviour {

    public static T ReadKeyFromHashtable<T>(Hashtable h, string key)
    {
        if (h != null && h.ContainsKey(key))
        {
            return (T)h[key];
        }
        throw new ArgumentException("You have fed the wrong hashtable into this event");
    }
}
