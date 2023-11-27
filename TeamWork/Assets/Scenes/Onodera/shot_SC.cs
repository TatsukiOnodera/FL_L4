using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_SC : MonoBehaviour
{
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FeverTime_SC fever = GetComponent<FeverTime_SC>();
        if (fever.GetIsBig() == true) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = transform.position;
            pos.x += 0.5f * transform.localScale.x + 0.5f * bullet.transform.localScale.x;
            Instantiate(bullet, pos, Quaternion.identity);
        }
    }
}
