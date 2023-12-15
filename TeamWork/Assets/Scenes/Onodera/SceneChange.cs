using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // フェードイン
    [SerializeField] private GameObject m_fadeIn;
    // フェードアウト
    [SerializeField] private GameObject m_fadeOut;
    // ゴールオブジェクト
    private Goal m_goal;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Goal");
        m_goal = obj.GetComponent<Goal>();
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_goal.GetIsGoal() == true)
        {
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
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
