using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScaler : MonoBehaviour
{

    private RectTransform m_rect;

    private IEnumerator m_currentCoroutine;
    private float m_speed = 5f;

    private void Awake()
    {
        m_rect = GetComponent<RectTransform>();
    }

    private void Start()
    {
        m_rect.localScale = Vector3.zero;
    }
    public void ScaleUp()
    {
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        m_currentCoroutine = ScaleUpCor();
        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator ScaleUpCor()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        float interpolator = 0f;
        bool isAnimating = true;

        while (isAnimating)
        {
            interpolator += Time.deltaTime * m_speed;

            if (interpolator >= 1f)
            {
                interpolator = 1f;
                isAnimating = false;
            }

            m_rect.localScale = Vector3.Lerp(startScale, endScale, interpolator);

            yield return 0;
        }
    }

    public void ScaleDown()
    {
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        m_currentCoroutine = ScaleDownCor();
        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator ScaleDownCor()
    {
        Vector3 startScale = Vector3.one;
        Vector3 endScale = Vector3.zero;

        float interpolator = 0f;
        bool isAnimating = true;

        while (isAnimating)
        {
            interpolator += Time.deltaTime * m_speed;

            if (interpolator >= 1f)
            {
                interpolator = 1f;
                isAnimating = false;
            }

            m_rect.localScale = Vector3.Lerp(startScale, endScale, interpolator);

            yield return 0;
        }
    }
}
