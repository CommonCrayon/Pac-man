using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip BMNormalAudioClip;
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
}
