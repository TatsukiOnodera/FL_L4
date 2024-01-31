using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot_SC : MonoBehaviour
{
    public GameObject bullet;

    Quaternion rot;

    private Animator anim = null;
    private AnimatorStateInfo state;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        state = anim.GetCurrentAnimatorStateInfo(0);

        if (state.normalizedTime > 1.0f)
        {
            anim.SetBool("shot", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.y, rot.x, rot.z);
            Instantiate(bullet, pos, Quaternion.Euler(rot));
            anim.SetBool("shot", true);
        }
    }
}
