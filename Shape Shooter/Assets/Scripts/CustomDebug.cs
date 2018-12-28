using System.Diagnostics;

#if !DEVELOPEMENT_BUILD && !UNITY_EDITOR
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

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color, float duration, bool depthTest) {
        UnityEngine.Debug.DrawLine(start, end, color, duration, depthTest);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color, float duration) {
        UnityEngine.Debug.DrawLine(start, end, color, duration);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color) {
        UnityEngine.Debug.DrawLine(start, end, color);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end) {
        UnityEngine.Debug.DrawLine(start, end);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color, float duration, bool depthTest) {
        UnityEngine.Debug.DrawRay(start, end, color, duration, depthTest);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color, float duration) {
        UnityEngine.Debug.DrawRay(start, end, color, duration);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 end, UnityEngine.Color color) {
        UnityEngine.Debug.DrawRay(start, end, color);
    }

    [Conditional("DEVELOPMENT_BUILD")]
    [Conditional("UNITY_EDITOR")]
    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 end) {
        UnityEngine.Debug.DrawRay(start, end);
    }
} 
#endif
