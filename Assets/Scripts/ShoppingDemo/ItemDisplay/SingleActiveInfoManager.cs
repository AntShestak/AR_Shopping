using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActiveInfoManager : MonoBehaviour
{
    public static SingleActiveInfoManager Instance { get; private set; }

    private InfoDotController m_currentActiveInfo;

    private void Awake()
    {
        if (Instance != this)
            Instance = this;
    }

    public void SetSingleInfo(InfoDotController newInfo) 
    {
        //if (m_currentActiveInfo != null && m_currentActiveInfo.IsActive)
        //    m_currentActiveInfo.DeactivateInfo();

        m_currentActiveInfo = newInfo;

    }
}
