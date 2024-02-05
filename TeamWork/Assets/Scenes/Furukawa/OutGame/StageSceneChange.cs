using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSceneChange : MonoBehaviour
{
    // �t�F�[�h�C��
    [SerializeField] private GameObject m_fadeIn;
    // �t�F�[�h�A�E�g
    [SerializeField] private GameObject m_fadeOut;
    // �S�[���I�u�W�F�N�g
    private Goal m_goal;
    // �|�[�Y�I�u�W�F�N�g
    private Pause m_pause;
    // ����������
    private bool m_isFall;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        GameObject obj = GameObject.Find("Goal");
        m_goal = obj.GetComponent<Goal>();
        m_pause = GetComponent<Pause>();
        m_isFall = false;

        // �t�F�[�h�C���J�n
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // �X�e�[�W���ς��Ƃ���
        if (m_goal.GetIsGoal() == true || m_pause.BackMenu() == true || m_isFall == true)
        {
            // �t�F�[�h�A�E�g�J�n
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            // �t�F�[�h�A�E�g���I�������
            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                // ���j���[�ɖ߂邩
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
                    // ���̃V�[���Ɉڍs����
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
    /// �v���C���[��������
    /// </summary>
    public void GetIsFall()
    {
        m_isFall = true;
    }
}
