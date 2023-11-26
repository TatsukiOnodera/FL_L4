using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���Ɉړ����鑬�x
    [SerializeField] private float speed = 0.01f;
    // �W�����v��
    [SerializeField] private float jumpP = 2000f;
    // �W�����v�\�ȉ�
    [SerializeField] private int maxJumpCount = 1;

    // ���W�b�h�{�f�B���g�����߂̐錾
    Rigidbody rbody;
    Vector3 position;
    Vector3 rotation;

    // �W�����v�̃J�E���^�[
    private int jumpCount = 0;

    // HP
    [SerializeField] private int HP = 0;

    // �����t���O
    private bool isAlive = false;

    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
        rbody = GetComponent<Rigidbody>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        rotation = new Vector3(0, 90, 0);

        // ������
        jumpCount = 0;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;

        // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
        if (Input.GetKey(KeyCode.A))
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            position.x -= speed;
            rotation = new Vector3(0, -90, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(1, 0, 0) * speed);
            position.x += speed;
            rotation = new Vector3(0, 90, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) && jumpCount < maxJumpCount)
        {
            // ���W�b�h�{�f�B�ɗ͂�������i������ɃW�����v�͂�������j
            rbody.AddForce(new Vector3(0, 1, 0) * jumpP);
            jumpCount++;
        }

        position.z = 0.0f;
        transform.position = position;
        transform.rotation = Quaternion.Euler(rotation);
    }

    /// <summary>
    /// �����蔻��
    /// </summary>
    /// <param name="collision">Collision</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            jumpCount = 0;
        }
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    public void damage()
    {
        HP--;
        if (HP < 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// HP���擾
    /// </summary>
    /// <returns>int</returns>
    public int GetHP()
    {
        return HP;
    }

    /// <summary>
    /// �����t���O���擾
    /// </summary>
    /// <returns>bool</returns>
    public bool GetIsAlive()
    {
        return isAlive;
    }
}
