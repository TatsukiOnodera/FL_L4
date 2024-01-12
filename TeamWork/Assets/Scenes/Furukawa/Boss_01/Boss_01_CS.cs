using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class Boss_01_CS : MonoBehaviour
{
    public enum bossState
    {
        Idle, //待機
        Shot, //射撃
        Attack, //近接
    };

    public struct shotManager
    {
        int shotCount; //発射間隔カウント
        const int shotTiming = 200; //発射間隔 
    }

    public Animator animator; //Animatorコンポーネント
    public GameObject bullet; //弾
    private GameObject player; //playerの情報
    private Vector3 forPlayer; //playerまでの距離
    private bossState state; //状態
    private int nextStateCount; //次の状態に移るまでの時間
    private bool isMoation = false;


    // Start is called before the first frame update
    void Start()
    {
        //キャラモデルのAnimatorコンポーネントとanimatorを関連付ける
        //animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        state = bossState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        setForPlayer();

        setState();

        shot();
    }

    public void setState()
    {
        if (isMoation)
        {
            return;
        }

        //近接
        if (forPlayer.magnitude <= 100)
        {
            if (GetState() != bossState.Attack)
            {
                return;
            }
            state = bossState.Attack;
        }
        //射撃
        else if (forPlayer.magnitude <= 200)
        {
            if (GetState() != bossState.Shot)
            {
                return;
            }
            state = bossState.Shot;
        }
        else if (forPlayer.magnitude <= 300)
        {

        }
    }

    public bossState GetState()
    {
        return state;
    }

    //プレイヤーまでの距離
    public void setForPlayer()
    {
        forPlayer = player.transform.position - transform.position;
    }

    public void attack()
    {
        if (state != bossState.Attack || !isMoation)
        {
            return;
        }

        //専用アニメーション
    }

    public void shot()
    {
        if (state != bossState.Shot || !isMoation)
        {
            return;
        }

        //専用アニメーション

        //弾生成
        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);
        Instantiate(bullet, pos, Quaternion.Euler(rot));

        var bulletobject = bullet.GetComponent<bullet_E_CS>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);
    }
}
