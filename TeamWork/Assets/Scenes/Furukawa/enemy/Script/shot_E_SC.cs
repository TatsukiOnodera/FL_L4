using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_E_SC : MonoBehaviour
{
    // オブジェクト
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject player;

    // エネミークラス
    [SerializeField] private enemy_SC m_enemy;

    // 発射のタイミング
    [SerializeField] private int shotTiming = 600;

    // 発射範囲
    [SerializeField] private int shotDistance = 25;

    // 発射のインターバル
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
            player = GameObject.FindGameObjectWithTag("player_model");
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
    }

    private void shot()
    {
        audioSource.PlayOneShot(shot_SE);

        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);
        Instantiate(bullet, pos, Quaternion.Euler(rot));

        var bulletobject = bullet.GetComponent<bullet_E_SC>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);
    }
}
