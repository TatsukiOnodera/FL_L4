using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // HPのHUD
    [SerializeField] private RectTransform HPGauge;
    // フィーバーゲージのHUD
    [SerializeField] private RectTransform FeverGauge;

    // プレイヤーのコンポーネント
    private player_SC m_player;

    // フィーバーゲージのコンポーネント
    private FeverTime m_feverTime;

    // プレイヤーの最大HP
    private int m_maxHP = 0;

    // HUDの横幅
    private float m_width = 0;

    private int nowHP = 0;

    private float nowFever = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("player_model");
        m_player = obj.GetComponent<player_SC>();
        m_feverTime = obj.GetComponent<FeverTime>();

        m_maxHP = m_player.getHP();
        m_width = HPGauge.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player == null || m_feverTime == null)
        {
            return;
        }

        nowHP = m_player.getHP();
        HPGauge.sizeDelta = new Vector2((m_width / (float)m_maxHP) * nowHP, HPGauge.rect.height);

        nowFever = m_feverTime.GetFeverGauge();
        FeverGauge.sizeDelta = new Vector2(m_width / nowFever, FeverGauge.rect.height);
    }
}
