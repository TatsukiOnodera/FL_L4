using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // プレイヤーのオブジェクト
    [SerializeField] private GameObject m_player;

    // 巨大化時間
    [SerializeField] private int m_limitBigTime;

    // フィーバーゲージの最大値
    [SerializeField] private int m_limitFeverGauge;

    // フィーバーゲージ
    private int m_feverGauge = 0;

    // タイマー
    private int m_timer = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        m_timer = m_limitBigTime;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 巨大化していないか
        if (m_feverGauge < m_limitFeverGauge)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_feverGauge++;
            }
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        else
        {
            m_player.transform.localScale = new Vector3(5, 5, 1);
        }

        // 制限時間
        m_timer--;
        if (m_timer <= 0)
        {
            m_timer = m_limitBigTime;
            m_feverGauge = 0;
        }
    }

    /// <summary>
    /// 巨大化中か
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        if (m_feverGauge < m_limitFeverGauge)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// ボリュームを取得
    /// </summary>
    /// <returns>float</returns>
    private float GetMikeVolume()
    {
        Mike mike = GetComponent<Mike>();
        return mike.GetVolume();
    }
}
