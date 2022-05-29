using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> musics = new List<AudioClip>();
    public List<AudioClip> sounds = new List<AudioClip>();

    private AudioSource _musicAudioSource;
    private AudioSource _soundAudioSource;

    private int _currentMusicIndex = 0;

    void Start()
    {
        _musicAudioSource = GetComponentInChildren<AudioSource>();
        _soundAudioSource = GetComponent<AudioSource>();
        PlayMusic();
    }
    private void PlayMusic()
    {
        _musicAudioSource.Stop();
        _musicAudioSource.loop = true;
        _musicAudioSource.clip = musics[_currentMusicIndex];
        _musicAudioSource.Play();
    }
    public void ChangeMusicClip()
    {
        _currentMusicIndex = (_currentMusicIndex >= musics.Count - 1) ? 0 : _currentMusicIndex + 1;
        PlayMusic();
    }
    public void MuteMusic()
    {
        _musicAudioSource.mute = !_musicAudioSource.mute;
    }
    public void PlayHitUltimateDefenderSound()
    {
        _soundAudioSource.PlayOneShot(sounds[0], 0.5f);
    }
    public void PlayHitEarthSound()
    {
        _soundAudioSource.PlayOneShot(sounds[1], 0.4f);
    }
}
