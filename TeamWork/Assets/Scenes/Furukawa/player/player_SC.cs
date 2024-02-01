using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_SC : MonoBehaviour
{
    public float speed = 0.01f;   // ���Ɉړ����鑬�x
    public float jumpP = 2000f; // �W�����v��
    public int maxJumpCount = 1;

    Rigidbody rbody; // ���W�b�h�{�f�B���g�����߂̐錾
    Vector3 position;
    Vector3 rotation;

    int jumpCount;

    int HP;

    private Animator anim = null;
    private AnimatorStateInfo animState;

    //���G����
    private int nodamageTime = 0;
    private bool isAmor = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
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
        animState = anim.GetCurrentAnimatorStateInfo(0);

        // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("L_Stick_H") < 0)
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
            rotation = new Vector3(0, -90, 0);
            anim.SetBool("run", true);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetAxis("L_Stick_H") > 0)
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
            rotation = new Vector3(0, 90, 0);
            anim.SetBool("run", true);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")) && jumpCount < maxJumpCount)
        {
            anim.SetBool("jump", true);
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);

        //���G����
        if (isAmor)
        {
            nodamageTime++;
            if (nodamageTime > 1000)
            {
                //isAmor = false;
            }
        }

        //HP��0�ɂȂ����玀�S
        if (HP == 0 && !anim.GetBool("death"))
        {
            anim.SetBool("death", true);
            speed = 0.0f;
        }
        
        if (animState.IsName("metarig|dead 0") && animState.normalizedTime >= 1.5f)
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
        if (isAmor)
        {
            return;
        }

        HP--;
        isAmor = true;

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
