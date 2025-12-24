using System;
using System.Collections.Generic;
using UnityEngine;

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
    public void Subscribe<TEvent>(Action<TEvent> funtion)
    {
        Type eventType = typeof(TEvent);
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
                if (cachePulish[eventType] is TEvent eventData)
                {
                    funtion(eventData);
                }
            }
        }
    }

    public void Unsubscribe<TEvent>(Action<TEvent> listener)
    {
        Type eventType = typeof(TEvent);
        if (eventTable.ContainsKey(eventType))
        {
            eventTable[eventType].Remove(listener);
        }
    }
    /// <summary>
    /// Quản lý việc phát và xử lý sự kiện.
    /// </summary>
    /// <typeparam name="TPublisher">
    /// Kiểu đối tượng phát ra sự kiện ( class hiện tại )
    /// </typeparam>
    /// <typeparam name="TEvent">
    /// Kiểu dữ liệu của sự kiện được phát ( struct hoặc C# Poco)
    /// </typeparam>
    public void Publish<TPublisher, TEvent>() where TEvent : new()
    {
        Publish<TPublisher, TEvent>(new TEvent());
    }
    /// <summary>
    /// Quản lý việc phát và xử lý sự kiện.
    /// </summary>
    /// <typeparam name="TPublisher">
    /// Kiểu đối tượng phát ra sự kiện ( class hiện tại )
    /// </typeparam>
    /// <typeparam name="TEvent">
    /// Kiểu dữ liệu của sự kiện được phát ( struct hoặc C# Poco)
    /// </typeparam>
    /// <param name="eventData">
    /// Dữ liệu sự kiện ( sau khi được khởi tạo thì truyền vào làm tham số )
    /// </param>
    public void Publish<TPublisher, TEvent>(TEvent eventData)
    {
        Type eventType = typeof(TEvent);
        Type baseType = typeof(TPublisher);
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
                if (listener is Action<TEvent> action)
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
