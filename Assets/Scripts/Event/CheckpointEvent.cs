using System.Collections;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class CheckpointEvent
{
    
    public static void TriggerEvent(Vector2 position)
    {
        EventManager.TriggerEvent(Constants.CHECKPOINT_EVENT_KEY, CreateLevelHashtable(position));
    }

    public static void Listen(UnityAction<Hashtable> listener)
    {
        EventManager.StartListening(Constants.CHECKPOINT_EVENT_KEY, listener);
    }

    public static Vector2 ReadCheckpoint(Hashtable h)
    {
        return EventManager.ReadKeyFromHashtable<Vector2>(h, Constants.CHECKPOINT_EVENT_KEY);
    }

    public static Hashtable CreateLevelHashtable(Vector2 position)
    {
        Hashtable h = new Hashtable();
        h.Add(Constants.CHECKPOINT_EVENT_KEY, position);
        return h;
    }
}
