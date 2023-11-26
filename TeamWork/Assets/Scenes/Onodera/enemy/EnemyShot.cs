using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject bullet;

    private GameObject player;

    int count = 0;
    const int shotTiming = 800;

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

        if(count == shotTiming)
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

        var bulletobject = bullet.GetComponent<bullet_E_SC>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);
    }
}
