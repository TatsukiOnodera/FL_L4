using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_SC : MonoBehaviour
{
    public int enemyHP = 3;

    private GameObject enemy = null;
    private Animator anim = null;
    private AnimatorStateInfo state;

    // Start is called before the first frame update
    void Start()
    {
        enemy = this.gameObject;

        if (this.gameObject.tag == "boss")
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHP == 0)
        {
            if (this.gameObject.tag == "enemy")
            {
                Destroy(this.gameObject);
            }
            else if (this.gameObject.tag == "boss")
            {
                if (!anim.GetBool("death"))
                {
                    anim.SetBool("death", true);
                }
            }
        }
    }

    public void damage()
    {
        enemyHP -= 1;
    }
}
