using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    // èIóπèàóù
    private bool isEnd = false;

    // SE
    [SerializeField] private AudioClip sound1;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(sound1);
            isEnd = true;
        }
    }

    public bool GetIsEnd()
    {
        return isEnd;
    }
}
