using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneChange : MonoBehaviour
{
    // フェードイン
    [SerializeField] private GameObject m_fadeIn;
    // フェードアウト
    [SerializeField] private GameObject m_fadeOut;
    // ゴールオブジェクト
    private Goal m_goal;
    // ポーズオブジェクト
    private Pause m_pause;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Goal");
        m_goal = obj.GetComponent<Goal>();
        m_pause = GetComponent<Pause>(); ;
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_goal.GetIsGoal() == true || m_pause.BackMenu() == true)
        {
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                if (m_pause.BackMenu() == true)
                {
                    m_pause.ResetPause();
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    if (SceneManager.GetActiveScene().name == "stage1")
                    {
                        SceneManager.LoadScene("GameClear");
                    }
                    else if (SceneManager.GetActiveScene().name == "Alexa_Space")
                    {
                        SceneManager.LoadScene("GameClear");
                    }
                }
            }
        }
    }
}
