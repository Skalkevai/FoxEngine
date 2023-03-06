using UnityEngine;

namespace FoxEngine
{
    public static class Debug
    {
        public static void DebugError(object _error)
        {
            UnityEngine.Debug.Log($"[<color=#FF0000FF>ERROR</color>] {_error}");
        }
        
        public static void DebugCancel(object _error)
        {
            UnityEngine.Debug.Log($"[<color=#F28C28FF>CANCEL</color>] {_error}");
        }
        
        public static void DebugWarning(object _warning)
        {
            UnityEngine.Debug.Log($"[<color=#FFFF00FF>WARNING</color>] {_warning}");
        }
        
        public static void DebugLog(object _log)
        {
            UnityEngine.Debug.Log($"[<color=#00FF11FF>LOG</color>] {_log}");
        }
        
        public static void DebugNotImportantLog(object _log)
        {
            UnityEngine.Debug.Log($"[<color=#808080FF>LOG</color>] {_log}");
        }

        public static void DebugColor(object _log,string _hexColor)
        {
            UnityEngine.Debug.Log($"[<color={_hexColor}>LOG</color>] {_log}");
        }

        public static void DebugColor(object _log, Color _color)
        {
            UnityEngine.Debug.Log($"[<color={_color.GetHexCode()}>LOG</color>] {_log}");
        }
    }
}