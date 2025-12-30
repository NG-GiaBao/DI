using System;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[Serializable]
public class AIContext
{
    [SerializeField] private SerializedDictionary<KeyData , float> keyValues = new();
    [SerializeField] private SerializedDictionary<ConditionData , float> conditionValues = new();
    
    public float GetKey(ContextData data)
    {
        return keyValues.TryGetValue(data.Key, out var value) ? value : 0f;
    }
    public void SetKey(ContextData data)
    {
        keyValues[data.Key] = Mathf.Clamp01(data.KeyValue);
    }
    public void AddKey(ContextData data )
    {
        keyValues[data.Key]= Mathf.Clamp01(data.KeyValue);
    }
    public float GetCondition(ContextData data)
    {
        return conditionValues.TryGetValue(data.Condition, out var value) ? value : 0f;
    }
    public void SetCondition(ContextData data)
    {
        conditionValues[data.Condition] = Mathf.Clamp01(data.ConditionValue);
    }
    public void SetCondition(ConditionData conditionData, float value)
    {
        if(conditionValues.ContainsKey(conditionData))
        {
            conditionValues[conditionData] = Mathf.Clamp01(value);
        }    
    }
    public void AddCondition(ContextData data)
    {
        conditionValues[data.Condition] = data.ConditionValue;
    }
}
