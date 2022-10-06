using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(OffsetManager))]
public class LineAnimator : MonoBehaviour
{
   
    [SerializeField] private Transform startAnchor;
    [SerializeField] private PanelActivator panels;

    private LineRenderer m_rend;
    private OffsetManager m_offsetMan;

    IEnumerator m_currentCoroutine;
    private float m_speed = 4.75f;



    private void Awake()
    {
        m_rend = GetComponent<LineRenderer>();
        m_offsetMan = GetComponent<OffsetManager>();
    }

    private void Start()
    {
        

        //Activate(new Vector2(0, 0));
        //settings
        m_rend.numCapVertices = 2;

    }

    public void Activate()
    {
        ScreenConsole.Instance.Log($"Activating line");

        //setup from object position

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.parent.position);

        
        //setup offsets
        m_offsetMan.SetOffsetFromTouchPosition((Vector2)screenPosition);
        //ScreenConsole.Instance.Log($"Line offset JUST set to pivot: {m_offsetMan.InfoBoxPivot}");
        //run animation
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);
        m_currentCoroutine = ActivationCor();
        StartCoroutine(m_currentCoroutine);
    }

    public void Deactivate()
    {

        //play animation
        if (m_currentCoroutine != null)
            StopCoroutine(m_currentCoroutine);
        m_currentCoroutine = DeactivateCor();
        StartCoroutine(m_currentCoroutine);
    }


    IEnumerator DeactivateCor()
    {
        panels.DeactivatePanels();

        float executionTime = 0f;

        int posCount = m_rend.positionCount;

        while (posCount > 1)
        {
            Vector3 startPosition = m_rend.GetPosition(posCount - 2);
            Vector3 endPosition = m_rend.GetPosition(posCount - 1);

            int positionToAnimate = posCount - 1;
            float interpolator = 1f;
            bool isAnimating = true;
            while (isAnimating)
            {


                interpolator -= m_speed * Time.deltaTime;
                if (interpolator <= 0)
                {
                    isAnimating = false;
                    interpolator = 0;
                }

                m_rend.SetPosition(positionToAnimate, Vector3.Lerp(startPosition, endPosition, interpolator));

                yield return 0;

                executionTime += Time.deltaTime;
                if (executionTime >= 5.0f)
                    break;
            }

            posCount -= 1;
            m_rend.positionCount = posCount;
            //if (executionTime >= 5.0f)
            //    break;
        }

        panels.gameObject.SetActive(false);
    }

    IEnumerator ActivationCor()
    {
        //ScreenConsole.Instance.Log("Line activation running");
        Vector2 endOffset = m_offsetMan.LineEndOffset;
        Vector3 endPosition = startAnchor.localPosition + (Vector3)endOffset * 0.03f;
        Vector3 startPosition = startAnchor.localPosition + (Vector3)endOffset * 0.0025f;



        float interpolator = 0;

        m_rend.positionCount = 2;
        m_rend.SetPosition(0, startPosition);
        m_rend.SetPosition(1, startPosition);

        bool isAnimating = true;
        while (isAnimating)
        {
            interpolator += m_speed * Time.deltaTime;
            if (interpolator >= 1)
            {
                isAnimating = false;
                interpolator = 1;
            }

            m_rend.SetPosition(1, Vector3.Lerp(startPosition, endPosition, interpolator));

            yield return 0;
        }

        //now turn on panels
        //ScreenConsole.Instance.Log($"Line: Setting pivot to {m_offsetMan.InfoBoxPivot}");
        panels.gameObject.SetActive(true);
        panels.ActivatePanels( m_rend.GetPosition(1), m_offsetMan.InfoBoxPivot);


        //stage 2 -straight line
        m_rend.positionCount = 3;
        
        m_rend.SetPosition(2, endPosition);
        startPosition = endPosition;
        endPosition = startPosition + Vector3.right * endOffset.x * 0.06f * 2.5f; //scale factor is 2.5

        interpolator = 0;
        isAnimating = true;
        while (isAnimating)
        {
            interpolator += m_speed * Time.deltaTime;
            if (interpolator >= 1)
            {
                isAnimating = false;
                interpolator = 1;
            }

            m_rend.SetPosition(2, Vector3.Lerp(startPosition, endPosition, interpolator));

            yield return 0;
        }

        

    }
}
