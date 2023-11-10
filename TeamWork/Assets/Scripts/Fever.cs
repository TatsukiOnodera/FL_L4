using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fever : MonoBehaviour
{
    // プレイヤーのオブジェクト
    [SerializeField] private GameObject m_player;

    // 巨大化＆処理の有無フラグ
    [SerializeField] private bool isBig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 巨大化していなければ更新しない
        if (isBig == false)
        {
            m_player.transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        m_player.transform.localScale = new Vector3(5, 5, 5);
    }
}
