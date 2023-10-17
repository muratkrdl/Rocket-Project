using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 0.5f;
    [SerializeField] AudioClip crashes;
    [SerializeField] AudioClip succes;
    [SerializeField] ParticleSystem crasheParticle;
    [SerializeField] ParticleSystem succesParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }
    private void Update() 
    {
        RespondToDebugKeys();    
    }

    private void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable) {return;}

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartLoadNextLevel();
                break;
            default:
                StartCrashSequence();
                break;
        }
        
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crasheParticle.Play();
        audioSource.PlayOneShot(crashes);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void StartLoadNextLevel()
    {
        isTransitioning = true;
        audioSource.Stop();
        succesParticle.Play();
        audioSource.PlayOneShot(succes);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1 ;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
