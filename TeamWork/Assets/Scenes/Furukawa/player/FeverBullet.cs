using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeverBullet : MonoBehaviour
{
    // カウンター
    private int count = 0;
    // 弾速
    [SerializeField] private float speed = 1.0f;
    // 威力
    [SerializeField] private int damage = 1;

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
        Vector3 vec = Vector3.Normalize(transform.rotation.eulerAngles);
        if (transform.rotation.eulerAngles.x >= 270)
        {
            vec.x = -1.0f;
        }

        pos += vec * speed;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= 120)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            for (int i = 0; i < damage; i++)
            {
                other.GetComponent<enemy_SC>().damage();
            }
        }
    }
}
