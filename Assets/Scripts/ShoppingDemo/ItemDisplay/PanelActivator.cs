using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelActivator : MonoBehaviour
{
    [SerializeField] RectTransform canvas;

    private PanelScaler[] m_panels;
    // Start is called before the first frame update
    void Awake()
    {
        m_panels = GetComponentsInChildren<PanelScaler>();
    }

    public void ActivatePanels(Vector3 pos, Vector2 canvasPivot)
    {
        ScreenConsole.Instance.Log($"Activating panels with pivot {canvasPivot}");
        transform.localPosition = pos;
        canvas.pivot = canvasPivot;

        foreach (var panel in m_panels)
        {
            panel.ScaleUp();
        }
    }

    public void DeactivatePanels()
    {
        foreach (var panel in m_panels)
        {
            panel.ScaleDown();
        }
    }
}
