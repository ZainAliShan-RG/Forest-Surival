using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectController : MonoBehaviour
{
    //Singleton Instance
    public static SoundEffectController Instance;
    
    // ******** Audio Source - GamePlay Music ********
    [SerializeField] private AudioSource gameplayMusicAudioSource;
    
    // ******** Audio Source - Sound Effects *********
    [SerializeField] private AudioSource soundEffectAudioSource;
    
    // ******** Sound Fields for Bird's Death ********
    [SerializeField] private AudioClip birdDeathAudioClip;
    
    // ******** Sound Fields for Bird's Death ********
    /*[SerializeField] private AudioClip fireBallAudioClip;*/
    
    // ******** Sound Fields for Healing Player ******



    #region Singleton

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    #endregion
    
    public void PlayBirdDeathSound()
    {
        Debug.Log("PLaying Death Effect");
        soundEffectAudioSource.PlayOneShot(birdDeathAudioClip);
    }

    /*
    public void PlayFireBallSound()
    {
        Debug.Log("PLaying Fireball Effect");
        soundEffectAudioSource.PlayOneShot(fireBallAudioClip);
    }
    */
    
    public void Start()
    {
        gameplayMusicAudioSource.playOnAwake = true;
        gameplayMusicAudioSource.loop = true;
        gameplayMusicAudioSource.Play();
    }
}
