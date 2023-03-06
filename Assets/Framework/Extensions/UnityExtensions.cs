using System.Reflection;

public static class UnityExtensions
{
    public static void ClearLogConsole()
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.SceneView));             
        System.Type logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");             
        MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");             
        clearConsoleMethod.Invoke(new object(), null);
#endif
    }
}
