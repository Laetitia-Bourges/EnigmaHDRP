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

/*
public JsonData(string _string)
{
    Alphabet = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    RotorsData = new List<string>() { "EKMFLGDQVZNTOWYHXUSPAIBRCJ", "AJDKSIRUXBLHWTMCQGZNPYFVOE", "BDFHJLCPRTXVZNYEIWGAKMUSQO" };
    ReflecteurData = new List<string>() { "ABCDEFGIJKMTV", "YRUHQSLPXNOZW" };
}
*/
