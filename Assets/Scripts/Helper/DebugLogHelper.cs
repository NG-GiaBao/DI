using UnityEngine;

public static class DebugLogHelper
{
    public static string ToColor(this string message, Color color)
    {
        return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
    }
}

