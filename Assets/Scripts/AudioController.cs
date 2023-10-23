using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip BMNormalAudioClip;
    [SerializeField] private AudioClip BMGhostScared;
    [SerializeField] private AudioClip BMDead;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // After the intro background music play the background music for the normal state ghosts.
        if (!audioSource.isPlaying)
        {
            audioSource.clip = BMNormalAudioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetBMToGhostScared()
    {
        // Check if the audio source is not playing the same clip.
        if (audioSource.clip != BMGhostScared)
        {
            audioSource.clip = BMGhostScared;
            audioSource.Play();
        }
    }

    public void SetBMToNormal()
    {
        // Check if the audio source is not playing the same clip.
        if (audioSource.clip != BMNormalAudioClip)
        {
            audioSource.clip = BMNormalAudioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void SetBMToDead()
    {
        // Check if the audio source is not playing the same clip.
        if (audioSource.clip != BMDead)
        {
            audioSource.clip = BMDead;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
