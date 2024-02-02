using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall_E_SC : MonoBehaviour
{
    public enum enemyState
    {
        idle, //�ҋ@
        fall, //�G����
    };

    public int maxMoveCount = 200;
    public float moveSpeed = 0.01f;
    public float flyHeight = 10.0f;
    public GameObject fallEnemy = null;

    private enemyState myState = enemyState.idle;
    private GameObject player = null;
    private float forPlayerX;
    //�ҋ@
    private bool isTurn = false;
    private int moveCount = 0;
    //�ːi
    private bool isAttack = false;
    private Vector3 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, flyHeight, 0);
        //player�Ƃ̋������v�Z
        forPlayerX = player.transform.position.x - transform.position.x;

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
            }
            else
            {
                pos.x -= moveSpeed;
            }

            transform.position = pos;

            //���G�Ɉ�������������
            if (forPlayerX <= 10.0f)
            {
                myState = enemyState.fall;
                goalPos = player.transform.position;
            }
        }
        else if (myState == enemyState.fall)
        {
            if (isAttack)
            {
                if (transform.position != goalPos)
                {

                }
                else
                {
                    isAttack = false;
                }
            }
            else
            {
                myState = enemyState.idle;
            }
        }
    }
}
