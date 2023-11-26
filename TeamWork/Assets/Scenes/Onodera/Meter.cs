using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    /*メンバ変数*/
    // HUDのオブジェクト
    [SerializeField] private RectTransform meter;
    // フィーバーゲージのScript
    private FeverTime fever;

    // HUDの横幅
    private float m_width = 0;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネント取得
        GameObject obj = GameObject.Find("Player");
        fever = obj.GetComponent<FeverTime>();

        // 初期化
        m_width = meter.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        // メーターの割合値
        float meterValue = fever.GetFeverGauge();

        // 幅の変更
        meter.sizeDelta = new Vector2(m_width / meterValue, meter.rect.height);
    }
}
