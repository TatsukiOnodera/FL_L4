using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_player;

    // ���剻�t���O
    [SerializeField] private bool m_isBig;

    // ���剻����
    [SerializeField] private int m_limit;

    // �^�C�}�[
    private int m_timer = 0;

    /// <summary>
    /// ������
    /// </summary>
    void Start()
    {
        m_timer = m_limit;
    }

    /// <summary>
    /// �X�V
    /// </summary>
    void Update()
    {
        // ���剻���Ă��Ȃ���΍X�V���Ȃ�
        if (m_isBig == false)
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }
        
        Mike mike = GetComponent<Mike>();
        float vol = mike.GetVolume();
        m_player.transform.localScale = new Vector3(1, vol + 1, 1);

        // ��������
        m_timer--;
        if (m_timer <= 0)
        {
            m_isBig = false;
            m_timer = m_limit;
        }
    }

    /// <summary>
    /// �傫����Ԃ̃t���O���擾
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsBig()
    {
        return m_isBig;
    }
}
