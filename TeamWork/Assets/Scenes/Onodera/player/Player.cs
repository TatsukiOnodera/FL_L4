using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
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
    private int jumpCount = 0;

    // HP
    [SerializeField] private int HP = 0;

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        rotation = new Vector3(0, 90, 0);

        // 初期化
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == false)
        {
            // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
            if (Input.GetKey(KeyCode.A))
            {
                // リジッドボディに力を加える（上方向にジャンプ力をかける）
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                position.x -= speed;
                rotation = new Vector3(0, -90, 0);
            }

            if (Input.GetKey(KeyCode.D))
            {
                // リジッドボディに力を加える（上方向にジャンプ力をかける）
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                position.x += speed;
                rotation = new Vector3(0, 90, 0);
            }

            if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
            {
                // リジッドボディに力を加える（上方向にジャンプ力をかける）
                rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
                jumpCount++;
            }
        }
        else if (0 < fever.GetIntervalTimer())
        {
            float num = 0.01f;
            position.x += num;
            rbody.AddForce(new Vector3(1, 0, 0) * num);
            rotation = new Vector3(0, 90, 0);
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);

        if (position.y < -5.5)
        {
            SceneManager.LoadScene("GameOver");
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            FeverTime fever = GetComponent<FeverTime>();
            if (0 < fever.GetIntervalTimer())
            {
                return;
            }

            int num = other.GetComponent<Enemy>().GetHP();
            for (int i = 0; i < num; i++)
            {
                other.GetComponent<Enemy>().damage();
            }
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    public void Damage()
    {
        FeverTime fever = GetComponent<FeverTime>();
        fever.UpFeverGauge();
        HP--;
        if (HP <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    /// <summary>
    /// HPを取得
    /// </summary>
    /// <returns>int</returns>
    public int GetHP()
    {
        return HP;
    }

    /// <summary>
    /// 生死フラグを取得
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsAlive()
    {
        if (0 < HP)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
