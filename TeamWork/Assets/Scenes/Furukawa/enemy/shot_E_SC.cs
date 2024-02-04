using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_E_SC : MonoBehaviour
{
    public GameObject bullet;

    private GameObject player;
    private Animator anim;
    private AnimatorStateInfo animState;
    private Vector3 rotation;
    private bool isShot = false;
    int count = 0;
    const int shotTiming = 800;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animState = anim.GetCurrentAnimatorStateInfo(0);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        if (player.transform.position.x > transform.position.x)
        {
            rotation = new Vector3(0, -90, 0);
        }
        if (player.transform.position.x < transform.position.x)
        {
            rotation = new Vector3(0, 90, 0);
        }
        transform.rotation = Quaternion.Euler(rotation);

        //発射間隔管理
        if (count >= shotTiming)
        {
            anim.SetBool("shot", true);
        }
        else
        {
            count++;
        }

        if (animState.IsName("アーマチュア|shot") && animState.normalizedTime >= 0.5f && isShot == false)
        {
            isShot = true;
            shot();
        }
        if (animState.IsName("アーマチュア|shot") && animState.normalizedTime >= 1.0f)
        {
            anim.SetBool("shot", false);
            count = 0;
            isShot = false;
        }
    }

    private void shot()
    {
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x, pos.y + 2.0f, pos.z);

        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.y, rot.x, rot.z);

        Instantiate(bullet, pos, Quaternion.Euler(rot));
        var bulletobject = bullet.GetComponent<bullet_E_SC>();

        Vector3 direction = player.transform.position - pos;

        bulletobject.setvec(direction.normalized);
    }
}
