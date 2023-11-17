using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_SC : MonoBehaviour
{
    int count = 0;
    float speed = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos += transform.rotation.eulerAngles * speed;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= 60)
        {
            Destroy(this.gameObject);
        }
    }
}
