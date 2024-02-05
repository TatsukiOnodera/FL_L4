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

    private Animator anim;
    private AnimatorStateInfo animState;
    private enemyState myState = enemyState.idle;
    private GameObject player = null;
    private Rigidbody rbody;
    private Vector3 forPlayer;
    private Vector3 rotation;
    //待機
    private bool isTurn = false;
    private int moveCount = 0;
    //突進
    private bool isAttack = false;
    private bool isFind = false;
    private Vector3 goalPos;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        rbody = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animState = anim.GetCurrentAnimatorStateInfo(0);

        //playerとの距離を計算
        forPlayer = player.transform.position - transform.position;

        //発見まで
        idle();

        //突進
        attack();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor") && myState == enemyState.Attack)
        {
            isAttack = true;
            anim.SetBool("attack", true);
            anim.SetBool("find", false);
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

        returnVec = Vector3.Normalize(goalPos - transform.position) * 0.02f;

        return returnVec;
    }

    private void idle()
    {
        if (myState != enemyState.idle)
        {
            return;
        }

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

        //索敵に引っかかったら
        if (forPlayer.magnitude <= 10.0f)
        {
            isFind = true;
            isAttack = false;
            anim.SetBool("find", true);
            anim.SetBool("attack", false);
            myState = enemyState.Attack;
            return;
        }
    }

    private void attack()
    {
        if (myState != enemyState.Attack)
        {
            return;
        }

        if (transform.position.x > player.transform.position.x)
        {
            rotation = new Vector3(0, -90, 0);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (transform.position.x < player.transform.position.x)
        {
            rotation = new Vector3(0, 90, 0);
            transform.rotation = Quaternion.Euler(rotation);
        }

        if (isAttack)
        {
            if (animState.IsName("metarig|find") && animState.normalizedTime < 1.0f)
            {
                return;
            }

            if (transform.position.x >= goalPos.x - 0.4f && transform.position.x <= goalPos.x + 0.4f)
            {
                isFind = true;
                isAttack = false;
                anim.SetBool("find", true);
                anim.SetBool("attack", false);
                return;
            }
            else
            {
                transform.position += forAttackVector(goalPos);
                return;
            }
        }

        if (isFind)
        {
            if (animState.IsName("metarig|find") && animState.normalizedTime >= 1.0f)
            {
                isFind = false;
                isAttack = true;
                anim.SetBool("find", false);
                anim.SetBool("attack", true);
                goalPos = player.transform.position;
                return;
            }
        }
    }
}
