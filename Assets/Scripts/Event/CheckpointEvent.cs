using System.Collections;
using UnityEngine.Events;

public class CheckpointEvent
{

    public static void TriggerEvent(int level)
    {
        EventManager.TriggerEvent(Constants.CHECKPOINT_EVENT_KEY, CreateLevelHashtable(level));
    }

    public static void Listen(UnityAction<Hashtable> listener)
    {
        EventManager.StartListening(Constants.CHECKPOINT_EVENT_KEY, listener);
    }

    public static int ReadCheckpoint(Hashtable h)
    {
        return EventManager.ReadKeyFromHashtable<int>(h, Constants.CHECKPOINT_EVENT_KEY);
    }

    private static Hashtable CreateLevelHashtable(int level)
    {
        Hashtable h = new Hashtable();
        h.Add(Constants.CHECKPOINT_EVENT_KEY, level);
        return h;
    }
}
