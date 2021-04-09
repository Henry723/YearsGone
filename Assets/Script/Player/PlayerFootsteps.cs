using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isWalking = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartWalk()
    {
        if (!isWalking)
        {
            audioSource.loop = true;
            audioSource.Play();
        }
        isWalking = true;
    }

    public void EndWalk()
    {
        audioSource.loop = false;
        isWalking = false;
    }
}
