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

    private Animator animator;

    int jumpCount;

    int HP;

    private bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        animator = GetComponent<Animator>();
        rotation = new Vector3(0, 90, 0);
        jumpCount = 0;
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        animator.SetBool("move", false);
        // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
        if (Input.GetKey(KeyCode.A))
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
            rotation = new Vector3(0, -90, 0);

            animator.SetBool("move",true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
            rotation = new Vector3(0, 90, 0);

            animator.SetBool("move", true);
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;

            animator.SetTrigger("jump");
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
