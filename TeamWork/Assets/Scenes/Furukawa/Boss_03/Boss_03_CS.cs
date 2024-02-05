using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_03_CS : MonoBehaviour
{
    public enum bossState
    {
        Idle, //�ҋ@
        Shot, //�ˌ�
        Attack, //�ߐ�
    };

    public GameObject bullet; //�e

    private int maxIdleAnimationCount = 2;
    private GameObject player; //player�̏��
    private Vector3 forPlayer; //player�܂ł̋���
    private Vector3 rotation;
    private bossState state; //���
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
        //�L�������f����Animator�R���|�[�l���g��animator���֘A�t����
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

        //�v���C���[�Ƃ̋���
        setForPlayer();

        animState = anim.GetCurrentAnimatorStateInfo(0);

        //���S����
        if (anim.GetBool("death"))
        {
            isMoation = false;

            if (animState.normalizedTime >= 1.3f)
            {
                Destroy(this.gameObject);
            }

            return;
        }

        if (animState.IsName("metarig|idol") && animState.normalizedTime >= 1.0f && !isMoation)
        {
            idleAnimationCount++;
        }

        if (idleAnimationCount < maxIdleAnimationCount && !isMoation)
        {
            return;
        }
    }

    public void setForPlayer()
    {
        forPlayer = player.transform.position - transform.position;
    }
}
