using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 風呂と井戸の追跡に関するプログラム
// 鎌倉の担当行：全て
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
        // ルート（Canvas）のスケール値を取得する
        float canvasScale = transform.root.localScale.z;
        // 画面中心
        var center = 0.5f * new Vector3(Screen.width, Screen.height);

        // （画面中心を原点(0,0)とした）ターゲットのスクリーン座標を求める
        var pos = mainCamera.WorldToScreenPoint(target.position) - center;

        // カメラ後方にあるターゲットのスクリーン座標は、画面中心に対する点対称の座標にする
        if (pos.z < 0f)
        {
            pos.x = -pos.x;
            pos.y = -pos.y;
            // カメラと水平なターゲットのスクリーン座標を補正する
            if(Mathf.Approximately(pos.y, 0f))
                pos.y = -center.y;
        }

         // UI座標系の値をスクリーン座標系の値に変換する
        var halfSize = 0.5f * canvasScale * rectTransform.sizeDelta;

        // 画面端の表示位置の調整（画面端のUIが見切れないよう）
        float d = Mathf.Max(Mathf.Abs(pos.x / (center.x - halfSize.x)), Mathf.Abs(pos.y / (center.y - halfSize.y)));

        // ターゲットのスクリーン座標が画面外なら、画面端になるよう調整する
        bool isOffscreen = (pos.z < 0f || d > 1f);
        if (isOffscreen)
        {
            pos.x /= d;
            pos.y /= d;
        }

        // スクリーン座標系の値をUI座標系の値に変換する
        rectTransform.anchoredPosition = pos / canvasScale;

        targetImage.enabled = isOffscreen;
        arrow.enabled = isOffscreen;

        // ターゲットのスクリーン座標が画面外なら、ターゲットの方向を指す矢印を表示する
        if (isOffscreen)
            arrow.rectTransform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg);
    }
}
