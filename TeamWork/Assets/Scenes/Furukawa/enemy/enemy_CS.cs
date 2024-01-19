using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_CS : MonoBehaviour
{
    public int enemyHP = 3;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP == 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void damage()
    {
        enemyHP -= 1;
    }
}
