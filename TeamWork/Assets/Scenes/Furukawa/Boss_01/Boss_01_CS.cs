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
        Idle, //�ҋ@
        Shot, //�ˌ�
        Attack, //�ߐ�
    };

    public struct shotManager
    {
        int shotCount; //���ˊԊu�J�E���g
        const int shotTiming = 200; //���ˊԊu 
    }

    //public Animator animator; //Animator�R���|�[�l���g
    public GameObject bullet; //�e
    private GameObject player; //player�̏��
    private Vector3 forPlayer; //player�܂ł̋���
    private bossState state; //���
    private int nextStateCount; //���̏�ԂɈڂ�܂ł̎���
    private bool isMoation = false;
    private int shotTime = 0;
    private int shotCount = 0;
    private int nextShotCount = 2000;


    // Start is called before the first frame update
    void Start()
    {
        //�L�������f����Animator�R���|�[�l���g��animator���֘A�t����
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

        attack();

        shot();
    }

    public void setState()
    {
        if (isMoation)
        {
            return;
        }

        //�ߐ�
        if (forPlayer.magnitude <= 5)
        {
            isMoation = true;
            if (GetState() == bossState.Attack)
            {
                return;
            }
            state = bossState.Attack;
        }
        //�ˌ�
        else if (forPlayer.magnitude <= 200)
        {
            isMoation = true;
            if (GetState() == bossState.Shot)
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

    //�v���C���[�܂ł̋���
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

        //��p�A�j���[�V����
    }

    public void shot()
    {
        if (state != bossState.Shot || !isMoation)
        {
            return;
        }

        //��p�A�j���[�V����

        //�e����
        nextShotCount++;
        if (nextShotCount < 2000)
        {
            return;
        }

        shotTime++;

        if (shotTime < 100)
        {
            return;
        }

        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);
        Instantiate(bullet, pos, Quaternion.Euler(rot));

        var bulletobject = bullet.GetComponent<bullet_E_CS>();

        Vector3 direction = player.transform.position - transform.position;

        bulletobject.setvec(direction.normalized);

        shotTime = 0;
        shotCount++;

        if (shotCount > 10)
        {
            shotCount = 0;
            nextShotCount = 0;
            isMoation = false;
        }
    }
}
