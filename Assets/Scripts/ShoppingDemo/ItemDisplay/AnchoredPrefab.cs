using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchoredPrefab : MonoBehaviour
{
    [SerializeField] GameObject dot;
    
    private LineAnimator m_line;

    private IEnumerator m_currentCoroutine;

    private void Awake()
    {
        m_line = GetComponentInChildren<LineAnimator>(true);
    }

    #region Children Controls

    public void Activate()
    {

        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        m_currentCoroutine = ActivationCor();
        StartCoroutine(m_currentCoroutine);
    }

    public void Deactivate()
    {
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);

        m_currentCoroutine = DeactivationCor();
        StartCoroutine(m_currentCoroutine);
    }

    IEnumerator ActivationCor()
    {
        dot.SetActive(true);
        IEnumerator scaler = DotScaleUpCor();
        yield return StartCoroutine(scaler);

        m_line.gameObject.SetActive(true);
        m_line.Activate();
    }

    IEnumerator DeactivationCor()
    {
        m_line.Deactivate();
        yield return new WaitForSeconds(0.35f);
        m_line.gameObject.SetActive(false);

        
        IEnumerator scaler = DotScaleDownCor();
        yield return StartCoroutine(scaler);
        dot.SetActive(false);


    }



    IEnumerator DotScaleUpCor()
    {
        //ScreenConsole.Instance.Log("Scaler running");
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = Vector3.one * 0.01f;
        dot.transform.localScale = initialScale;



        float speed = 725f;
        float angle = 0;
        bool isAnimating = true;
        float i;

        while (isAnimating)
        {


            angle += Time.fixedDeltaTime * speed;
            if (angle >= 135)
            {
                angle = 135;
                isAnimating = false;
            }
            i = Mathf.Sin(angle * Mathf.Deg2Rad);
            dot.transform.localScale = Vector3.Lerp(initialScale, targetScale, i);
            //ScreenConsole.Instance.Log($"Scaling angle {angle} interpolator {i}");
            yield return 0;
        }

        //ScreenConsole.Instance.Log($"Scaler finished");
    }

    IEnumerator DotScaleDownCor()
    {
        //ScreenConsole.Instance.Log("Scaler running");
        Vector3 initialScale = Vector3.zero;
        Vector3 targetScale = dot.transform.localScale;
        //dot.transform.localScale = initialScale;



        float speed = 725f;
        float angle = 135;
        bool isAnimating = true;
        float i;

        while (isAnimating)
        {


            angle -= Time.fixedDeltaTime * speed;
            if (angle <= 0)
            {
                angle = 0;
                isAnimating = false;
            }
            i = Mathf.Sin(angle * Mathf.Deg2Rad);
            dot.transform.localScale = Vector3.Lerp(initialScale, targetScale, i);
            //ScreenConsole.Instance.Log($"Scaling angle {angle} interpolator {i}");
            yield return 0;
        }

        //ScreenConsole.Instance.Log($"Scaler finished");
    }

    #endregion

    #region Anchoring
    public void AttachToAnchor(Transform anchor)
    {
        IEnumerator follow = FollowAnchorCor(anchor);
        StartCoroutine(follow);
    }

    IEnumerator FollowAnchorCor(Transform anchor)
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);

        while (true)
        {
            //move parent object
            transform.position = anchor.position;

            yield return wait;
        }
    }

    #endregion
}
