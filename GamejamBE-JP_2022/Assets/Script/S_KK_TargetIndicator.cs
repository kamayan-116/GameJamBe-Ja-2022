using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_KK_TargetIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Image targetImage;
    [SerializeField] private Image arrow;
    private Camera mainCamera;
    private RectTransform rectTransform;

    void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        float canvasScale = transform.root.localScale.z;
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        var pos = mainCamera.WorldToScreenPoint(target.position) - center;
        if (pos.z < 0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;
            if(Mathf.Approximately(pos.y, 0f))
                pos.y = -center.y;
        }

        var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;
        float d = Mathf.Max(Mathf.Abs(pos.x / (center.x - halfSize.x)), Mathf.Abs(pos.y / (center.y - halfSize.y)));

        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }
        rectTransform.anchoredPosition = pos / canvasScale;

        targetImage.enabled = isOffscreen;
        arrow.enabled = isOffscreen;
        if (isOffscreen)
            arrow.rectTransform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
    }
}
