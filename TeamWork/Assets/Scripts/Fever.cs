using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // プレイヤーのオブジェクト
    [SerializeField] private GameObject m_player;

    // 巨大化フラグ
    [SerializeField] private bool m_isBig;

    // 巨大化時間
    [SerializeField] private int m_limit;

    // タイマー
    private int m_timer = 0;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        m_timer = m_limit;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 巨大化していなければ更新しない
        if (m_isBig == false)
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        
        Mike mike = GetComponent<Mike>();
        float vol = mike.GetVolume();
        m_player.transform.localScale = new Vector3(1, vol + 1, 1);

        // 制限時間
        m_timer--;
        if (m_timer <= 0)
        {
            m_isBig = false;
            m_timer = m_limit;
        }
    }

    /// <summary>
    /// 大きい状態のフラグを取得
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        return m_isBig;
    }
}
