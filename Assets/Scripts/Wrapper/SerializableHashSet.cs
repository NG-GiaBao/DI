using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableHashSet
{
    //Unity chỉ serialize = [SerializeField] 
    [SerializeField] private List<string> tags = new();
    //Danh sách dùng để duyệt
    private HashSet<string> runtimeSet;
    public HashSet<string> RuntimeSet
    {
        get
        {
            runtimeSet ??= new();
            return runtimeSet;
        }
    }
    public List<string> Tags => tags;
    public bool  Contains(string tag)
    {
        return RuntimeSet.Contains(tag);
    }
    public void Syns()
    {
        runtimeSet = new (tags);
    }
}
