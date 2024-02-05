using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shot_SC : MonoBehaviour
{
    public GameObject bullet;

    private GameObject player = null;
    Quaternion rot;

    private Animator anim = null;
    private AnimatorStateInfo state;

    // SE
    [SerializeField] private AudioClip shot_SE;
    private AudioSource audioSource;

    // 発射エフェクト
    [SerializeField] private GameObject shotEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("player");
        }

        anim = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ポーズ中は動かない
        if (Time.deltaTime == 0 || GetComponent<player_SC>().getHP() <= 0)
        {
            return;
        }

        state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.normalizedTime > 1.0f)
        {
            anim.SetBool("shot", false);
        }

        if(state.IsName("metarig|dead 0"))
        {
            return;
        }

        FeverTime fever = GetComponent<FeverTime>();
        if (fever.GetIsBig() == true) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            audioSource.PlayOneShot(shot_SE);
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.y, rot.x, rot.z);
            Instantiate(bullet, pos, Quaternion.Euler(rot));
            anim.SetBool("shot", true);
            if (transform.localEulerAngles.y == 90)
            {
                pos.x += 0.75f;
            }
            else
            {
                pos.x -= 0.75f;
            }
            Instantiate(shotEffect, pos, Quaternion.identity);
        }
    }
}
