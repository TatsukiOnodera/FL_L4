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

    //public Animator animator; //Animatorコンポーネント
    public GameObject bullet; //弾

    private int maxIdleAnimationCount = 2;
    private GameObject player; //playerの情報
    private Vector3 forPlayer; //playerまでの距離
    private Vector3 rotation;
    private bossState state; //状態
    private bool isMoation = false;
    private int shotTime = 0;
    private int idleAnimationCount = 0;
    private CapsuleCollider bossCollider;
    private Vector3 baceColliderPos;
    private Vector3 setColliderPos;

    private Animator anim = null;
    private AnimatorStateInfo animState;


    // Start is called before the first frame update
    void Start()
    {
        //キャラモデルのAnimatorコンポーネントとanimatorを関連付ける
        anim = GetComponent<Animator>();

        bossCollider = GetComponent<CapsuleCollider>();

        setColliderPos = new Vector3(0, 0, 1.0f);

        state = bossState.Idle;
        rotation = new Vector3(0, -90, 0);
        transform.rotation = Quaternion.Euler(rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        //プレイヤーとの距離
        setForPlayer();

        animState = anim.GetCurrentAnimatorStateInfo(0);

        //死亡処理
        if (anim.GetBool("death"))
        {
            isMoation = false;

            if (animState.normalizedTime >= 1.3f)
            {
                Destroy(this.gameObject);
            }

            return;
        }

        if (animState.IsName("アーマチュア|idol") && animState.normalizedTime >= 1.0f && !isMoation)
        {
            idleAnimationCount++;
        }

        if (idleAnimationCount < maxIdleAnimationCount && !isMoation)
        {
            return;
        }

        //行動セット
        setState();

        //近接
        attack();

        //射撃
        shot();
    }

    public void setState()
    {
        if (isMoation)
        {
            return;
        }

        //近接
        if (forPlayer.magnitude <= 7)
        {
            isMoation = true;
            if (GetState() == bossState.Attack)
            {
                return;
            }
            state = bossState.Attack;
            baceColliderPos = bossCollider.center;
            bossCollider.center = setColliderPos;
            anim.SetBool("attack", true);
            idleAnimationCount = 0;
        }
        //射撃
        else if (forPlayer.magnitude <= 200)
        {
            isMoation = true;
            if (GetState() == bossState.Shot)
            {
                return;
            }
            state = bossState.Shot;
            anim.SetBool("shot", true);
            idleAnimationCount = 0;
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

        if (animState.normalizedTime >= 1.0f)
        {
            bossCollider.center = baceColliderPos;
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("attack", false);
        }
    }

    public void shot()
    {
        if (state != bossState.Shot || !isMoation)
        {
            return;
        }

        //専用アニメーション

        //弾生成
        shotTime++;

        if (shotTime < 200)
        {
            return;
        }

        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);
        Instantiate(bullet, pos, Quaternion.Euler(rot));

        var bulletobject = bullet.GetComponent<bullet_E_SC>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);

        shotTime = 0;

        if (animState.normalizedTime >= 1.0f)
        {
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("shot", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (state != bossState.Attack)
        {
            return;
        }

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
