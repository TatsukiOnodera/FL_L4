using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTime_SC : MonoBehaviour
{
    /*�����o�ϐ�*/
    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_player;

    // �e�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_bullet;

    // ���剻����
    [SerializeField] private int m_limitBigTime;

    // �t�B�[�o�[�Q�[�W�̍ő�l
    [SerializeField] private int m_limitFeverGauge;

    // �V���b�g�̍ő�Ԋu
    [SerializeField] private int m_LimitShotInterval;

    // �t�B�[�o�[�Q�[�W
    private int m_feverGauge = 0;

    // ���剻�^�C�}�[
    private int m_isBigTimer = 0;

    // �V���b�g�̊Ԋu�^�C�}�[
    private int m_intervalTimer = 0;

    // �O�i���鑬�x
    private float m_advanceSpeed = 0;

    /*�����o�֐�*/
    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        m_isBigTimer = m_limitBigTime;
        m_intervalTimer = m_LimitShotInterval;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    void Update()
    {
        // ���剻���Ă��Ȃ��Ȃ珈�����I��
        if (IsBig() == false)
        {
            return;
        }

        IsBigShot();

        CountLimit();
    }

    /// <summary>
    /// ���剻����
    /// </summary>
    private bool IsBig()
    {
        // �t�B�[�o�[�Q�[�W�����܂��Ă��Ȃ���
        if (m_feverGauge < m_limitFeverGauge)
        {
            // �X�y�[�X�L�[�������Ă��邩
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_feverGauge++;
                if (m_limitFeverGauge <= m_feverGauge)
                {
                    m_player.transform.Translate(0, 5.0f, 0);
                }
            }

            m_player.transform.localScale = new Vector3(1, 1, 1);
            return false;
        }
        else
        {
            m_player.transform.localScale = new Vector3(5, 5, 1);
            return true;
        }
    }

    /// <summary>
    /// ���剻���̃V���b�g
    /// </summary>
    private void IsBigShot()
    {
        // �Ԋu�������Ă��邩
        if (m_intervalTimer <= 0)
        {
            // �}�C�N�ɉ��������Ă��邩
            if (0 < GetMikeVolume())
            {
                m_intervalTimer = m_LimitShotInterval;
                m_advanceSpeed = GetMikeVolume();

                Vector3 pos = m_player.transform.position;
                pos.x += m_advanceSpeed / 8 + 0.5f * m_player.transform.localScale.x;

                var bullet = Instantiate(m_bullet, pos, Quaternion.identity);
                bullet.transform.localScale = new Vector3(m_advanceSpeed / 4, m_advanceSpeed / 4, m_advanceSpeed / 4);
            }
        }
        else
        {
            m_player.transform.Translate(new Vector3(m_advanceSpeed / 10000, 0, 0));
            m_intervalTimer--;
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void CountLimit()
    {
        // �������Ԃ��߂�����
        if (m_isBigTimer <= 0)
        {
            m_isBigTimer = m_limitBigTime;
            m_intervalTimer = m_LimitShotInterval;
            m_feverGauge = 0;
            m_advanceSpeed = 0;
        }
        else
        {
            m_isBigTimer--;
        }
    }

    /// <summary>
    /// �{�����[�����擾
    /// </summary>
    /// <returns>float</returns>
    private float GetMikeVolume()
    {
        Mike_SC mike = GetComponent<Mike_SC>();
        return mike.GetVolume();
    }

    /*�A�N�Z�b�Tetc.*/
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
    /// �t�B�[�o�[�Q�[�W�̃��[�^�[�������擾
    /// </summary>
    /// <returns>int</returns>
    public float GetFeverGauge()
    {
        if ((float)m_limitFeverGauge / m_feverGauge <= 0)
        {
            return 0;
        }

        return (float)m_limitFeverGauge / m_feverGauge;
    }
}