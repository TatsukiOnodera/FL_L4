using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int enemyHP = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void damage()
    {
        enemyHP -= 1;
    }

    public int GetHP()
    {
        return enemyHP;
    }
}
