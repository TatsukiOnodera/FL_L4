using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
   // 弾のコンポーネント
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == true) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position;
            pos.x += 0.5f * transform.localScale.x + 0.5f * bullet.transform.localScale.x;
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.y, rot.x, rot.z);
            Instantiate(bullet, pos, Quaternion.Euler(rot));
        }
    }
}
