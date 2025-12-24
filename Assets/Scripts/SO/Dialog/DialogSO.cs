using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    public List<string> dialogLst = new();
}
