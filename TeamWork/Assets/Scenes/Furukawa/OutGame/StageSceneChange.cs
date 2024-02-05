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
    // 落下したか
    private bool m_isFall;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        GameObject obj = GameObject.Find("Goal");
        m_goal = obj.GetComponent<Goal>();
        m_pause = GetComponent<Pause>();
        m_isFall = false;

        // フェードイン開始
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ステージが変わるときか
        if (m_goal.GetIsGoal() == true || m_pause.BackMenu() == true || m_isFall == true)
        {
            // フェードアウト開始
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            // フェードアウトが終わったか
            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                // メニューに戻るか
                if (m_pause.BackMenu() == true)
                {
                    m_pause.ResetPause();
                    SceneManager.LoadScene("Menu");
                }
                else if (m_isFall == true)
                {
                    SceneManager.LoadScene("GameOver");
                }
                else
                {
                    // 次のシーンに移行する
                    if (SceneManager.GetActiveScene().name == "stage1")
                    {
                        SceneManager.LoadScene("GameClear");
                        //SceneManager.LoadScene("stage_boss1");
                    }
                    else if (SceneManager.GetActiveScene().name == "stage2")
                    {
                        SceneManager.LoadScene("GameClear");
                        //SceneManager.LoadScene("stage_boss2");
                    }
                    else if (SceneManager.GetActiveScene().name == "stage3")
                    {
                        SceneManager.LoadScene("GameClear");
                        //SceneManager.LoadScene("stage_boss3");
                    }
                    else if (SceneManager.GetActiveScene().name == "stage_boss1")
                    {
                        SceneManager.LoadScene("GameClear");
                    }
                    else if (SceneManager.GetActiveScene().name == "stage_boss2")
                    {
                        SceneManager.LoadScene("GameClear");
                    }
                    else if (SceneManager.GetActiveScene().name == "stage_boss3")
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

    /// <summary>
    /// プレイヤーが落ちた
    /// </summary>
    public void GetIsFall()
    {
        m_isFall = true;
    }
}
