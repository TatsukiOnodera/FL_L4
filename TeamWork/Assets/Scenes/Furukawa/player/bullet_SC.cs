using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_SC : MonoBehaviour
{
    int count = 0;
    float speed = 0.07f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 vec = Vector3.Normalize(transform.rotation.eulerAngles);
        if (transform.rotation.eulerAngles.x >= 270)
        {
            vec.x = -1.0f;
        }

        pos += vec * speed;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= 140)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            other.GetComponent<enemy_SC>().damage();
            Destroy(this.gameObject);
        }
    }
}
