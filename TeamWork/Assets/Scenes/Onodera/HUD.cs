using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    /*�����o�ϐ�*/
    // HUD�̃I�u�W�F�N�g
    [SerializeField] private RectTransform meter;

    // �v���C���[�̃R���|�[�l���g
    private player_SC m_player;

    // �v���C���[��HP
    public int m_HP = 0;

    // HUD�̉���
    public float m_width = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        m_player = obj.GetComponent<player_SC>();
        m_HP = m_player.GetHP();
        m_width = meter.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.GetIsAlive() == false)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            int nowHP = m_player.GetHP();
            meter.sizeDelta = new Vector2(m_width * (float)m_HP / nowHP, meter.rect.height);
        }
    }
}
