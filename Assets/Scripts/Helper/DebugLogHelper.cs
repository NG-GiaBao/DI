using UnityEngine;

public static class DebugLogHelper
{
    public static string ToColor(this string message,object nameObj, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{nameObj.GetType().Name}.{message}</color>";
    }
}

