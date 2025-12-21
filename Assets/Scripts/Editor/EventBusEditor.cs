using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

[CustomEditor(typeof(EventBusDebugger))]
public class EventBusEditor : Editor
{
    private string search = "";

    private readonly Dictionary<Type, bool> eventFoldouts = new();
    private readonly Dictionary<Type, bool> listenerFoldouts = new();
    private readonly Dictionary<Type, bool> dataFoldouts = new();
    private readonly Dictionary<string, bool> objectFoldouts = new();

    public override void OnInspectorGUI()
    {
        EventBusDebugger dbg = (EventBusDebugger)target;
        EventBus bus = dbg.Bus;

        if (bus == null)
        {
            EditorGUILayout.HelpBox(
                "EventBus chưa được bind. Chỉ khả dụng khi Play Mode.",
                MessageType.Info
            );
            return;
        }

        if (Application.isPlaying)
            Repaint();

        EditorGUILayout.LabelField("EventBus Debugger", EditorStyles.boldLabel);
        search = EditorGUILayout.TextField("Search Event", search);

        EditorGUILayout.Space();
        DrawPublishHistory(bus);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Registered Events", EditorStyles.boldLabel);

        foreach (var pair in bus.EventTable)
        {
            Type eventType = pair.Key;
            var listeners = pair.Value;

            if (!string.IsNullOrEmpty(search) &&
                !eventType.Name.ToLower().Contains(search.ToLower()))
                continue;

            eventFoldouts[eventType] = EditorGUILayout.Foldout(
                GetFoldout(eventFoldouts, eventType),
                $"{eventType.Name} ({listeners.Count})",
                true
            );

            if (!eventFoldouts[eventType])
                continue;

            EditorGUI.indentLevel++;
            DrawListenersSection(eventType, listeners);
            DrawEventDataSection(bus, eventType);
            EditorGUI.indentLevel--;
        }
    }

    // ================= Publish History =================

    private void DrawPublishHistory(EventBus bus)
    {

        if (bus == null || bus.DebugPublishHistory == null)
        {
            EditorGUILayout.LabelField("(EventBus not ready)");
            return;
        }

        EditorGUILayout.LabelField("Publish Events", EditorStyles.boldLabel);


        if (bus.DebugPublishHistory.Count == 0)
        {
            EditorGUILayout.LabelField("(No publish yet)");
            return;
        }

        foreach (var record in bus.DebugPublishHistory)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField(
                $"{record.publisherType.Name} → {record.eventType.Name}",
                EditorStyles.boldLabel
            );

            DrawObjectLikeInspector(record.data, record.eventType.Name);
            EditorGUILayout.EndVertical();
        }
    }

    // ================= Sections =================

    private void DrawListenersSection(Type eventType, List<Delegate> listeners)
    {
        listenerFoldouts[eventType] = EditorGUILayout.Foldout(
            GetFoldout(listenerFoldouts, eventType),
            "Listeners",
            true
        );

        if (!listenerFoldouts[eventType])
            return;

        EditorGUI.indentLevel++;
        foreach (var del in listeners)
        {
            if (del == null) continue;
            EditorGUILayout.LabelField(
                $"{del.Target?.GetType().Name ?? "Static"}.{del.Method.Name}"
            );
        }
        EditorGUI.indentLevel--;
    }

    private void DrawEventDataSection(EventBus bus, Type eventType)
    {
        dataFoldouts[eventType] = EditorGUILayout.Foldout(
            GetFoldout(dataFoldouts, eventType),
            "Last Event Data",
            true
        );

        if (!dataFoldouts[eventType])
            return;

        EditorGUI.indentLevel++;

        if (bus.DebugLastEventData.TryGetValue(eventType, out var data))
            DrawObjectLikeInspector(data, eventType.Name);
        else
            EditorGUILayout.LabelField("No data published yet");

        EditorGUI.indentLevel--;
    }

    // ================= Inspector-like Drawer =================

    private void DrawObjectLikeInspector(object obj, string path)
    {
        if (obj == null) return;

        string key = path + obj.GetType().FullName;
        objectFoldouts[key] = EditorGUILayout.Foldout(
            GetFoldout(objectFoldouts, key),
            obj.GetType().Name,
            true
        );

        if (!objectFoldouts[key])
            return;

        EditorGUI.indentLevel++;

        foreach (var field in obj.GetType().GetFields(
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
        {
            if (field.IsPrivate &&
                field.GetCustomAttribute<SerializeField>() == null)
                continue;

            DrawField(field, obj, key);
        }

        EditorGUI.indentLevel--;
    }

    private void DrawField(FieldInfo field, object target, string path)
    {
        object value = field.GetValue(target);
        Type t = field.FieldType;

        if (t == typeof(int))
            EditorGUILayout.IntField(field.Name, (int)value);
        else if (t == typeof(float))
            EditorGUILayout.FloatField(field.Name, (float)value);
        else if (t == typeof(string))
            EditorGUILayout.TextField(field.Name, value as string);
        else if (t == typeof(Vector2))
            EditorGUILayout.Vector2Field(field.Name, (Vector2)value);
        else if (t == typeof(Vector3))
            EditorGUILayout.Vector3Field(field.Name, (Vector3)value);
        else if (t.IsEnum)
            EditorGUILayout.EnumPopup(field.Name, (Enum)value);
        else if (IsPlainStruct(t))
            DrawObjectLikeInspector(value, path + field.Name);
        else
            EditorGUILayout.LabelField(field.Name, value?.ToString() ?? "null");
    }

    private bool IsPlainStruct(Type t)
    {
        return t.IsValueType && !t.IsPrimitive && !t.IsEnum;
    }

    private bool GetFoldout<T>(Dictionary<T, bool> dict, T key)
    {
        if (!dict.ContainsKey(key))
            dict[key] = true;
        return dict[key];
    }
}
