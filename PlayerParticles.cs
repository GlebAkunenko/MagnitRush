using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bump;
    [SerializeField]
    private ParticleSystem[] moveParticles;

    public bool IsMoving
    {
        get => moveParticles[0].isPlaying;
        set
        {
            foreach(ParticleSystem ps in moveParticles) {
                if (value == true)
                    ps.Play();
                else
                    ps.Stop();
            }
        }
    }

    public void Bump()
    {
        bump.Play();
    }

    public void OnPlayerFall()
    {
        Bump();
        IsMoving = true;
    }

    public void OnPlayerJump()
    {
        IsMoving = false;
    }
}

