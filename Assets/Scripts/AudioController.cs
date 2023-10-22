using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip BMNormalAudioClip;
    [SerializeField] private AudioClip BMGhostScared;
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
        audioSource.clip = BMGhostScared;
        audioSource.Play();
    }

    public void SetBMToNormal()
    {
        audioSource.clip = BMNormalAudioClip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
