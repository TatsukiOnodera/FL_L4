using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_SC : MonoBehaviour
{
    public float speed = 0.01f;   // 横に移動する速度
    public float jumpP = 2000f; // ジャンプ力
    public int maxJumpCount = 1;

    Rigidbody rbody; // リジッドボディを使うための宣言
    Vector3 position;

    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.position - position;
        position = transform.position;

        if (diff.magnitude > 0.01f)
        {
            diff = diff.normalized;
            transform.rotation = Quaternion.LookRotation(diff);
        }

        // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        if (Input.GetKey(KeyCode.A))
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;
        }

        position.z = 0.0f;
        transform.position = position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
        }
    }
}
