using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_E_SC : MonoBehaviour
{
    // 発射インターバル
    private int count = 0;

    // 弾速
    [SerializeField] private float speed = 1;

    // ベクトル
    [SerializeField] private Vector3 vec = Vector3.zero;

    // エフェクト
    [SerializeField] private GameObject damageEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は動かない
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
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(damageEffect, pos, Quaternion.identity);
            Destroy(this.gameObject);
            FeverTime m_feverTime = other.GetComponent<FeverTime>();
            if (m_feverTime.GetIsBig() == false)
            {
                other.GetComponent<player_SC>().damage();
            }
        }
        if (other.gameObject.tag == "floor")
        {
            Destroy(this.gameObject);
        }
    }
}
