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
        // 初期化
        GameObject obj = GameObject.Find("Goal");
        m_goal = obj.GetComponent<Goal>();
        m_pause = GetComponent<Pause>();

        // フェードイン開始
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ステージが変わるときか
        if (m_goal.GetIsGoal() == true || m_pause.BackMenu() == true)
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
                else
                {
                    // 次のシーンに移行する
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
