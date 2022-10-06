using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraAlways : MonoBehaviour
{
    private Transform m_cam;

    private Quaternion m_targetRotationQuat;
    private float m_interpolator;

    private void Awake()
    {
        m_cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        IEnumerator cor = UpdateQuaternionCor();
        StartCoroutine(cor);
        cor = UpdateRotationCor();
        StartCoroutine(cor);
    }

    IEnumerator UpdateRotationCor()
    {
        var wait = new WaitForSeconds(0.01f);
        float speed = 500f;


        
        while (true)
        {
            m_interpolator += Time.deltaTime * speed;
            
            transform.rotation = Quaternion.Lerp(transform.rotation, m_targetRotationQuat, Mathf.Clamp01(m_interpolator));

            yield return wait;
        }
    }

    IEnumerator UpdateQuaternionCor()
    {
        var wait = new WaitForSeconds(0.5f);
        
        Quaternion newRotation;


        while (true)
        {
            newRotation = GetCameraFacingQuaternion();

            if (!newRotation.Equals(m_targetRotationQuat))
            {
                m_interpolator = 0;
                m_targetRotationQuat = newRotation;
            }

            yield return wait;
        }
    }

    private Quaternion GetCameraFacingQuaternion()
    {
        return Quaternion.LookRotation(transform.position - m_cam.position, m_cam.up);
    }
}
