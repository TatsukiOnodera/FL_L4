using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_SC : MonoBehaviour
{
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

    // アニメーション
    private Animator anim = null;
    private AnimatorStateInfo state;

    // SE
    [SerializeField] private AudioClip dead_SE;
    [SerializeField] private AudioClip damage_SE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rbody = GetComponent<Rigidbody>();
        if (this.gameObject.tag == "boss")
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は動かない
        if (Time.deltaTime == 0)
        {
            return;
        }

        // HPが0なら
        if (enemyHP <= 0)
        {
            audioSource.PlayOneShot(dead_SE);
            if (this.gameObject.tag == "enemy")
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "boss")
            {
                if (!anim.GetBool("death"))
                {
                    anim.SetBool("death", true);
                }
            }
        }

        // プレイヤーがいないもしくは歩行しない場合
        if (m_isWalker == false)
        {
            return;
        }

        // ジャンプ中なら
        if (m_isJump == true)
        {
            if (GameObject.Find("player_model") == null)
            {
                return;
            }

            float playerX = GameObject.Find("player_model").transform.position.x;
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
        if (m_isJump == false && 120 < jumpInterval)
        {
            jumpInterval = 0;
            m_isJump = true;
            rbody.AddForce(new Vector3(0, 1, 0) * jumpPower);
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

    /// <summary>
    /// ジャンプ状態を取得
    /// </summary>
    /// <returns></returns>
    public bool GetIsJump()
    {
        return m_isJump;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void damage()
    {
        audioSource.PlayOneShot(damage_SE);
        enemyHP -= 1;
    }

    /// <summary>
    /// HPを取得
    /// </summary>
    /// <returns></returns>
    public int GetHP()
    {
        return enemyHP;
    }
}
