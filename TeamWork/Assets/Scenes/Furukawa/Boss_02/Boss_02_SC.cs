using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_02_SC : MonoBehaviour
{
    public enum bossState
    {
        Idle, //ë“ã@
        Shot, //éÀåÇ
        Attack, //ãﬂê⁄
        summon  //ìGè¢ä´
    };

    public GameObject bossBullet; //íe
    public GameObject summonEnemy;//ìG
    public GameObject slashObject;
    public Vector3 slashPos;//è’åÇîgÇèoÇ∑à íu

    private Vector3 centerPos;
    private int maxIdleAnimationCount = 3;
    private GameObject player; //playerÇÃèÓïÒ
    private Vector3 forPlayer; //playerÇ‹Ç≈ÇÃãóó£
    private Vector3 rotation;
    private bossState state; //èÛë‘
    private int stateNum;
    private bool isMoation = false;
    private int shotTime = 0;
    private int idleAnimationCount = 0;
    private CapsuleCollider bossCollider;
    private Vector3 baceColliderPos;
    private Vector3 setColliderPos;

    private Animator anim = null;
    private AnimatorStateInfo animState;
    private float idleAnimNormlizedTime = 0;

    //è’åÇîgä÷åW
    private bool isSlash = false;
    private bool isSlashObjectInstantiate = false;

    //éÀåÇä÷åW
    private bool isBulletObjectInstantiate = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        bossCollider = GetComponent<CapsuleCollider>();

        setColliderPos = new Vector3(0, 0, 1.0f);
        centerPos = new Vector3(Camera.main.transform.position.x, 3, 0);

        state = bossState.Idle;
        rotation = new Vector3(0, -90, 0);
        transform.rotation = Quaternion.Euler(rotation);
        transform.position = centerPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        //ÉvÉåÉCÉÑÅ[Ç∆ÇÃãóó£
        setForPlayer();

        animState = anim.GetCurrentAnimatorStateInfo(0);

        //éÄñSèàóù
        if (anim.GetBool("death"))
        {
            isMoation = false;

            if (animState.normalizedTime >= 1.3f)
            {
                Destroy(this.gameObject);
            }

            return;
        }

        if (animState.IsName("metarig|idol"))
        {
            if (animState.normalizedTime > 1.0f)
            {
                idleAnimNormlizedTime = animState.normalizedTime - 1.0f;
            }
            else
            {
                idleAnimNormlizedTime = animState.normalizedTime;
            }
        }

        if (animState.IsName("metarig|idol") && idleAnimNormlizedTime >= 1.0f && !isMoation)
        {
            if (transform.position == centerPos)
            {
                idleAnimationCount++;
            }
        }

        if (idleAnimationCount < maxIdleAnimationCount && !isMoation)
        {
            if (transform.position != centerPos)
            {
                forAttackVector(centerPos);
            }
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            return;
        }

        setState();

        summon();

        shot();

        attack();
    }
    public void setForPlayer()
    {
        forPlayer = player.transform.position - transform.position;
    }

    private void setState()
    {
        if (isMoation)
        {
            return;
        }

        //stateNum = Random.Range(0, 100);
        stateNum = 40;

        if (isBetweenI(stateNum, 0, 20))
        {
            //éGãõìGè¢ä´
            isMoation = true;
            if (state == bossState.summon)
            {
                return;
            }
            state = bossState.summon;
            anim.SetBool("summon", true);
            idleAnimationCount = 0;
        }
        else if (isBetweenI(stateNum, 21, 60))
        {
            //âHîÚÇŒÇµ
            isMoation = true;
            if (state == bossState.Shot)
            {
                return;
            }
            state = bossState.Shot;
            anim.SetBool("shot", true);
            idleAnimationCount = 0;
        }
        else
        {
            //è’åÇîg
            isMoation = true;
            if (state == bossState.Attack)
            {
                return;
            }
            state = bossState.Attack;
            isSlash = false;
            idleAnimationCount = 0;
        }
    }

    //è’åÇîg
    private void attack()
    {
        if (state != bossState.Attack || !isMoation)
        {
            return;
        }

        if (isSlash)
        {
            //ç∂Çå¸Ç≠
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

            if (animState.normalizedTime > 1.0f)
            {
                isSlash = false;
                state = bossState.Idle;
                anim.SetBool("slash", false);
                isMoation = false;
                idleAnimationCount = 0;
                isSlashObjectInstantiate = false;
            }
            else if (animState.normalizedTime > 0.8f && !isSlashObjectInstantiate)
            {
                //è’åÇîgê∂ê¨
                Vector3 pos = transform.position;
                pos = new Vector3(pos.x, pos.y + 0.5f, pos.z);
                Vector3 rot = new Vector3(0, 0, 0);
                Instantiate(slashObject, pos, Quaternion.Euler(rot));

                var bulletobject = slashObject.GetComponent<bullet_E_SC>();
                Vector3 direction = new Vector3(-1, 0, 0);
                bulletobject.setvec(direction.normalized);
                isSlashObjectInstantiate = true;
            }
        }
        else
        {
            if (transform.position != slashPos)
            {
                forAttackVector(slashPos);
            }
            else
            {
                isSlash = true;
                anim.SetBool("slash", true);
            }
        }
    }

    //éÀåÇ
    private void shot()
    {
        if (state != bossState.Shot || !isMoation)
        {
            return;
        }

        if (animState.IsName("metarig|Action") && animState.normalizedTime > 0.8f && !isBulletObjectInstantiate)
        {
            //íeê∂ê¨
            for (int i = 0; i < 6; i++)
            {
                Vector3 pos = transform.position;
                Vector3 rot = transform.rotation.eulerAngles;
                rot = new Vector3(rot.y, rot.x, rot.z);

                Instantiate(bossBullet, pos, Quaternion.Euler(rot));
                var bulletobject = bossBullet.GetComponent<Boss_02_Bullet_SC>();

                Vector3 direction = player.transform.position - transform.position;

                bulletobject.setAttack(direction.normalized, new Vector3(i * 3, 3, 0));
            }
            isBulletObjectInstantiate = true;
        }

        if (animState.IsName("metarig|Action") && animState.normalizedTime >= 1.0f)
        {
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("shot", false);
            isBulletObjectInstantiate = false;
            idleAnimationCount = 0;
        }
    }

    private void summon()
    {
        if (state != bossState.summon || !isMoation)
        {
            return;
        }
    }

    public static bool isBetweenI(int target, int a, int b)
    {
        if (a > b)
        {
            return target <= a && target >= b;
        }
        return target <= b && target >= a;
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
