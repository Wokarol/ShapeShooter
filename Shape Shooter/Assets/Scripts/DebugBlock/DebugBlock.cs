using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugBlock
{
    public string OverrideName { get; set; } = "";
    public Dictionary<string, DataObject> Data { get; private set; } = new Dictionary<string, DataObject>();

    public void Clear() {
        Data.Clear();
    }

    #region Public functions - Defining
    public bool Define(string DataNameAndID) {
        return Define(DataNameAndID, DataNameAndID);
    }
    public bool Define(string dataName, string dataID) {
        if (!Data.ContainsKey(dataID)) {
            Data.Add(dataID, new DataObject(dataName, ""));
            return true;
        } else {
            return false;
        }
    }
    public bool Undefine(string dataID) {
        if (Data.ContainsKey(dataID)) {
            Data.Remove(dataID);
            return true;
        } else {
            return false;
        }
    }
    #endregion
    #region Public functions - Managing
    public bool Change(string dataID, string value) {
        if (Data.ContainsKey(dataID)) {
            var data = Data[dataID];
            data.Value = value;
            Data[dataID] = data;
            return true;
        } else {
            return false;
        }
    }
    #endregion

}

public struct DataObject
{
    public string Name;
    public string Value;

    public DataObject(string name, string value) {
        Name = name;
        Value = value;
    }
}