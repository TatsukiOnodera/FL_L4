using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_player;

    // ���剻����
    [SerializeField] private int m_limitBigTime;

    // �t�B�[�o�[�Q�[�W�̍ő�l
    [SerializeField] private int m_limitFeverGauge;

    // �t�B�[�o�[�Q�[�W
    private int m_feverGauge = 0;

    // �^�C�}�[
    private int m_timer = 0;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        m_timer = m_limitBigTime;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    void Update()
    {
        // ���剻���Ă��Ȃ���
        if (m_feverGauge < m_limitFeverGauge)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_feverGauge++;
            }
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        else
        {
            m_player.transform.localScale = new Vector3(5, 5, 1);
        }

        // ��������
        m_timer--;
        if (m_timer <= 0)
        {
            m_timer = m_limitBigTime;
            m_feverGauge = 0;
        }
    }

    /// <summary>
    /// ���剻����
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        if (m_feverGauge < m_limitFeverGauge)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// �{�����[�����擾
    /// </summary>
    /// <returns>float</returns>
    private float GetMikeVolume()
    {
        Mike mike = GetComponent<Mike>();
        return mike.GetVolume();
    }
}
