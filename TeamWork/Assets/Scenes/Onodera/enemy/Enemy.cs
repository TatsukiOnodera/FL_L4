using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // �v���C���[�̃I�u�W�F�N�g
    //[SerializeField] private GameObject m_player;

    // HP
    [SerializeField] private int enemyHP = 3;

    // �����܂�邩
    [SerializeField] private bool m_isWalker = false;

    // ���Ɉړ����鑬�x
    [SerializeField] private float speed = 0.01f;

    // �W�����v��
    [SerializeField] private float jumpPower = 1000f;

    // �W�����v�̊Ԋu
    private int jumpInterval = 0;

    // �W�����v�ł��邩
    private bool m_isJump = false;

    // ���W�b�h�{�f�B
    private Rigidbody rbody;

    public Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // HP��0�Ȃ�
        if (enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }

        // �v���C���[�����Ȃ��������͕��s���Ȃ��ꍇ
        if (m_isWalker == false)
        {
            return;
        }

        pos = GameObject.Find("Player").transform.position;
        rbody = GetComponent<Rigidbody>();

        // �W�����v���Ȃ�
        if (m_isJump == true)
        {
            float playerX = pos.x;
            float enemyX = transform.position.x;
            if (playerX < enemyX)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                transform.position = new Vector3(transform.position.x - 1 * speed, transform.position.y, transform.position.z);
            }
            else
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                transform.position = new Vector3(transform.position.x + 1 * speed, transform.position.y, transform.position.z);
            }
        }

        // �W�����v�ł��邩
        if (m_isJump == false && 90 < jumpInterval)
        {
            jumpInterval = 0;
            m_isJump = true;
            rbody.AddForce(new Vector3(0, 1, 0) * jumpPower);

            rbody = GetComponent<Rigidbody>();
            float playerX = pos.x;
            float enemyX = transform.position.x;
            if (playerX < enemyX)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
            }
            else
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
            }
        }
        else
        {
            jumpInterval++;
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
            m_isJump = false;
        }
    }

    public void damage()
    {
        enemyHP -= 1;
    }

    public int GetHP()
    {
        return enemyHP;
    }
}
