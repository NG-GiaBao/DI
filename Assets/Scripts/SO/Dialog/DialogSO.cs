using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogSO", menuName = "Scriptable Objects/DialogSO")]
public class DialogSO : ScriptableObject
{
    public List<string> dialogLst = new();
    private Queue<string> dialogQueue;

    public void InitQueue()
    {
        dialogQueue = new Queue<string>(dialogLst);
    }
    public string GetNextDialog()
    {
        if (dialogQueue == null || dialogQueue.Count == 0)
        {
            return null;
        }
        return dialogQueue.Dequeue();
    }
}
