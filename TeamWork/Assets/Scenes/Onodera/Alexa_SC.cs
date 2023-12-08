using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alexa_SC : MonoBehaviour
{
    // プレイヤーのオブジェクト
    private FeverTime m_fever;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        m_fever = obj.GetComponent<FeverTime>();
        Application.targetFrameRate = 60;
        int num = m_fever.GetLimitFeverGauge();
        m_fever.SetFeverGauge(num - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
