using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChange : MonoBehaviour
{
    // �t�F�[�h�C��
    [SerializeField] private GameObject m_fadeIn;
    // �t�F�[�h�A�E�g
    [SerializeField] private GameObject m_fadeOut;
    // �S�[���I�u�W�F�N�g
    [SerializeField] private Title m_title;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_title.GetIsChange() == true)
        {
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
