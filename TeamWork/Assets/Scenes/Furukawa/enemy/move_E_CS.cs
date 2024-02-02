using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Boss_01_CS;

public class move_E_CS : MonoBehaviour
{
    public int maxMoveCount = 200;
    public float moveSpeed = 0.01f;

    bool isTurn = false;
    int moveCount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
}
