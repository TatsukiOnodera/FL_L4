using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTime_SC : MonoBehaviour
{
    /*メンバ変数*/
    // プレイヤーのオブジェクト
    [SerializeField] private GameObject m_player;

    // 弾のオブジェクト
    [SerializeField] private GameObject m_bullet;

    // 巨大化時間
    [SerializeField] private int m_limitBigTime;

    // フィーバーゲージの最大値
    [SerializeField] private int m_limitFeverGauge;

    // ショットの最大間隔
    [SerializeField] private int m_LimitShotInterval;

    // フィーバーゲージ
    private int m_feverGauge = 0;

    // 巨大化タイマー
    private int m_isBigTimer = 0;

    // ショットの間隔タイマー
    private int m_intervalTimer = 0;

    // 前進する速度
    private float m_advanceSpeed = 0;

    /*メンバ関数*/
    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        m_isBigTimer = m_limitBigTime;
        m_intervalTimer = m_LimitShotInterval;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        // 巨大化していないなら処理を終了
        if (IsBig() == false)
        {
            return;
        }

        IsBigShot();

        CountLimit();
    }

    /// <summary>
    /// 巨大化処理
    /// </summary>
    private bool IsBig()
    {
        // フィーバーゲージがたまっていないか
        if (m_feverGauge < m_limitFeverGauge)
        {
            // スペースキーを押しているか
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_feverGauge++;
                if (m_limitFeverGauge <= m_feverGauge)
                {
                    m_player.transform.Translate(0, 5.0f, 0);
                }
            }

            m_player.transform.localScale = new Vector3(1, 1, 1);
            return false;
        }
        else
        {
            m_player.transform.localScale = new Vector3(5, 5, 1);
            return true;
        }
    }

    /// <summary>
    /// 巨大化時のショット
    /// </summary>
    private void IsBigShot()
    {
        // 間隔があいているか
        if (m_intervalTimer <= 0)
        {
            // マイクに音が入っているか
            if (0 < GetMikeVolume())
            {
                m_intervalTimer = m_LimitShotInterval;
                m_advanceSpeed = GetMikeVolume();

                Vector3 pos = m_player.transform.position;
                pos.x += m_advanceSpeed / 8 + 0.5f * m_player.transform.localScale.x;

                var bullet = Instantiate(m_bullet, pos, Quaternion.identity);
                bullet.transform.localScale = new Vector3(m_advanceSpeed / 4, m_advanceSpeed / 4, m_advanceSpeed / 4);
            }
        }
        else
        {
            m_player.transform.Translate(new Vector3(m_advanceSpeed / 10000, 0, 0));
            m_intervalTimer--;
        }
    }

    /// <summary>
    /// 制限時間
    /// </summary>
    private void CountLimit()
    {
        // 制限時間を過ぎたか
        if (m_isBigTimer <= 0)
        {
            m_isBigTimer = m_limitBigTime;
            m_intervalTimer = m_LimitShotInterval;
            m_feverGauge = 0;
            m_advanceSpeed = 0;
        }
        else
        {
            m_isBigTimer--;
        }
    }

    /// <summary>
    /// ボリュームを取得
    /// </summary>
    /// <returns>float</returns>
    private float GetMikeVolume()
    {
        Mike_SC mike = GetComponent<Mike_SC>();
        return mike.GetVolume();
    }

    /*アクセッサetc.*/
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
    /// フィーバーゲージのメーター割合を取得
    /// </summary>
    /// <returns>int</returns>
    public float GetFeverGauge()
    {
        if ((float)m_limitFeverGauge / m_feverGauge <= 0)
        {
            return 0;
        }

        return (float)m_limitFeverGauge / m_feverGauge;
    }
}
