using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChilliController : MonoBehaviour
{
 
    [SerializeField] Sprite sprPlaceholder;
    [SerializeField] Sprite sprChilli;

    [SerializeField] GameObject[] objects;

    private const int MAX_CHILLI = 3;

    private IEnumerator m_currentCoroutine;
    
    private int m_hotness = 0;


    void Start() {
        
        
    }

    void OnDisable()
    {
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);
    }

    void OnEnable()
    {
        ScreenConsole.Instance.Log($"Setting chilli placeholders length {objects.Length}");

        
        foreach (var obj in objects)
        {
            
            Image img = obj.GetComponent<Image>();
            img.sprite = sprPlaceholder;
            Color col = Color.black;
            col.a = 0.36f;
            img.color = col;

        }


        m_currentCoroutine = ChilliActivator();
        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator ChilliActivator()
    {
        ScreenConsole.Instance.Log($"Activating chillis {m_hotness}");

        for (int i = 0; i < m_hotness; i++)
        {
            Vector3 targetScale = objects[i].transform.localScale * 1.5f;
            objects[i].transform.localScale = Vector3.zero;
            
            Image img = objects[i].GetComponent<Image>();
            img.sprite = sprChilli;
            Color col = Color.white;
            col.a = 1f;
            img.color = col;



            float speed = 250f;
            float angle = 0f;
            bool isAnimating = true;

            while (isAnimating)
            {
                angle += Time.deltaTime * speed;

                if (angle >= 135f)
                {
                    angle = 135f;
                    isAnimating = false;
                }

                objects[i].transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, Mathf.Sin(angle * Mathf.Deg2Rad));
                
                yield return 0;
            }

            //ScreenConsole.Instance.Log($"Chilli is set. Alpha {objects[i].GetComponent<Image>().color.a}");
        }
    }

    public void SetHottnes(int value)
    {
        m_hotness = Mathf.Clamp(value, 0, MAX_CHILLI);

        
    }
}
