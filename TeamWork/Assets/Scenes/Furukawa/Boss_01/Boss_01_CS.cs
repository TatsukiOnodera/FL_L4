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
    private Transform targetTransform; //�^�[�Q�b�g�̏��
    private NavMeshAgent navMeshAgent; //NavMeshAgent�R���|�[�l���g
    public Animator animator; //Animator�R���|�[�l���g
    [SerializeField]
    private PlayableDirector timeline; //PlayableDirector�R���|�[�l���g
    private Vector3 destination; //�ړI�n�̈ʒu�����i�[���邽�߂̃p�����[�^


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //�L�������f����Animator�R���|�[�l���g��animator���֘A�t����
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();

        setState(bossState.Idle); //������Ԃ�Idle��Ԃɐݒ肷��
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
            //�@�G�̌������v���C���[�̕����ɏ����Âς���
            var dir = (GetDestination() - transform.position).normalized;
            dir.y = 0;
            Quaternion setRotation = Quaternion.LookRotation(dir);
            //�@�Z�o���������̊p�x��G�̊p�x�ɐݒ�
            //transform.rotation = Quaternion.Slerp(transform.rotation, setRotation, navMeshAgent.angularSpeed * 0.1f * Time.deltaTime);
        }
    }

    public void setState(bossState tempstate, Transform target = null)
    {
        state = tempstate;

        if (tempstate == bossState.Idle)
        {
            //navMeshAgent.isStopped = true; //�L�����̈ړ����~�߂�
            //animator.SetBool("chase", false); //�A�j���[�V�����R���g���[���[�̃t���O�ؑցiChase��Idle�j
        }

        if (tempstate == bossState.Shot)
        {
            //targetTransform = targetObject; //�^�[�Q�b�g�̏����X�V
            //navMeshAgent.SetDestination(targetTransform.position); //�ړI�n���^�[�Q�b�g�̈ʒu�ɐݒ�
            //navMeshAgent.isStopped = false; //�L�����𓮂���悤�ɂ���
            //animator.SetBool("chase", true); //�A�j���[�V�����R���g���[���[�̃t���O�ؑցiIdle��Chase�j
        }

        if (tempstate == bossState.Attack)
        {
            //navMeshAgent.isStopped = true; //�L�����̈ړ����~�߂�
            //animator.SetBool("chase", false);�@//�A�j���[�V�����R���g���[���[�̃t���O�ؑցiChase��Idle�j
            //timeline.Play();//�U���p�̃^�C�����C�����Đ�����

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

    //�@�ړI�n���擾����
    public Vector3 GetDestination()
    {
        return destination;
    }
}
