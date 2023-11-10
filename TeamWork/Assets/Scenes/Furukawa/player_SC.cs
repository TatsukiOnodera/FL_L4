using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_SC : MonoBehaviour
{
    public float speed = 0f;   // 横に移動する速度
    public float jumpP = 300f; // ジャンプ力
    public Vector3 vec = new Vector3();

    Rigidbody rbody; // リジッドボディを使うための宣言
    Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

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

        if (Input.GetKeyDown(KeyCode.W) /*&& rbody.velocity.y == 0*/)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
        }

        position.z = 0.0f;
        transform.position = position;
        vec = rbody.velocity;
    }
}
