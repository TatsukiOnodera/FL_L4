using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    // フェードの速度
    [SerializeField] private float m_fadeSpeed = 1;
    // スクリーンの色
    private Color m_color = new Vector4(0, 0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        m_color = GetComponent<Image>().color;
        m_color.r = 0.0f;
        m_color.g = 0.0f;
        m_color.b = 0.0f;
        m_color.a = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < m_color.a)
        {
            m_color.a -= m_fadeSpeed;
            if (m_color.a < 0)
            {
                m_color.a = 0;
            }

            GetComponent<Image>().color = m_color;
        }
    }

    /// <summary>
    /// フェードが終了したか
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsEnd()
    {
        if (m_color.a <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
