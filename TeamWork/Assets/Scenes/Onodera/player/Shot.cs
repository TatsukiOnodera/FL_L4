using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public GameObject bullet;

    Quaternion rot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position;
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.y, rot.x, rot.z);
            Instantiate(bullet, pos, Quaternion.Euler(rot));
        }
    }
}
