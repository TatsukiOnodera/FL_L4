using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour
{
    /*�����o�ϐ�*/
    // HUD�̃I�u�W�F�N�g
    [SerializeField] private RectTransform meter;
    // �t�B�[�o�[�Q�[�W��Script
    private FeverTime fever;

    // HUD�̉���
    private float m_width = 0;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�擾
        GameObject obj = GameObject.Find("Player");
        fever = obj.GetComponent<FeverTime>();

        // ������
        m_width = meter.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        // ���[�^�[�̊����l
        float meterValue = fever.GetFeverGauge();

        // ���̕ύX
        meter.sizeDelta = new Vector2(m_width / meterValue, meter.rect.height);
    }
}
