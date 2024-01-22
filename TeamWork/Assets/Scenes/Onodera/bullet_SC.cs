using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_SC : MonoBehaviour
{
    // タイマー
    [SerializeField] private int m_timer = 60;

    // カウンター
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.x += 0.07f;

        transform.position = new Vector3(pos.x, pos.y, pos.z);

        count++;

        if (count >= m_timer)
        {
            Destroy(this.gameObject);
        }
    }
}
