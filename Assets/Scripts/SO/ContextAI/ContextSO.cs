using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ContextSO", menuName = "Scriptable Objects/ContextSO")]
[Serializable]
public class ContextSO : ScriptableObject
{
    [SerializedDictionary("Key", "Value")]
    [SerializeField] private SerializedDictionary<KeyData, float> values = new();
    [SerializeField] private SerializedDictionary<ConditionData, float> conditionValues = new();

    public KeyData GetKey(KeyData keyData)
    {
        if (values.ContainsKey(keyData))
        {
            return keyData;
        }
        return KeyData.UnKnow;
    }
    public float GetKeyValue(KeyData keyData)
    {
        if (values.ContainsKey(keyData))
        {
            return values[keyData];
        }
        return 0f;
    }
    public ConditionData GetCondition(ConditionData conditionData)
    {
        if (conditionValues.ContainsKey(conditionData))
        {
            return conditionData;
        } 
            return ConditionData.UnKnow;
    }
    public float GetConditionValue(ConditionData conditionData)
    {
        if(conditionValues.ContainsKey(conditionData))
        {
            return conditionValues[conditionData];
        }    
        return 0f;
    }
}
public enum KeyData
{
    UnKnow,
    Hunger,
    Fear,
}
public enum ConditionData
{
    UnKnow,
    IsEating,
    IsRunning,
}
