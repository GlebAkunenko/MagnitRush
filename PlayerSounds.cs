using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    private float pitchChangeCoefficient = 1;
    [SerializeField]
    private AudioClip[] sounds;

    private new AudioSource audio;

    private float GetNewPitch(float speedUp)
    {
        float sp = speedUp;
        float k = pitchChangeCoefficient;
        return (sp - 1) * k + 1;
    }

    public float Speed
    {
        get => audio.pitch;
        set => audio.pitch = GetNewPitch(value);
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private AudioClip GetRandomSound() => sounds[Random.Range(0, sounds.Length)];

    public void Play()
    {
        audio.PlayOneShot(GetRandomSound());
    }

}