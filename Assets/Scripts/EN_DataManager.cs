using System.Collections.Generic;
using System;
using UnityEngine;

public class EN_DataManager : EN_Singleton<EN_DataManager>
{
    public JsonData Data = new JsonData();
    protected override void Awake()
    {
        base.Awake();
        GetJson();
    }
    void GetJson()
    {
        string _jsonData = Resources.Load<TextAsset>("JsonData").text;
        Data = JsonUtility.FromJson<JsonData>(_jsonData);
    }
}

[Serializable]
public struct JsonData
{
    public List<char> Alphabet;
    public List<string> RotorsData;
    public List<string> ReflecteurData;
}
