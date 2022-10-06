using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private Dictionary<string, ItemData> data = null;

    public ItemData GetDataFor(string name) 
    {
        ScreenConsole.Instance.Log($"Data requested for {name}");

        if (data == null)
            data = DataParser.GetData();

        if (data.ContainsKey(name))
        {
            ScreenConsole.Instance.Log($"Returning data {data[name]}");
            return data[name];
        }
        else
        {
            ScreenConsole.Instance.Log($"Data doesn't contain name: {name}");
            return null;
        }
            
    }
}
