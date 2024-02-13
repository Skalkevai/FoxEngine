using UnityEngine;

namespace Frost
{
    public static class Debug
    {
        public static void LogError(object _error)
        {
            UnityEngine.Debug.Log($"[<color=#FF0000FF>ERROR</color>] {_error}");
        }
        
        public static void LogCancel(object _error)
        {
            UnityEngine.Debug.Log($"[<color=#F28C28FF>CANCEL</color>] {_error}");
        }
        
        public static void LogWarning(object _warning)
        {
            UnityEngine.Debug.Log($"[<color=#FFFF00FF>WARNING</color>] {_warning}");
        }
        
        public static void Log(object _log)
        {
            UnityEngine.Debug.Log($"[<color=#00FF11FF>LOG</color>] {_log}");
        }
        
        public static void LogNotImportant(object _log)
        {
            UnityEngine.Debug.Log($"[<color=#808080FF>LOG</color>] {_log}");
        }

        public static void LogColor(object _log,string _hexColor)
        {
            UnityEngine.Debug.Log($"[<color={_hexColor}>LOG</color>] {_log}");
        }

        public static void LogColor(object _log, Color _color)
        {
            UnityEngine.Debug.Log($"[<color={"#" + ColorUtility.ToHtmlStringRGBA(_color)}>LOG</color>] {_log}");
        }
    }
}