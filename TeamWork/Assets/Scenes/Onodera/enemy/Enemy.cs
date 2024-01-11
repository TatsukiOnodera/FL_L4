using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // プレイヤーのオブジェクト
    //[SerializeField] private GameObject m_player;

    // HP
    [SerializeField] private int enemyHP = 3;

    // 動きまわるか
    [SerializeField] private bool m_isWalker = false;

    // 横に移動する速度
    [SerializeField] private float speed = 0.01f;

    // ジャンプ力
    [SerializeField] private float jumpPower = 1000f;

    // ジャンプの間隔
    private int jumpInterval = 0;

    // ジャンプできるか
    private bool m_isJump = false;

    // リジッドボディ
    private Rigidbody rbody;

    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // HPが0なら
        if (enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }

        // プレイヤーがいないもしくは歩行しない場合
        if (m_isWalker == false)
        {
            return;
        }

        pos = GameObject.Find("Player").transform.position;
        rbody = GetComponent<Rigidbody>();

        // ジャンプ中なら
        if (m_isJump == true)
        {
            float playerX = pos.x;
            float enemyX = transform.position.x;
            if (playerX < enemyX)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                transform.position = new Vector3(transform.position.x - 1 * speed, transform.position.y, transform.position.z);
            }
            else
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                transform.position = new Vector3(transform.position.x + 1 * speed, transform.position.y, transform.position.z);
            }
        }

        // ジャンプできるか
        if (m_isJump == false && 90 < jumpInterval)
        {
            jumpInterval = 0;
            m_isJump = true;
            rbody.AddForce(new Vector3(0, 1, 0) * jumpPower);

            rbody = GetComponent<Rigidbody>();
            float playerX = pos.x;
            float enemyX = transform.position.x;
            if (playerX < enemyX)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            }
            else
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
            }
        }
        else
        {
            jumpInterval++;
        }
    }

    /// <summary>
    /// 当たり判定
    /// </summary>
    /// <param name="collision">Collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            m_isJump = false;
        }
    }

    public void damage()
    {
        enemyHP -= 1;
    }

    public int GetHP()
    {
        return enemyHP;
    }
}
