using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEncode<T>
{
    T Encode(T _value);
}
