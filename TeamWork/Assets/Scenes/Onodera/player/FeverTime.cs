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
    [SerializeField] private int m_limitShotInterval;

    // フィーバーゲージ
    private int m_feverGauge = 0;

    // 巨大化タイマー
    private int m_isBigTimer = 0;

    // ショットの間隔タイマー
    private int m_intervalTimer = 0;

    // 巨大化状態
    private bool m_isBig = false;

    /*メンバ関数*/
    // Start is called before the first frame update
    void Start()
    {
        m_isBigTimer = m_limitBigTime;
        m_intervalTimer = 0;
        m_isBig = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 巨大化するか
        BeBig();

        // 巨大化していないなら処理を終了
        if (m_isBig == false)
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
        }
    }

    /// <summary>
    /// 巨大化処理
    /// </summary>
    private void BeBig()
    {
        // フィーバーゲージがたまっていないか
        if (m_limitFeverGauge <= m_feverGauge && Input.GetKeyDown(KeyCode.V) && m_isBig == false)
        {
            if (m_player.transform.localScale.x != 5.0f)
            {
                m_player.transform.Translate(0, 2.5f, 0);
            }
            m_player.transform.localScale = new Vector3(5, 5, 5);
            m_isBig = true;
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
                m_intervalTimer = m_limitShotInterval;

                Vector3 pos = m_player.transform.position;
                float advanceSpeed = GetMikeVolume();
                pos.x += advanceSpeed / 8 + 0.5f * m_player.transform.localScale.x;

                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.y, rot.x, rot.z);

                var bullet = Instantiate(m_bullet, pos, Quaternion.Euler(rot));
                bullet.transform.localScale = new Vector3(advanceSpeed / 3, advanceSpeed / 3, advanceSpeed / 3);
            }
        }
        else
        {
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
            m_intervalTimer = 0;
            m_feverGauge = 0;
            m_player.transform.localScale = new Vector3(1, 1, 1);
            m_isBig = false;
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

    /// <summary>
    /// フィーバーゲージの割合を取得
    /// </summary>
    /// <returns>int</returns>
    public float GetFeverGauge()
    {
        return (float)m_limitFeverGauge / m_feverGauge;
    }

    /*アクセッサetc.*/
    /// <summary>
    /// 巨大化中か
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        return m_isBig;
    }

    /// <summary>
    /// フィーバーゲージの最大値を取得
    /// </summary>
    /// <returns></returns>
    public int GetLimitFeverGauge()
    {
        return m_limitFeverGauge;
    }

    /// <summary>
    /// インターバルのタイマーを取得
    /// </summary>
    /// <returns>int</returns>
    public int GetIntervalTimer()
    {
        return m_intervalTimer;
    }

    /// <summary>
    /// フィーバーゲージをセット
    /// </summary>
    /// <param name="feverGaugeNum">int</param>
    public void SetFeverGauge(int feverGaugeNum)
    {
        m_feverGauge = feverGaugeNum;
    }
}
