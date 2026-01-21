using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Register
{
    private static readonly Dictionary<Type, Component> mapRef = new();
    private static readonly Dictionary<Type, Action<Component>> actionRef = new();
    private static readonly List<BaseInject> baseInjects = new();
    private static readonly Dictionary<string , BaseInject> mapInjects = new();

    public static void RegisterRef<T>(T component) where T : Component
    {
        var type = typeof(T);
        if (!mapRef.ContainsKey(type))
        {
            mapRef[type] = component;
        }
        else
        {
            Debug.LogWarning($"{mapRef[type].name} Đã có ref");
        }
        if (actionRef.TryGetValue(type, out Action<Component> action))
        {
            action?.Invoke(component);
            actionRef.Remove(type);
        }
    }

    public static void RegisterInject(BaseInject baseInject)
    {
        if (!baseInjects.Contains(baseInject))
        {
            baseInjects.Add(baseInject);
        }

        if(mapInjects.ContainsKey(baseInject.GetType().Name))
        {
            Debug.LogWarning($"{baseInject.GetType().Name} Đã có Inject");
        }
        else
        {
            mapInjects[baseInject.GetType().Name] = baseInject;
        }
    }
    public static List<BaseInject> GetAllInject()
    {
        return baseInjects;
    }
    public static Dictionary<string, BaseInject> GetMapInject()
    {
        return mapInjects;
    }
    public static void GetRef<T>(Action<T> action) where T : Component
    {
        var type = typeof(T);
        if (mapRef.TryGetValue(type, out var component))
        {
            action(component as T);
        }
        else
        {
            actionRef[type] = c => action(c as T);
        }
    }
}
