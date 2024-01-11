using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject bullet;

    public GameObject player;

    private int count = 0;
    [SerializeField] private int shotTiming = 800;

    [SerializeField] private int shotDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        count++;

        Vector3 enemyPos = transform.position;
        Vector3 playerPos = player.transform.position;
        float dis = Vector3.Distance(enemyPos, playerPos);
        if (shotTiming <= count && dis <= shotDistance)
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
