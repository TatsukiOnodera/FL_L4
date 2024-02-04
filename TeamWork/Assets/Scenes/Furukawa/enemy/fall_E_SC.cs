using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class fall_E_SC : MonoBehaviour
{
    public enum enemyState
    {
        idle, //ë“ã@
        fall, //ìGê∂ê¨
    };

    public int maxMoveCount = 500;
    public float moveSpeed = 0.005f;
    public float flyHeight = 10.0f;
    public GameObject fallEnemy = null;

    private Animator anim;
    private AnimatorStateInfo animState;
    private enemyState myState = enemyState.idle;
    private GameObject player = null;
    private float forPlayerX;
    private Vector3 rotation;
    //ë“ã@
    private bool isTurn = false;
    private int moveCount = 0;
    //ìÀêi
    private bool isAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animState = anim.GetCurrentAnimatorStateInfo(0);
        transform.position = new Vector3(transform.position.x, flyHeight, 0);
        //playerÇ∆ÇÃãóó£ÇåvéZ
        forPlayerX = MathF.Abs(player.transform.position.x - transform.position.x);

        if (myState == enemyState.idle)
        {
            Vector3 pos = transform.position;

            moveCount++;
            if (moveCount > maxMoveCount)
            {
                isTurn = !isTurn;
                moveCount = 0;
            }

            if (isTurn)
            {
                pos.x += moveSpeed;
                rotation = new Vector3(0, 90, 0);
                transform.rotation = Quaternion.Euler(rotation);
            }
            else
            {
                pos.x -= moveSpeed;
                rotation = new Vector3(0, -90, 0);
                transform.rotation = Quaternion.Euler(rotation);
            }

            transform.position = pos;

            //çıìGÇ…à¯Ç¡Ç©Ç©Ç¡ÇΩÇÁ
            if (forPlayerX <= 1.0f)
            {
                isAttack = true;
                myState = enemyState.fall;
                anim.SetBool("fall", true);
            }
        }
        else if (myState == enemyState.fall)
        {
            if (isAttack)
            {
                Vector3 pos = transform.position;
                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.y, rot.x, rot.z);
                Instantiate(fallEnemy, pos, Quaternion.Euler(rot));

                isAttack = false;
            }
            else
            {
                if (animState.normalizedTime >= 1.0f)
                {
                    anim.SetBool("fall", false);
                    myState = enemyState.idle;
                }
            }
        }
    }
}
