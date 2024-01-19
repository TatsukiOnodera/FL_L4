using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    // Update is called once per frame
    void Update()
    {
        // �|�[�Y���͓����Ȃ�
        if (Time.deltaTime == 0)
        {
            return;
        }

        position = transform.position;

        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == false)
        {
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

        if (position.y < -5.5)
        {
            SceneManager.LoadScene("GameOver");
        }
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
        if (collision.gameObject.CompareTag("enemy"))
        {
            FeverTime fever = GetComponent<FeverTime>();
            if (fever.GetIsBig() == false)
            {
                return;
            }

            int num = collision.gameObject.GetComponent<Enemy>().GetHP();
            for (int i = 0; i < num; i++)
            {
                collision.gameObject.GetComponent<Enemy>().damage();
            }
        }
    }

    /// <summary>
    /// �_���[�W����
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
