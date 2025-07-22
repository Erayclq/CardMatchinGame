using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;

    [SerializeField] private AudioSource SoundFXObject;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(SoundFXObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play(); // Play the sound.

        Destroy(audioSource.gameObject, audioClip.length);
    }
}
