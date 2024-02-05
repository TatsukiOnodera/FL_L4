using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_02_Bullet_SC : MonoBehaviour
{
    public enum bulletState
    {
        ready,  //Žw’èˆÊ’u‚ÖˆÚ“®
        attack  //UŒ‚
    };

    private GameObject player;

    int count = 0;
    float speed = 0.009f;
    public Vector3 vec = Vector3.zero;
    public Vector3 attackPos;
    bulletState state = bulletState.ready;

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
        if(state == bulletState.ready)
        {
            if (transform.position != attackPos)
            {
                forAttackVector(attackPos);
            }
            else
            {
                state = bulletState.attack;

            }
        }
        else
        {
            forAttackVector(player.transform.position);

            count++;

            if (count >= 2000)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void setAttack(Vector3 vec, Vector3 attackPos)
    {
        this.vec = vec;
        this.attackPos = attackPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "player")
        {
            Destroy(this.gameObject);
            if (other.GetComponent<player_SC>().getHP() == 0)
            {
                return;
            }
            other.GetComponent<player_SC>().damage();
        }
    }

    public static bool isBetweenF(float target, float a, float b)
    {
        if (a > b)
        {
            return target <= a && target >= b;
        }
        return target <= b && target >= a;
    }

    private void forAttackVector(Vector3 goal)
    {
        Vector3 returnVec;

        returnVec = Vector3.Normalize(goal - transform.position) * 0.02f;

        transform.position += returnVec;

        if (isBetweenF(transform.position.x, goal.x - 0.02f, goal.x + 0.02f))
        {
            transform.position = new Vector3(goal.x, transform.position.y, transform.position.z);
            return;
        }

        if (isBetweenF(transform.position.y, goal.y - 0.02f, goal.y + 0.02f))
        {
            transform.position = new Vector3(transform.position.x, goal.y, transform.position.z);
            return;
        }

        return;
    }
}
