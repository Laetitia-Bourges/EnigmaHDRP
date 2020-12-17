using System.Collections.Generic;

public class EN_Reflecteur : EN_Encodeur
{
    public void Start() => GerenateCorrespondance();
    void GerenateCorrespondance()
    {
        List<string> _data = EN_DataManager.Instance.Data.ReflecteurData;
        for (int i = 0; i < _data[0].Length; i++)
        {
            correspondance.Add(_data[0][i], _data[1][i]);
            correspondance.Add(_data[1][i], _data[0][i]);
        }
    }
    public override int Encode(int _index)
    {
        char _toEncode = EN_DataManager.Instance.Data.Alphabet[_index];
        char _codedLetter = correspondance[_toEncode];
        return EN_DataManager.Instance.Data.Alphabet.IndexOf(_codedLetter);
    }
}
