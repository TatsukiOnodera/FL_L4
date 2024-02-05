using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shield_CS : MonoBehaviour
{
    int count = 0;
    float speed = 0.02f;
    public Vector3 vec = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if (count >= 1000)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
