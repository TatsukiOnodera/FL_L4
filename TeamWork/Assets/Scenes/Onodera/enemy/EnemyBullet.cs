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
        // ƒ|[ƒY’†‚Í“®‚©‚È‚¢
        if (Time.deltaTime == 0)
        {
            return;
        }

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
            Destroy(this.gameObject);
            FeverTime m_feverTime = other.GetComponent<FeverTime>();
            if (m_feverTime.GetIsBig() == false)
            {
                other.GetComponent<Player>().Damage();
            }
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }
}
