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
        Idle,
        Shot,
        Attack,
        Freeze
    };


    private bossState state = bossState.Idle;
    private Transform targetTransform; //ターゲットの情報
    private NavMeshAgent navMeshAgent; //NavMeshAgentコンポーネント
    public Animator animator; //Animatorコンポーネント
    [SerializeField]
    private PlayableDirector timeline; //PlayableDirectorコンポーネント
    private Vector3 destination; //目的地の位置情報を格納するためのパラメータ


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //キャラモデルのAnimatorコンポーネントとanimatorを関連付ける
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        setState(bossState.Idle); //初期状態をIdle状態に設定する
    }

    // Update is called once per frame
    void Update()
    {
        if (state == bossState.Shot)
        {
            if (targetTransform == null)
            {
                setState(bossState.Idle);
            }
            else
            {
                SetDestination(targetTransform.position);
                //navMeshAgent.SetDestination(GetDestination());
            }
            //　敵の向きをプレイヤーの方向に少しづつ変える
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //　算出した方向の角度を敵の角度に設定
            //transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    public void setState(bossState tempstate, Transform target = null)
    {
        state = tempstate;

        if (tempstate == bossState.Idle)
        {
            //navMeshAgent.isStopped = true; //キャラの移動を止める
            //animator.SetBool("chase", false); //アニメーションコントローラーのフラグ切替（Chase⇒Idle）
        }

        if (tempstate == bossState.Shot)
        {
            //targetTransform = targetObject; //ターゲットの情報を更新
            //navMeshAgent.SetDestination(targetTransform.position); //目的地をターゲットの位置に設定
            //navMeshAgent.isStopped = false; //キャラを動けるようにする
            //animator.SetBool("chase", true); //アニメーションコントローラーのフラグ切替（Idle⇒Chase）
        }

        if (tempstate == bossState.Attack)
        {
            //navMeshAgent.isStopped = true; //キャラの移動を止める
            //animator.SetBool("chase", false);　//アニメーションコントローラーのフラグ切替（Chase⇒Idle）
            //timeline.Play();//攻撃用のタイムラインを再生する

        }

        if (tempstate == bossState.Freeze)
        {
            Invoke("ResetState", 2.0f);
        }
    }
    public bossState GetState()
    {
        return state;
    }
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //　目的地を取得する
    public Vector3 GetDestination()
    {
        return destination;
    }
}
