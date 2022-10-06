using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorScript : MonoBehaviour
{
    private AnchoredPrefab m_anchoredObject;

    private void OnEnable()
    {
        if (m_anchoredObject)
            m_anchoredObject.Activate();
    }

    private void OnDisable()
    {
        if (m_anchoredObject)
            m_anchoredObject.Deactivate();


    }

    public void SetAnchoredObject(AnchoredPrefab go)
    {
        m_anchoredObject = go;
        m_anchoredObject.AttachToAnchor(this.transform);
        ScreenConsole.Instance.Log("Anchor script: Object assigned");
        m_anchoredObject.Activate();
    }
}
