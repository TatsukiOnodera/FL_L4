using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // �J�[�\��
    [SerializeField] private RectTransform selectCursor;

    // �J�[�\����Y���W
    public float textY;

    // Start is called before the first frame update
    void Start()
    {
        textY = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (textY == 25)
            {
                SceneManager.LoadScene("stage1");
            }
            else if (textY == -50)
            {
                
            }
            else if (textY == -125)
            {
                
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            textY += 75;

            if (textY > 25)
            {
                textY = -125;
            }

            selectCursor.anchoredPosition = new Vector2(-100, textY);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            textY -= 75;

            if (textY < -125)
            {
                textY = 25;
            }

            selectCursor.anchoredPosition = new Vector2(-100, textY);
        }
    }
}
