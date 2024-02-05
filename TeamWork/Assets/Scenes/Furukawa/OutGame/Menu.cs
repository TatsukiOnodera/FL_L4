using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // カーソル
    [SerializeField] private RectTransform selectCursor;

    // カーソルのY座標
    private float textY;

    // ステージを選んだか
    private bool isSelect = false;

    // SE
    [SerializeField] private AudioClip sound1;
    [SerializeField] private AudioClip sound2;
    private AudioSource audioSource;

    private bool pushStick;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textY = 100;
        pushStick = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1"))
        {
            audioSource.PlayOneShot(sound1);

            isSelect = true;
        }

        if (Input.GetKeyUp(KeyCode.W) || (Input.GetAxis("L_Stick_V") < 0 && pushStick == false))
        {
            pushStick = true;
            audioSource.PlayOneShot(sound2);

            textY += 200;

            if (textY > 100)
            {
                textY = -300;
            }

            selectCursor.anchoredPosition = new Vector2(-254, textY);
        }
        else if (Input.GetKeyUp(KeyCode.S) || (0 < Input.GetAxis("L_Stick_V") && pushStick == false))
        {
            pushStick = true;
            audioSource.PlayOneShot(sound2);

            textY -= 200;

            if (textY < -300)
            {
                textY = 100;
            }

            selectCursor.anchoredPosition = new Vector2(-254, textY);
        }
        else if (Input.GetAxis("L_Stick_V") == 0)
        {
            pushStick = false;
        }
    }

    public int GetStageNum()
    {
        if (isSelect == false)
        {
            return 0;
        }

        if (textY == 100)
        {
            return 1;
        }
        else if (textY == -100)
        {
            return 2;
        }
        else if (textY == -300)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}
