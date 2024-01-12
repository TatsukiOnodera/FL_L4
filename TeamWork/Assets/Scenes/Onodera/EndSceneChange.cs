using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneChange : MonoBehaviour
{
    // �t�F�[�h�C��
    [SerializeField] private GameObject m_fadeIn;
    // �t�F�[�h�A�E�g
    [SerializeField] private GameObject m_fadeOut;
    // �^�C�g���X�N���v�g
    [SerializeField] private End m_end;

    // Start is called before the first frame update
    void Start()
    {
        m_fadeIn.SetActive(true);
        m_fadeOut.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_end.GetIsEnd() == true)
        {
            m_fadeOut.SetActive(true);
            m_fadeIn.SetActive(false);

            FadeOut fadeOut = m_fadeOut.GetComponent<FadeOut>();
            if (fadeOut.GetIsEnd() == true)
            {
                SceneManager.LoadScene("Title");
            }
        }
    }
}
