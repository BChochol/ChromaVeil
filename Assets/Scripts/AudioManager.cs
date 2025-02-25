using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxPlayerSource;
    [SerializeField] private AudioSource _sfxEnviroSource;
    
    public static AudioManager Instance;

    public AudioClip musicMenu;
    public AudioClip musicLevel;
    public AudioClip sfxClickOn;
    public AudioClip sfxClickOff;
    public AudioClip sfxWheels;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        SwitchBgMusic(true);
    }
    
    public void PlaySFXClickOn()
    {
        _sfxEnviroSource.loop = false;  
        _sfxEnviroSource.clip = sfxClickOn;
        _sfxEnviroSource.Play();
    }
    
    public void PlaySFXClickOff()
    {
        _sfxEnviroSource.loop = false;
        _sfxEnviroSource.clip = sfxClickOff;
        _sfxEnviroSource.Play();
    }
    
    public void SwitchBgMusic(bool isMenu)
    {
        _musicSource.Stop();
        if (isMenu)
        {
            _musicSource.clip = musicMenu;
        }
        else
        {
            _musicSource.clip = musicLevel;
        }
        _musicSource.loop = true;
        _musicSource.Play();
    }
    
    public void PlaySFXWheels()
    {
        if (!_sfxPlayerSource.isPlaying || _sfxPlayerSource.clip != sfxWheels) 
        {
            Debug.Log("Playing Wheels Sound"); 
            _sfxPlayerSource.clip = sfxWheels;
            _sfxPlayerSource.loop = true;
            _sfxPlayerSource.Play();
        }
    }
    
    public void StopSFXWheels()
    {
        if (_sfxPlayerSource.isPlaying && _sfxPlayerSource.clip == sfxWheels)
        {
            Debug.Log("Stopping Wheels Sound");
            _sfxPlayerSource.Stop();
        }
    }
}
