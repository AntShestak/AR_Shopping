using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderAnimator : MonoBehaviour
{
    private Slider m_slider;
    private IEnumerator m_currentCoroutine;
    private float m_targetValue = 0;

    // Start is called before the first frame update
    void Awake()
    {
        m_slider = GetComponent<Slider>();
    }

    void OnEnable()
    {
        RunAnimation();
    }

    void OnDisable() 
    {
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        
    }


    public void SetValue(float value)
    {
        m_targetValue = value;
    }

    public void RunAnimation()
    {
        if (m_targetValue == 0)
            return;

        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        m_currentCoroutine = AnimateCor();
        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator AnimateCor()
    {
        
        float value = m_targetValue;

        float speed = 5f;
        float interpolator = 0f;
        bool isAnimating = true;

        while (isAnimating)
        {
            interpolator += Time.deltaTime * speed;

            if (interpolator >= 1f)
            {
                interpolator = 1f;
                isAnimating = false;
            }

            m_slider.value = Mathf.Lerp(0,value, interpolator);

            yield return 0;
        }
    }
}
