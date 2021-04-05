using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSFX, finishSFX, restartSFX;


    public void PlayCoinSFX()
    {
        audioSource.PlayOneShot(coinSFX);
    }


    public void PlayFinishSFX()
    {
        audioSource.PlayOneShot(finishSFX);
    }


    public void PlayRestartSFX()
    {
        audioSource.PlayOneShot(restartSFX);
    }
}
