using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject m_player;

    // ���剻�������̗L���t���O
    [SerializeField] private bool isBig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���剻���Ă��Ȃ���΍X�V���Ȃ�
        if (isBig == false)
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        m_player.transform.localScale = new Vector3(5, 5, 5);
    }
}
