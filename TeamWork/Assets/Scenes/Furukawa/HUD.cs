using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    // HP��HUD
    [SerializeField] private RectTransform HPGauge;
    // �t�B�[�o�[�Q�[�W��HUD
    [SerializeField] private RectTransform FeverGauge;

    // �v���C���[�̃R���|�[�l���g
    private player_SC m_player;

    // �t�B�[�o�[�Q�[�W�̃R���|�[�l���g
    private FeverTime m_feverTime;

    // �v���C���[�̍ő�HP
    private int m_maxHP = 0;

    // HUD�̉���
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
