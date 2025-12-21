using UnityEngine;
using System;
using System.Collections.Generic;

public class UIView : MonoBehaviour
{
    [SerializeField] private Transform mainCanvas;
    [SerializeField] private Dictionary<Type, UiElement> uiElements = new();
    [SerializeField] private List<UiElement> uiElementList = new();
    [SerializeField] private List<string> uiElementTypeList = new();
    private const string uiElementPath = "UI";

    public GameObject LoadView(Type type)
    {
        string name = type.Name;
        GameObject gameObject = Resources.Load<GameObject>($"{uiElementPath}/{name}");
        return gameObject;
    }

    public void CreatUiPrefabs<T>(object data = null) where T : UiElement
    {
        Type type = typeof(T);
        GameObject obj = Instantiate(LoadView(type), mainCanvas);
        if (obj.TryGetComponent(out UiElement uiElement))
        {
            if(!uiElements.ContainsKey(type))
            {
                uiElements[type] = uiElement;
                uiElementList.Add(uiElement);
                uiElementTypeList.Add(type.Name);
            }    
            uiElement.Show();
            if (data != null)
            {
                uiElement.OnStartData(data);
            }
        }
        else
        {
            Debug.LogWarning($"The prefab {obj.name} does not contain a UiElement component.");
        }


        //GameObject prefabs = Instantiate(uiElements[type], transform);
        //if (prefabs.TryGetComponent(out UiElement uiElement))
        //{
        //    uiElement.Show();
        //}
        //else
        //{
        //    Debug.LogWarning($"The prefab {prefabs.name} does not contain a UiElement component.");
        //}
        //if (data != null)
        //{
        //    uiElement.OnStartData(data);
        //}
    }
    public void HideUi<T>() where T : UiElement
    {
        Type type = typeof(T);
        if (uiElements.ContainsKey(type))
        {
            uiElements[type].Hide();
        }

    }
}
