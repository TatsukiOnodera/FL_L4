using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_CS : MonoBehaviour
{
    private int enemyHP;

    // Start is called before the first frame update
    void Start()
    {
        enemyHP = 3;
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
