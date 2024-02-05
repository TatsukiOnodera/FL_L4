using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // ポーズ画面
    [SerializeField] private GameObject m_pause;

    // カーソル
    [SerializeField] private RectTransform selectCursor;

    // カーソルのY座標
    private float textY;

    // ポーズ状態
    private bool isPose = false;

    // メニュー画面に移行
    private bool isBack = false;

    private bool pushStick;

    // SE
    [SerializeField] private AudioClip select;
    [SerializeField] private AudioClip move;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (m_pause ==null)
        {
            m_pause = GameObject.Find("Pause");
        }
        pushStick = false;
        ResetPause();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPose == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
            {
                audioSource.PlayOneShot(select);
                Time.timeScale = 0;
                isPose = true;
                m_pause.SetActive(true);
                textY = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && isBack == false)
            {
                audioSource.PlayOneShot(select);
                if (textY == 0)
                {
                    isBack = true;
                }
                else
                {
                    ResetPause();
                }
            }
            else if (CheckKey())
            {
                audioSource.PlayOneShot(move);
                if (textY == 0)
                {
                    textY = -250;
                }
                else
                {
                    textY = 0;
                }
            }
        }
            
        selectCursor.anchoredPosition = new Vector2(-400, textY);
    }

    private bool CheckKey()
    {
        if (Input.GetKeyUp(KeyCode.W) || (Input.GetAxis("L_Stick_V") < 0 && pushStick == false))
        {
            pushStick = true;
            return pushStick;
        }
        else if (Input.GetKeyUp(KeyCode.S) || (0 < Input.GetAxis("L_Stick_V") && pushStick == false))
        {
            pushStick = true;
            return pushStick;
        }
        else if (Input.GetAxis("L_Stick_V") == 0)
        {
            pushStick = false;
            return pushStick;
        }
        else
        {
            return false;
        }
    }

    public void ResetPause()
    {
        isPose = false;
        isBack = false;
        Time.timeScale = 1;
        m_pause.SetActive(false);
        textY = 0;
    }

    /// <summary>
    /// メニューに移動
    /// </summary>
    /// <returns>bool</returns>
    public bool BackMenu()
    {
        return isBack;
    }
}
