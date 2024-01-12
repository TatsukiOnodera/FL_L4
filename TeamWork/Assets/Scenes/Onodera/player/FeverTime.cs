using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverTime : MonoBehaviour
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
    [SerializeField] private int m_limitShotInterval;

    // �t�B�[�o�[�Q�[�W
    private int m_feverGauge = 0;

    // ���剻�^�C�}�[
    private int m_isBigTimer = 0;

    // �V���b�g�̊Ԋu�^�C�}�[
    private int m_intervalTimer = 0;

    // ���剻���
    private bool m_isBig = false;

    /*�����o�֐�*/
    // Start is called before the first frame update
    void Start()
    {
        m_isBigTimer = m_limitBigTime;
        m_intervalTimer = 0;
        m_isBig = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ���剻���邩
        BeBig();

        // ���剻���Ă��Ȃ��Ȃ珈�����I��
        if (m_isBig == false)
        {
            return;
        }

        IsBigShot();

        CountLimit();
    }

    /// <summary>
    /// �t�B�[�o�[�Q�[�W�̏㏸
    /// </summary>
    public void UpFeverGauge()
    {
        m_feverGauge++;
        if (m_limitFeverGauge <= m_feverGauge)
        {
            m_feverGauge = m_limitFeverGauge;
        }
    }

    /// <summary>
    /// ���剻����
    /// </summary>
    private void BeBig()
    {
        // �t�B�[�o�[�Q�[�W�����܂��Ă��Ȃ���
        if (m_limitFeverGauge <= m_feverGauge && Input.GetKeyDown(KeyCode.V) && m_isBig == false)
        {
            if (m_player.transform.localScale.x != 5.0f)
            {
                m_player.transform.Translate(0, 2.5f, 0);
            }
            m_player.transform.localScale = new Vector3(5, 5, 5);
            m_isBig = true;
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
                m_intervalTimer = m_limitShotInterval;

                Vector3 pos = m_player.transform.position;
                float advanceSpeed = GetMikeVolume();
                pos.x += advanceSpeed / 8 + 0.5f * m_player.transform.localScale.x;

                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.y, rot.x, rot.z);

                var bullet = Instantiate(m_bullet, pos, Quaternion.Euler(rot));
                bullet.transform.localScale = new Vector3(advanceSpeed / 3, advanceSpeed / 3, advanceSpeed / 3);
            }
        }
        else
        {
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
            m_intervalTimer = 0;
            m_feverGauge = 0;
            m_player.transform.localScale = new Vector3(1, 1, 1);
            m_isBig = false;
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
        Mike mike = GetComponent<Mike>();
        return mike.GetVolume();
    }

    /// <summary>
    /// �t�B�[�o�[�Q�[�W�̊������擾
    /// </summary>
    /// <returns>int</returns>
    public float GetFeverGauge()
    {
        return (float)m_limitFeverGauge / m_feverGauge;
    }

    /*�A�N�Z�b�Tetc.*/
    /// <summary>
    /// ���剻����
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        return m_isBig;
    }

    /// <summary>
    /// �t�B�[�o�[�Q�[�W�̍ő�l���擾
    /// </summary>
    /// <returns></returns>
    public int GetLimitFeverGauge()
    {
        return m_limitFeverGauge;
    }

    /// <summary>
    /// �C���^�[�o���̃^�C�}�[���擾
    /// </summary>
    /// <returns>int</returns>
    public int GetIntervalTimer()
    {
        return m_intervalTimer;
    }

    /// <summary>
    /// �t�B�[�o�[�Q�[�W���Z�b�g
    /// </summary>
    /// <param name="feverGaugeNum">int</param>
    public void SetFeverGauge(int feverGaugeNum)
    {
        m_feverGauge = feverGaugeNum;
    }
}
