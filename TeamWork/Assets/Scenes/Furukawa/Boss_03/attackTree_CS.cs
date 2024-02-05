using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTree_CS : MonoBehaviour
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
        Vector3 pos = transform.position;

        pos += vec * speed;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= 2000)
        {
            Destroy(this.gameObject);
        }
    }

    public void setvec(Vector3 vec)
    {
        this.vec = vec;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (other.GetComponent<player_SC>().getHP() == 0)
            {
                return;
            }
            other.GetComponent<player_SC>().damage();
        }
    }
}
