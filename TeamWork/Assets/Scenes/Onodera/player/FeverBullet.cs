using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBullet : MonoBehaviour
{
    int count = 0;
    [SerializeField] private float speed = 1.0f;

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

        if (count >= 240)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            for (int i = 0; i < 3; i++)
            {
                other.GetComponent<Enemy>().damage();
            }
            Destroy(this.gameObject);
            GameObject obj = GameObject.Find("Player");
            FeverTime fever = obj.GetComponent<FeverTime>();
            fever.UpFeverGauge();
        }
    }
}
