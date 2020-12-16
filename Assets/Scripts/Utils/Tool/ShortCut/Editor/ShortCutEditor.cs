using UnityEditor;
using UnityEngine;

public class ShortCutEditor : Editor
{
    [MenuItem("Shortcuts/PersistentDataPath")]
    static void OpenPersistantDataPath() => Application.OpenURL(Application.persistentDataPath);

    [MenuItem("Shortcuts/DataPath")]
    static void OpenDataPath() => Application.OpenURL(Application.dataPath);
}
