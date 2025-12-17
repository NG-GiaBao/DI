using System;
using System.Collections.Generic;

public class EventBus
{
    private readonly Dictionary<Type, List<Delegate>> eventTable = new();
    public Dictionary<Type, List<Delegate>> EventTable => eventTable;

    private readonly Dictionary<Type, object> cachePulish = new();
#if UNITY_EDITOR
    private readonly Dictionary<Type, object> lastEventData = new();
    private readonly List<PublishRecord> publishHistory = new();

    public IReadOnlyDictionary<Type, object> DebugLastEventData => lastEventData;
    public IReadOnlyList<PublishRecord> DebugPublishHistory => publishHistory;
#endif
    public void Subscribe<T>(Action<T> funtion)
    {
        Type eventType = typeof(T);
        if (!eventTable.ContainsKey(eventType))
        {
            eventTable[eventType] = new List<Delegate>
            {
                funtion
            };
            return;
        }
        else
        {
            if (cachePulish.ContainsKey(eventType))
            {
                if (cachePulish[eventType] is T eventData)
                {
                    funtion(eventData);
                }
            }
        }
    }

    public void Unsubscribe<T>(Action<T> listener)
    {
        Type eventType = typeof(T);
        if (eventTable.ContainsKey(eventType))
        {
            eventTable[eventType].Remove(listener);
        }
    }
    public void Publish<Tbase, T>(T eventData) 
    {
        Type eventType = typeof(T);
        Type baseType = typeof(Tbase);
#if UNITY_EDITOR
        publishHistory.Add(new PublishRecord
        {
            publisherType = baseType,
            eventType = eventType,
            data = eventData
        });
        lastEventData[eventType] = eventData;
#endif
        if (eventTable.ContainsKey(eventType))
        {
            foreach (var listener in eventTable[eventType])
            {
                if (listener is Action<T> action)
                {
                    action(eventData);
                }
            }
        }
        else
        {
            eventTable[eventType] = new List<Delegate>();
            cachePulish[eventType] = eventData;
        }
    }
    public void Clear()
    {
        eventTable.Clear();
    }
}

#if UNITY_EDITOR
[Serializable]
public class PublishRecord
{
    public Type publisherType;
    public Type eventType;
    public object data;
}
#endif
