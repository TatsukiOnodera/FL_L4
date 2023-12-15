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
    private Player m_player;

    // �t�B�[�o�[�Q�[�W�̃R���|�[�l���g
    private FeverTime m_feverTime;

    // �v���C���[�̍ő�HP
    public int m_maxHP = 0;

    // HUD�̉���
    public float m_width = 0;

    public int nowHP = 0;

    public float nowFever = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        m_player = obj.GetComponent<Player>();
        m_feverTime = obj.GetComponent<FeverTime>();

        m_maxHP = m_player.GetHP();
        m_width = HPGauge.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        nowHP = m_player.GetHP();
        HPGauge.sizeDelta = new Vector2((m_width / (float)m_maxHP) * nowHP, HPGauge.rect.height);

        nowFever = m_feverTime.GetFeverGauge();
        FeverGauge.sizeDelta = new Vector2(m_width / nowFever, FeverGauge.rect.height);
    }
}
