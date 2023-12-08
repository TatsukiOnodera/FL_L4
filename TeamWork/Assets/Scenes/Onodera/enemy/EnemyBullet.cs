using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 vec = Vector3.zero;

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

        if (count >= 300)
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
            GameObject obj = GameObject.Find("Player");
            FeverTime m_feverTime = obj.GetComponent<FeverTime>();
            if (m_feverTime.GetIsBig() == true) return;

                other.GetComponent<Player>().Damage();
            Destroy(this.gameObject);
        }
    }
}