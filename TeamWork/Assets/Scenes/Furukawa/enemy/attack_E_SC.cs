using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boss_01_CS;

public class attack_E_SC : MonoBehaviour
{
    public enum enemyState
    {
        idle, //待機
        Attack, //発見
    };

    public int maxMoveCount = 200;
    public float moveSpeed = 0.01f;

    private enemyState myState = enemyState.idle;
    private GameObject player = null;
    private Rigidbody rbody;
    private Vector3 forPlayer;
    //待機
    private bool isTurn = false;
    private int moveCount = 0;
    //突進
    private bool isAttack = false;
    private Vector3 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        rbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerとの距離を計算
        forPlayer = player.transform.position - transform.position;

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

            //索敵に引っかかったら
            if (forPlayer.magnitude <= 10.0f)
            {
                myState = enemyState.Attack;
                goalPos = player.transform.position;
                rbody.AddForce(new Vector3(0, 1, 0) * 100.0f);
            }
        }
        else if (myState == enemyState.Attack)
        {
            if (isAttack)
            {
                if (transform.position != goalPos)
                {
                    transform.position += forAttackVector(goalPos);
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") && myState == enemyState.Attack)
        {
            isAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            if (other.GetComponent<player_SC>().getHP() == 0)
            {
                return;
            }
            other.GetComponent<player_SC>().damage();
        }
    }

    private Vector3 forAttackVector(Vector3 goal)
    {
        Vector3 returnVec;

        returnVec = Vector3.Normalize(goalPos - transform.position) * 0.03f;

        return returnVec;
    }
}
