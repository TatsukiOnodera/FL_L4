using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChangeScene : MonoBehaviour
{
    // フェードイン
    [SerializeField] private GameObject m_fadeIn;
    // フェードアウト
    [SerializeField] private GameObject m_fadeOut;
    // メニュースクリプト
    [SerializeField] private Menu m_menu;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (0 < m_menu.GetStageNum())
        {
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                if (m_menu.GetStageNum() == 1)
                {
                    SceneManager.LoadScene("stage1");
                }
                else if (m_menu.GetStageNum() == 2)
                {
                    SceneManager.LoadScene("stage2");
                }
                else if (m_menu.GetStageNum() == 3)
                {
                    SceneManager.LoadScene("stage3");
                }
            }
        }
    }
}
