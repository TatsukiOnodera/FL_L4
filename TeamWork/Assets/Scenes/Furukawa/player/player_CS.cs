using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_CS : MonoBehaviour
{
    public float speed = 0.01f;   // 横に移動する速度
    public float jumpP = 2000f; // ジャンプ力
    public int maxJumpCount = 1;

    Rigidbody rbody; // リジッドボディを使うための宣言
    Vector3 position;
    Vector3 rotation;

    int jumpCount;

    int HP;


    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        rotation = new Vector3(0, 90, 0);
        jumpCount = 0;
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("L_Stick_H") < 0)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
            rotation = new Vector3(0, -90, 0);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("L_Stick_H") > 0)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
            rotation = new Vector3(0, 90, 0);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")) && jumpCount < maxJumpCount)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
        }
    }

    public void damage()
    {
        HP--;
        if (HP < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
