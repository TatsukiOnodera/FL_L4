using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_SC : MonoBehaviour
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
            Quaternion rot = transform.rotation;
            pos.x += 1.0f;
            Instantiate(bullet, pos, rot);
        }
    }
}
