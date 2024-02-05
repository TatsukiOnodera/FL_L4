using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_SC : MonoBehaviour
{
    // 横に移動する速度
    [SerializeField] private float speed = 0.01f;
    // ジャンプ力
    [SerializeField] private float jumpP = 2000f;
    // ジャンプ可能な回数
    [SerializeField] private int maxJumpCount = 1;

    // リジッドボディを使うための宣言
    Rigidbody rbody;
    Vector3 position;
    Vector3 rotation;

    // ジャンプのカウンター
    private int jumpCount;

    // HP
    [SerializeField] private int HP;

    // アニメーション
    private Animator anim = null;
    private AnimatorStateInfo animState;

    //無敵時間
    private int nodamageTime;
    private bool isAmor;

    // SE
    [SerializeField] private AudioClip dead_SE;
    [SerializeField] private AudioClip damage_SE;
    [SerializeField] private AudioClip jump_SE;
    private AudioSource audioSource;

    // 発射エフェクト
    [SerializeField] private GameObject dashEffect;

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody>();
        //初期化
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        rotation = new Vector3(0, 90, 0);
        jumpCount = 0;
        nodamageTime = 0;
        isAmor = false;
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は動かない
        if (Time.deltaTime == 0)
        {
            return;
        }

        // 座標取得
        position = transform.position;
        // アニメーション
        anim.SetBool("run", false);
        animState = anim.GetCurrentAnimatorStateInfo(0);

        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == false && HP > 0)
        {
            // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("L_Stick_H") < 0)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                position.x -= speed;
                rotation = new Vector3(0, -90, 0);
                anim.SetBool("run", true);

                if (jumpCount == 0)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Instantiate(dashEffect, pos, Quaternion.identity);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetAxis("L_Stick_H") > 0)
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                position.x += speed;
                rotation = new Vector3(0, 90, 0);
                anim.SetBool("run", true);

                if (jumpCount == 0)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Instantiate(dashEffect, pos, Quaternion.identity);
                }
            }
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")) && jumpCount < maxJumpCount)
            {
                audioSource.PlayOneShot(jump_SE);
                anim.SetBool("jump", true);
                // リジッドボディに力を加える（上方向にジャンプ力をかける）
                rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
                jumpCount++;
            }
        }
        else if (0 < fever.GetIntervalTimer())
        {
            position.x += speed * 0.1f;
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            rotation = new Vector3(0, 90, 0);
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);

        //無敵時間
        if (isAmor == true)
        {
            nodamageTime++;
            if (nodamageTime > 180)
            {
                isAmor = false;
            }
        }

        //HPが0になったら死亡
        if (HP == 0 && !anim.GetBool("death"))
        {
            audioSource.PlayOneShot(dead_SE);
            anim.SetBool("death", true);
            speed = 0.0f;
        }
        if (animState.IsName("metarig|dead 0") && animState.normalizedTime >= 1.5f)
        {
            Destroy(this.gameObject);
            GameObject obj = GameObject.Find("Scene");
            StageSceneChange scene = obj.GetComponent<StageSceneChange>();
            scene.GetIsFall();
        }

        // 落下したとき
        if (position.y < -5.5)
        {
            GameObject obj = GameObject.Find("Scene");
            StageSceneChange scene = obj.GetComponent<StageSceneChange>();
            scene.GetIsFall();
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
            jumpCount = 0;
            anim.SetBool("jump", false);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(dashEffect, pos, Quaternion.identity);
            }
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            FeverTime fever = GetComponent<FeverTime>();
            if (fever.GetIsBig() == false)
            {
                return;
            }

            int num = collision.gameObject.GetComponent<enemy_SC>().GetHP();
            for (int i = 0; i < num; i++)
            {
                collision.gameObject.GetComponent<enemy_SC>().damage();
            }
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void damage()
    {
        if (isAmor)
        {
            return;
        }

        HP--;
        isAmor = true;

        if (HP <= 0)
        {
            anim.SetBool("death", true);
        }
        else
        {
            audioSource.PlayOneShot(damage_SE);
        }
    }

    /// <summary>
    /// HPを取得
    /// </summary>
    /// <returns>int</returns>
    public int getHP()
    {
        return HP;
    }
}
