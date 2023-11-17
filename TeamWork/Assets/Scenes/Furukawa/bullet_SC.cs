using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_SC : MonoBehaviour
{
    int count = 0;
    float speed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 vec = transform.rotation.eulerAngles;

        pos += vec * speed;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= 60)
        {
            Destroy(this.gameObject);
        }
    }
}
