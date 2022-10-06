using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldInfoDotController : MonoBehaviour
{
    [SerializeField] private LineAnimator line;
    [SerializeField] private Color touchColor;

    public bool IsActive { get; set; } = false;

    private Transform m_anchor;
    private Material m_mat;
    private Color m_normalColor;
    private IEnumerator m_colorCor;
    private IEnumerator m_scaleCor;
    private Vector3 m_initialScale;
    private Vector3 m_maxScale;
    private float m_amplitude = 1.2f;

    private void Awake()
    {
        m_mat = GetComponent<MeshRenderer>().material;
    }

    private void Start()
    {
        m_initialScale = transform.localScale;
        m_maxScale = m_initialScale * m_amplitude;
        m_normalColor = m_mat.color;

        m_scaleCor = ScalingCor();
        StartCoroutine(m_scaleCor);
    }





    public void Touched(Vector2 touchPos)
    {
        //adjust the layout

        if (IsActive)
        {
            DeactivateInfo();
        }
        else
        {
            ActivateInfo(touchPos);
        }

        if (m_colorCor != null)
            StopCoroutine(m_colorCor);
        m_colorCor = TouchCor();
        StartCoroutine(m_colorCor);
    }

    public void ActivateInfo(Vector2 touchPos)
    {
       //line.Activate(touchPos);

        StopCoroutine(m_scaleCor);
        transform.localScale = m_initialScale;

        IsActive = true;
        //SingleActiveInfoManager.Instance.SetSingleInfo(this);
    }

    public void DeactivateInfo()
    {
        //information object is active
        line.Deactivate();
        StartCoroutine(m_scaleCor);


        IsActive = false; //info is disabled
        //SingleActiveInfoManager.Instance.SetSingleInfo(null);
    }

    IEnumerator ScalingCor()
    {

        Debug.Log("Coroutine started");
        var wait = new WaitForSeconds(0.01f);
        float speed = 5f;
        float angle = 0;
        float i = 0; //interpolator

        while (true)
        {
            angle += Time.fixedDeltaTime * speed;
            i = (Mathf.Sin(angle * Mathf.Deg2Rad) + 1) / 2;
            Debug.Log(i);
            transform.localScale = Vector3.Lerp(m_initialScale, m_maxScale, i);

            yield return wait;
        }
    }

    IEnumerator TouchCor()
    {
        m_mat.color = m_normalColor;
        Color startColor = m_normalColor;
        Color endColor = touchColor;

        float speed = 5f;
        float angle = 0f;
        bool isAnimating = true;

        while (isAnimating)
        {
            angle += Time.deltaTime * speed;


            if (angle >= 180)
            {
                angle = 180.0f;
                isAnimating = false;
            }

            m_mat.color = Color.Lerp(startColor, endColor, Mathf.Sin(angle * Mathf.Deg2Rad));

            yield return 0;

        }

    }
}
