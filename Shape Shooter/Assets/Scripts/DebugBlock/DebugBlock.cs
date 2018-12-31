using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct DebugBlock
{
    [SerializeField] List<Data> data;

    public DebugBlock(List<Data> _data) : this() {
        data = _data;
    }

    public void Clear() {
        data.Clear();
    }

    public void Add(string name, string value) {
        data.Add(new global::Data(name, value));
    }
}

[System.Serializable]
public struct Data
{
    public string Name;
    public string Value;

    public Data(string name, string value) {
        Name = name;
        Value = value;
    }
}