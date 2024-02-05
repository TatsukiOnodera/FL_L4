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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        textY = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(sound1);

            isSelect = true;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            audioSource.PlayOneShot(sound2);

            textY += 75;

            if (textY > 25)
            {
                textY = -125;
            }

            selectCursor.anchoredPosition = new Vector2(-100, textY);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            audioSource.PlayOneShot(sound2);

            textY -= 75;

            if (textY < -125)
            {
                textY = 25;
            }

            selectCursor.anchoredPosition = new Vector2(-100, textY);
        }
    }

    public int GetStageNum()
    {
        if (isSelect == false)
        {
            return 0;
        }

        if (textY == 25)
        {
            return 1;
        }
        else if (textY == -50)
        {
            return 2;
        }
        else if (textY == -125)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}
