using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Boss_03_CS : MonoBehaviour
{
    public enum bossState
    {
        Idle, //待機
        spike, //木根(下)
        ground, //木根(横)
        shield  //盾
    };

    public GameObject plantObject;
    public GameObject subPlantObject;
    public GameObject shieldObject;
    public Vector3 summonPlantPos;//木根を召喚する位置

    private GameObject player; //playerの情報
    private int maxIdleAnimationCount = 3;
    private Vector3 rotation;
    private bossState state = bossState.Idle; //状態
    private int stateNum;
    private bool isMoation = false;
    private int shotTime = 0;
    private int idleAnimationCount = 0;
    private CapsuleCollider bossCollider;
    private Vector3 baceColliderPos;
    private Vector3 setColliderPos;

    //アニメーション管理(Animatorのフラグのオンオフ)
    private Animator anim = null;

    //アニメーターの状態を取得(今やってるアニメーションの名前とかどんくらい進んだか等)
    private AnimatorStateInfo animState;

    //アニメーションの時間(1～0)
    //ループアニメーションだと1以上になっちゃうのでこいつに代入しなおす
    private float idleAnimNormlizedTime = 0;

    //木根関係
    private bool isPlant = false;
    private bool isPlantObjectInstantiate = false;

    //盾関係
    private int shieldTime = 0;
    private int maxShieldTime = 1000;
    private bool isShieldObjectInstantiate = false;

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

        animState = anim.GetCurrentAnimatorStateInfo(0);

        //死亡処理
        if (anim.GetBool("death"))
        {
            anim.SetBool("spike", false);
            anim.SetBool("shield", false);
            anim.SetBool("shield_a", false);
            anim.SetBool("ground", false);
            anim.SetBool("ground_a", false);
            isMoation = false;

            if (animState.normalizedTime >= 1.5f)
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
            idleAnimationCount++;
        }

        if (idleAnimationCount < maxIdleAnimationCount && !isMoation)
        {
            return;
        }

        setState();

        spike();

        ground();

        shield();
    }

    private void setState()
    {
        if (isMoation)
        {
            return;
        }

        stateNum = Random.Range(0, 100);

        if (isBetweenI(stateNum, 0, 40))
        {
            //木根(下)
            isMoation = true;
            if (state == bossState.spike)
            {
                return;
            }
            state = bossState.spike;
            isPlant = false;
            anim.SetBool("spike", true);
            idleAnimationCount = 0;
        }
        else if (isBetweenI(stateNum, 41, 80))
        {
            //木根(横)
            isMoation = true;
            if (state == bossState.ground)
            {
                return;
            }
            state = bossState.ground;
            isPlant = false;
            anim.SetBool("ground", true);
            idleAnimationCount = 0;
        }
        else
        {
            //盾
            isMoation = true;
            if (state == bossState.shield)
            {
                return;
            }
            state = bossState.shield;
            anim.SetBool("shield", true);
            idleAnimationCount = 0;
            shieldTime = 0;
        }
    }

    private void spike()
    {
        if (state != bossState.spike || !isMoation)
        {
            return;
        }

        if (animState.IsName("metarig|attack_1") && animState.normalizedTime > 0.8f && !isPlantObjectInstantiate)
        {
            //弾生成
            Vector3 pos = player.transform.position;
            Vector3 rot;

            pos = new Vector3(pos.x, pos.y - 7, 0);
            rot = new Vector3(0, 0, 0);

            Instantiate(plantObject, pos, Quaternion.Euler(rot));
            var bulletobject = plantObject.GetComponent<attackTree_CS>();
            bulletobject.setvec(new Vector3(0, 1, 0));

            if (player.transform.rotation.y > 0)
            {
                pos = new Vector3(pos.x + 1, pos.y - 7, 0);
            }
            else
            {
                pos = new Vector3(pos.x - 1, pos.y - 7, 0);
            }

            rot = new Vector3(0, 0, 0);
            Instantiate(subPlantObject, pos, Quaternion.Euler(rot));
            bulletobject = subPlantObject.GetComponent<attackTree_CS>();
            bulletobject.setvec(new Vector3(0, 1, 0));

            isPlantObjectInstantiate = true;
        }

        if (animState.IsName("metarig|attack_1") && animState.normalizedTime >= 1.0f)
        {
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("spike", false);
            isPlantObjectInstantiate = false;
            idleAnimationCount = 0;
        }
    }

    private void ground()
    {
        if (state != bossState.ground || !isMoation)
        {
            return;
        }

        if (animState.IsName("metarig|attack_2_befor") && animState.normalizedTime >= 1.0f && !isPlantObjectInstantiate)
        {
            //弾生成
            Vector3 pos;
            Vector3 rot;
            Vector3 size;

            pos = new Vector3(32, -0.5f, 0);
            rot = new Vector3(0, 0, 90);
            size = new Vector3(2.0f, 2.0f, 2.0f);

            Instantiate(plantObject, pos, Quaternion.Euler(rot));
            plantObject.transform.localScale = size;
            var bulletobject = plantObject.GetComponent<attackTree_CS>();
            bulletobject.setvec(new Vector3(-1, 0, 0));

            isPlantObjectInstantiate = true;
            anim.SetBool("ground_a", true);
        }

        if (animState.IsName("metarig|attack_2_after") && animState.normalizedTime >= 1.0f)
        {
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("ground", false);
            anim.SetBool("ground_a", false);
            isPlantObjectInstantiate = false;
            idleAnimationCount = 0;
        }
    }

    private void shield()
    {
        if (state != bossState.shield || !isMoation)
        {
            return;
        }

        if (animState.IsName("metarig|guard_befor") && animState.normalizedTime >= 0.7f && !isShieldObjectInstantiate)
        {
            //盾生成
            Vector3 pos = transform.position;
            Vector3 rot;

            pos = new Vector3(pos.x - 3.3f, pos.y + 4, 0);
            rot = new Vector3(0, 0, 90);

            Instantiate(shieldObject, pos, Quaternion.Euler(rot));

            isShieldObjectInstantiate = true;
            shieldTime = 0;
        }

        if(!isShieldObjectInstantiate)
        {
            return;
        }

        if (shieldTime >= maxShieldTime)
        {
            anim.SetBool("shield_a", true);
        }
        else
        {
            shieldTime++;
        }

        if(animState.IsName("metarig|guard_after") && animState.normalizedTime >= 1.0f)
        {
            shieldTime = 0;
            isMoation = false;
            state = bossState.Idle;
            anim.SetBool("shield", false);
            anim.SetBool("shield_a", false);
            isShieldObjectInstantiate = false;
            idleAnimationCount = 0;
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
}
