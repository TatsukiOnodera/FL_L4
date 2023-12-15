using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private bool m_isGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        m_isGoal = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">Collision</param>
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("player") && m_isGoal == false)
        {
            m_isGoal = true;
        }
    }

    /// <summary>
    /// ゴールフラグを取得
    /// </summary>
    /// <returns></returns>
    public bool GetIsGoal()
    {
        return m_isGoal;
    }

    /// <summary>
    /// ゴールフラグをセット
    /// </summary>
    /// <param name="isGoal"></param>
    public void SetIsGoal(bool isGoal)
    {
        m_isGoal = isGoal;
    }
}
