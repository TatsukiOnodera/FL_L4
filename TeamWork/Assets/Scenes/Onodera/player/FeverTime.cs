using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTime : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
        m_isBigTimer = m_limitBigTime;
        m_intervalTimer = m_LimitShotInterval;
    }

    // Update is called once per frame
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
    /// フィーバーゲージの上昇
    /// </summary>
    public void UpFeverGauge()
    {
        m_feverGauge++;
        if (m_limitFeverGauge <= m_feverGauge)
        {
            m_feverGauge = m_limitFeverGauge;
            m_player.transform.Translate(0, 2.5f, 0);
        }
    }

    /// <summary>
    /// 巨大化処理
    /// </summary>
    private bool IsBig()
    {
        // フィーバーゲージがたまっていないか
        if (m_feverGauge < m_limitFeverGauge)
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return false;
        }
        else
        {
            m_player.transform.localScale = new Vector3(5, 5, 5);
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
        Mike mike = GetComponent<Mike>();
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
    /// フィーバーゲージの割合を取得
    /// </summary>
    /// <returns>int</returns>
    public float GetFeverGauge()
    {
        return (float)m_limitFeverGauge / m_feverGauge;
    }
}
