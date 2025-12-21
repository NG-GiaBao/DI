using System;
using System.Collections.Generic;
using UnityEngine;

public static class Register
{
    private static readonly Dictionary<Type, Component> mapRef = new();
    private static readonly Dictionary<Type, Action<Component>> actionRef = new();

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
    public static void GetRef<T>(Action<T> action) where T : Component
    {
        var type = typeof(T);
        if (mapRef.TryGetValue(type, out var component))
        {
            action((T)component);
        }
        else
        {
            actionRef[type] = c => action((T)c);
        }
    }
}
