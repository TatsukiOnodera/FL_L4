using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_E_SC : MonoBehaviour
{
    // �I�u�W�F�N�g
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;

    // �G�l�~�[�N���X
    [SerializeField] private enemy_SC m_enemy;

    // ���˂̃^�C�~���O
    [SerializeField] private int shotTiming = 600;

    // ���˔͈�
    [SerializeField] private int shotDistance = 25;

    // ���˂̃C���^�[�o��
    private int count = 0;

    // SE
    [SerializeField] private AudioClip shot_SE;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {
            player = GameObject.Find("player_model");
        }
        m_enemy = GetComponent<enemy_SC>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.deltaTime == 0 || player == null)
        {
            return;
        }

        count++;

        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.transform.position;
        float dis = Vector3.Distance(enemyPos, playerPos);
        if (shotTiming <= count && dis <= shotDistance && m_enemy.GetIsJump() == false)
        {
            shot();
            count = 0;
        }

        if (enemyPos.x < playerPos.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
    }

    private void shot()
    {
        // SE
        audioSource.PlayOneShot(shot_SE);

        // ����
        Vector3 playerPos = player.transform.position;
        playerPos.y += 0.5f;
        transform.LookAt(playerPos);
        Vector3 pos = transform.position;
        pos.y += 0.75f;
        Vector3 rot = new Vector3(0.0f, 0.0f, 0.0f);
        GameObject shotObj = Instantiate(bullet, pos, Quaternion.Euler(rot));
        shotObj.GetComponent<bullet_E_SC>().setvec(transform.forward);
    }
}
