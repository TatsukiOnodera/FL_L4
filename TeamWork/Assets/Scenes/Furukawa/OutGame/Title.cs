using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // チェンジフラグ
    bool isChange = false;

    // SE
    [SerializeField] private AudioClip sound1;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(sound1);
            isChange = true;
        }
    }

    public bool GetIsChange()
    {
        return isChange;
    }
}
