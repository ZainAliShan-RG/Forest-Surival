using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonControls : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName: "GamePlay");
    }

    public void PlayButtonSound()
    {
        audioSource.PlayOneShot(audioClip);
    }
    
}
