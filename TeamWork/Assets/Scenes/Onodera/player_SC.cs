using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_SC : MonoBehaviour
{
    public float speed = 0f;   // ���Ɉړ����鑬�x
    public float jumpP = 300f; // �W�����v��
    public Vector3 vec = new Vector3();
    public int maxJumpCount = 0;

    Rigidbody rbody; // ���W�b�h�{�f�B���g�����߂̐錾
    Vector3 position;

    int jumpCount;

    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        FeverTime_SC fever = GetComponent<FeverTime_SC>();
        if (fever.GetIsBig() == false)
        {
            // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
            if (Input.GetKey(KeyCode.A))
            {
                // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                position.x -= speed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                position.x += speed;
            }

            if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
            {
                // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
                rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
                jumpCount++;
            }
        }

        position.z = 0.0f;
        transform.position = position;
        vec = rbody.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
        }
    }
}
