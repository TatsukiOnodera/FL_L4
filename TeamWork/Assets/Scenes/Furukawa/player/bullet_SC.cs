using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_SC : MonoBehaviour
{
    // カウンター
    private int count = 0;
    // 弾速
    [SerializeField] private float speed = 0.07f;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
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

        if (count >= 200)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy" || other.gameObject.tag == "boss")
        {
            other.GetComponent<enemy_SC>().damage();
            Destroy(this.gameObject);
            GameObject obj = GameObject.Find("player_model");
            FeverTime fever = obj.GetComponent<FeverTime>();
            fever.UpFeverGauge();
        }
    }
}
