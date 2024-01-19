using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    [SerializeField] private GameObject player;

    [SerializeField] private Enemy m_enemy;

    private int count = 0;
    [SerializeField] private int shotTiming = 600;

    [SerializeField] private int shotDistance = 25;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
        m_enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // É|Å[ÉYíÜÇÕìÆÇ©Ç»Ç¢
        if (Time.deltaTime == 0)
        {
            return;
        }

        if (player == null)
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
        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);
        Instantiate(bullet, pos, Quaternion.Euler(rot));

        var bulletobject = bullet.GetComponent<EnemyBullet>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);
    }
}
