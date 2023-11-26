using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    /*メンバ変数*/
    // HUDのオブジェクト
    [SerializeField] private RectTransform meter;

    // プレイヤーのコンポーネント
    private player_SC m_player;

    // プレイヤーのHP
    public int m_HP = 0;

    // HUDの横幅
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
