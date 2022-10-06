using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetItemData : MonoBehaviour
{
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text price;
    [SerializeField] TMP_Text description;
    [SerializeField] SliderAnimator startSlider;
    [SerializeField] ChilliController chilli;

    public void SetData(string _name, ItemData data)
    {
        ScreenConsole.Instance.Log($"Set data: {_name}, {data.Price}");

        itemName.text = _name;
        price.text = data.Price;
        FormatDescriptionText(data.Description);
        
        description.text = FormatDescriptionText(data.Description);
        startSlider.SetValue(data.Rating);
        chilli.SetHottnes(data.Hot);
    }

    private string FormatDescriptionText(string original)
    {
        //formats string adding tags to highight first capital letter
        //string ret = "<b><color=#F3D700>" + original[0] + "</b></color>";
        string ret = original.Insert(1, "</b></color>"); 
        ret = ret.Insert(0, "<b><color=#F3D700>");

        return ret;
    }

}
