using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [FormerlySerializedAs("coinSFX")] [SerializeField] private AudioClip coinSfx;
    [FormerlySerializedAs("finishSFX")] [SerializeField] private AudioClip finishSfx;
    [FormerlySerializedAs("restartSFX")] [SerializeField] private AudioClip restartSfx;


    public void PlayCoinSfx() => audioSource.PlayOneShot(coinSfx);

    public void PlayFinishSfx() => audioSource.PlayOneShot(finishSfx);

    public void PlayRestartSfx() => audioSource.PlayOneShot(restartSfx);
}
