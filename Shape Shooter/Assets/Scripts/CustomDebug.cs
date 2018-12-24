using System.Diagnostics;

public static class Debug
{
    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Log(string message) {
        UnityEngine.Debug.Log(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void Log(string message, UnityEngine.GameObject context) {
        UnityEngine.Debug.Log(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(string message) {
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogWarning(string message, UnityEngine.GameObject context) {
        UnityEngine.Debug.LogWarning(message, context);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogError(string message) {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void LogError(string message, UnityEngine.GameObject context) {
        UnityEngine.Debug.LogError(message, context);
    }
}
