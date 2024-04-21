using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSound : MonoBehaviour
{
    AudioSource audioSource; 
    string _sceneName; 
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/MainSound");
        audioSource.Play();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (_sceneName != SceneManager.GetActiveScene().name)
        {
            _sceneName = SceneManager.GetActiveScene().name;
            ChangeBackgroundMusic();
        }
    }

    void ChangeBackgroundMusic()
    {
        if (_sceneName == "GamePlay")
        {
            audioSource.clip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/GameSceneSound"); 
            audioSource.Play();
        }
        else if (_sceneName == "GameResultScene")
        {
            audioSource.clip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/ResultSound");
            audioSource.Play();
        }
        else if (_sceneName == "LoginScene")
        {
            audioSource.clip = Managers.Resources.Load<AudioClip>("Prefabs/sh/Sound/MainSound");
            audioSource.Play();
        }
    }
}
