using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenConsole : MonoBehaviour
{
    public static ScreenConsole Instance { get; private set; }

    private Text m_text;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;

        
    }

    private void Start()
    {
        m_text = GetComponentInChildren<Text>();
    }

    public void Log(string txt) {

        ////m_text.text = txt  + "\n\n" + m_text.text;
    }

    
}
