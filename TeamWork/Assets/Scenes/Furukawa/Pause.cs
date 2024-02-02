using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // �|�[�Y���
    [SerializeField] private GameObject m_pause;

    // �J�[�\��
    [SerializeField] private RectTransform selectCursor;

    // �J�[�\����Y���W
    private float textY;

    // �|�[�Y���
    private bool isPose = false;

    // ���j���[��ʂɈڍs
    private bool isBack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_pause ==null)
        {
            m_pause = GameObject.Find("Pause");
        }
        ResetPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPose == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                isPose = true;
                m_pause.SetActive(true);
                textY = -50;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isBack == false)
            {
                if (textY == -50)
                {
                    isBack = true;
                }
                else
                {
                    ResetPause();
                }
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                if (textY == -50)
                {
                    textY = -230;
                }
                else
                {
                    textY = -50;
                }
            }
        }
            
        selectCursor.anchoredPosition = new Vector2(-275, textY);
    }

    public void ResetPause()
    {
        isPose = false;
        isBack = false;
        Time.timeScale = 1;
        m_pause.SetActive(false);
        textY = -50;
    }

    /// <summary>
    /// ���j���[�Ɉړ�
    /// </summary>
    /// <returns>bool</returns>
    public bool BackMenu()
    {
        return isBack;
    }
}
