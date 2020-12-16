using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EN_LightsManagerTemp))]
public class EN_LightsManagerEditor : EN_MyEditor<EN_LightsManagerTemp>
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //base.OnInspectorGUI();
        DrawLightProperty();
        DrawPositionsList();
        DrawAddButton();
        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        serializedObject.Update();
        DrawHandlePosition();
        DrawDebug();
        serializedObject.ApplyModifiedProperties();
    }

    void DrawLightProperty()
    {
        SerializedProperty _lightProp = serializedObject.FindProperty("lightFeedback");
        EditorGUILayout.PropertyField(_lightProp);
        Space(3);
    }

    void DrawAddButton()
    {
        SerializedProperty _cursorPosProp = serializedObject.FindProperty("currentCursorPosition");
        SerializedProperty _characterIDProp = serializedObject.FindProperty("characterID");

        _characterIDProp.stringValue = EditorGUILayout.TextField("Character id : ", _characterIDProp.stringValue);
        _cursorPosProp.vector3Value = EditorGUILayout.Vector3Field("Position : ", _cursorPosProp.vector3Value);
        if (GUILayout.Button("Add Light Position"))
            eTarget.Add(_characterIDProp.stringValue.ToUpper(), _cursorPosProp.vector3Value);
    }

    void DrawPositionsList()
    {
        SerializedProperty _idList = serializedObject.FindProperty("idList");
        SerializedProperty _positionList = serializedObject.FindProperty("positionList");

        if (_idList.arraySize == 0) return;
        for (int i = 0; i < _idList.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("ID : ", _idList.GetArrayElementAtIndex(i).stringValue);
            if (GUILayout.Button("X"))
                eTarget.Remove(i);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Vector3Field("Pos : ", _positionList.GetArrayElementAtIndex(i).vector3Value);
        }

        Space(3);
    }

    void DrawHandlePosition()
    {
        SerializedProperty _positionProp = serializedObject.FindProperty("currentCursorPosition");
        _positionProp.vector3Value = Handles.PositionHandle(_positionProp.vector3Value, Quaternion.identity);
    }
    void DrawDebug()
    {
        SerializedProperty _positionList = serializedObject.FindProperty("positionList");
        Handles.color = Color.yellow - new Color(0, 0, 0, .5f);
        for (int i = 0; i < _positionList.arraySize; i++)
            Handles.DrawSolidDisc(_positionList.GetArrayElementAtIndex(i).vector3Value, Vector3.up, .2f);
    }

    void Space(int _i = 1)
    {
        for (int i = 0; i < _i; i++)
            EditorGUILayout.Space();
    }
}
