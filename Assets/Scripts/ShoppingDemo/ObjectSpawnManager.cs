using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] DataManager m_data;
    [SerializeField] GameObject m_anchor;
    [SerializeField] GameObject m_Prefab;

    

    

    public void Spawn(ARTrackedImage image) {

        GameObject anchor = Instantiate(m_anchor, image.transform);

       

        GameObject prefab = Instantiate(m_Prefab, this.gameObject.transform);
        ScreenConsole.Instance.Log("Objects spawned");

        anchor.GetComponent<AnchorScript>().SetAnchoredObject(prefab.GetComponent<AnchoredPrefab>());

        var data = m_data.GetDataFor(image.referenceImage.name);
        if (data != null)
        {
            ScreenConsole.Instance.Log("Data is NOT null");
            SetItemData setter = prefab.GetComponent<SetItemData>();
            setter.SetData(image.referenceImage.name, data);
        }
        else
        {
            ScreenConsole.Instance.Log("Data is null");
        }


    }


}
