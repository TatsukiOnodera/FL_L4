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
    Vector3 rotation;

    int jumpCount;

    int HP;

    private Animator anim = null;
    private AnimatorStateInfo deathState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
        anim.SetBool("run", false);
        deathState = anim.GetCurrentAnimatorStateInfo(1);

        // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("L_Stick_H") < 0)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
            rotation = new Vector3(0, -90, 0);
            anim.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("L_Stick_H") > 0)
        {
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
            rotation = new Vector3(0, 90, 0);
            anim.SetBool("run", true);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")) && jumpCount < maxJumpCount)
        {
            anim.SetBool("jump", true);
            // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);


        //HPが0になったら死亡
        if (HP == 0 && deathState.normalizedTime <= 0)
        {
            anim.SetBool("death", true);
        }

        if (deathState.normalizedTime > 2.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
            anim.SetBool("jump", false);
        }
    }

    public void damage()
    {
        HP--;
        if (HP < 0)
        {
            anim.SetBool("death", true);
        }
    }

    public int getHP()
    {
        return HP;
    }
}
