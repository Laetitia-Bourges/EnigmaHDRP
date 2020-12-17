using System.Collections.Generic;
using UnityEngine;

public abstract class EN_Encodeur : MonoBehaviour, IEncode<int>
{
    protected Dictionary<char, char> correspondance = new Dictionary<char, char>();
    public abstract int Encode(int _index);
}
