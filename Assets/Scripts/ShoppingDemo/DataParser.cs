using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class ItemData
{
    public string Price { get; set; }
    public string Description { get; set; }
    public float Rating { get; set; }
    public int Hot { get; set; }
}

public static class DataParser
{

    public static Dictionary<string, ItemData> GetData()
    {
        TextAsset json = Resources.Load("ramyun_data") as TextAsset;
        ScreenConsole.Instance.Log($"Raw data loaded {json.text}");

        var data = JsonConvert.DeserializeObject<Dictionary<string,ItemData>>(json.text);
        ScreenConsole.Instance.Log($"Deserialized data is {data["Shin Ramen"].Price}");


        return data;
    }
}
