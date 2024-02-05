using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_SC : MonoBehaviour
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
    private int jumpCount;

    // HP
    [SerializeField] private int HP;

    // �A�j���[�V����
    private Animator anim = null;
    private AnimatorStateInfo animState;

    //���G����
    private int nodamageTime;
    private bool isAmor;

    // SE
    [SerializeField] private AudioClip dead_SE;
    [SerializeField] private AudioClip damage_SE;
    [SerializeField] private AudioClip jump_SE;
    private AudioSource audioSource;

    // ���˃G�t�F�N�g
    [SerializeField] private GameObject dashEffect;

    // Start is called before the first frame update
    void Start()
    {
        // ���W�b�h�{�f�B2D���R���|�[�l���g����擾���ĕϐ��ɓ����
        rbody = GetComponent<Rigidbody>();
        //������
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        position = transform.position;
        transform.rotation = Quaternion.identity;
        rotation = new Vector3(0, 90, 0);
        jumpCount = 0;
        nodamageTime = 0;
        isAmor = false;
    }

    // Update is called once per frame
    void Update()
    {
        // �|�[�Y���͓����Ȃ�
        if (Time.deltaTime == 0)
        {
            return;
        }

        // ���W�擾
        position = transform.position;
        // �A�j���[�V����
        anim.SetBool("run", false);
        animState = anim.GetCurrentAnimatorStateInfo(0);

        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == false && HP > 0)
        {
            // �W�����v�����邽�߂̃R�[�h�i�����X�y�[�X�L�[��������āA������ɑ��x���Ȃ����Ɂj
            if (Input.GetKey(KeyCode.A) || Input.GetAxis("L_Stick_H") < 0)
            {
                rbody.AddForce(new Vector3(-1, 0, 0) * speed);
                position.x -= speed;
                rotation = new Vector3(0, -90, 0);
                anim.SetBool("run", true);

                if (jumpCount == 0)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Instantiate(dashEffect, pos, Quaternion.identity);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetAxis("L_Stick_H") > 0)
            {
                rbody.AddForce(new Vector3(1, 0, 0) * speed);
                position.x += speed;
                rotation = new Vector3(0, 90, 0);
                anim.SetBool("run", true);

                if (jumpCount == 0)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    Instantiate(dashEffect, pos, Quaternion.identity);
                }
            }
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown("joystick button 0")) && jumpCount < maxJumpCount)
            {
                audioSource.PlayOneShot(jump_SE);
                anim.SetBool("jump", true);
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

        //���G����
        if (isAmor == true)
        {
            nodamageTime++;
            if (nodamageTime > 180)
            {
                isAmor = false;
            }
        }

        //HP��0�ɂȂ����玀�S
        if (HP == 0 && !anim.GetBool("death"))
        {
            audioSource.PlayOneShot(dead_SE);
            anim.SetBool("death", true);
            speed = 0.0f;
        }
        if (animState.IsName("metarig|dead 0") && animState.normalizedTime >= 1.5f)
        {
            Destroy(this.gameObject);
            GameObject obj = GameObject.Find("Scene");
            StageSceneChange scene = obj.GetComponent<StageSceneChange>();
            scene.GetIsFall();
        }

        // ���������Ƃ�
        if (position.y < -5.5)
        {
            GameObject obj = GameObject.Find("Scene");
            StageSceneChange scene = obj.GetComponent<StageSceneChange>();
            scene.GetIsFall();
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
            anim.SetBool("jump", false);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(dashEffect, pos, Quaternion.identity);
            }
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            FeverTime fever = GetComponent<FeverTime>();
            if (fever.GetIsBig() == false)
            {
                return;
            }

            int num = collision.gameObject.GetComponent<enemy_SC>().GetHP();
            for (int i = 0; i < num; i++)
            {
                collision.gameObject.GetComponent<enemy_SC>().damage();
            }
        }
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    public void damage()
    {
        if (isAmor)
        {
            return;
        }

        HP--;
        isAmor = true;

        if (HP <= 0)
        {
            anim.SetBool("death", true);
        }
        else
        {
            audioSource.PlayOneShot(damage_SE);
        }
    }

    /// <summary>
    /// HP���擾
    /// </summary>
    /// <returns>int</returns>
    public int getHP()
    {
        return HP;
    }
}
