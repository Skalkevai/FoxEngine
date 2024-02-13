using Frost;
using UnityEngine;

public class SoundItem : PoolItem
{
    private AudioSource audioSource = default;

    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        UnityEngine.Debug.Assert(audioSource);
    }

    public void SetAudioClip(AudioClip _audioClip, float _volume = 1, float _pitch = 1)
    {
        audioSource.clip = _audioClip;
        audioSource.volume = _volume;
        audioSource.pitch = _pitch;
    }

    public void SetVolume(float _volume)
    {
        audioSource.volume = _volume;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }
}
