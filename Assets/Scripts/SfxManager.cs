using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSfx;
    [SerializeField] private AudioClip finishSfx;
    [SerializeField] private AudioClip restartSfx;


    public void PlayCoinSfx() => audioSource.PlayOneShot(coinSfx);

    public void PlayFinishSfx() => audioSource.PlayOneShot(finishSfx);

    public void PlayRestartSfx() => audioSource.PlayOneShot(restartSfx);
}
