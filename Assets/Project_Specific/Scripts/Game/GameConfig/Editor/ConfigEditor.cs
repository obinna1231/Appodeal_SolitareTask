using UnityEditor;

public static class ConfigEditor
{
    [MenuItem("MrWayne/Open GameConfig #%t")]
    public static void OpenGameSettings()
    {
        Selection.activeObject = GameConfig.Instance;
    }
}