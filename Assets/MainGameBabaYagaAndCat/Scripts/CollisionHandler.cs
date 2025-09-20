using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Movement))]
public class CollisionHandler : MonoBehaviour
{   
    [SerializeField, Tooltip("Delay before loading next level")]
    private float levelLoadDelay = 2f;

    [SerializeField, Tooltip("Sound played on success")]
    private AudioClip successSFX;

    [SerializeField, Tooltip("Sound played on crash")]
    private AudioClip crashSFX;

    [SerializeField, Tooltip("Particles played on success")]
    private ParticleSystem successParticles;

    [SerializeField, Tooltip("Particles played on crash")]
    private ParticleSystem crashParticles;
    
    private AudioSource audioSource;

    private bool isControllable = true;
    private bool isCollidable = true;

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
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (!isControllable || !isCollidable)  return;
        
        if (other.gameObject.CompareTag("Finish"))
        {
            StartSuccessSequence();
        }
        else if(!other.gameObject.CompareTag("Neutral"))
        {
            StartCrashSequence();
        }
    }

    private void StartSuccessSequence()
    {
        isControllable = false;
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(successSFX);
        }
        if(successParticles != null) successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        isControllable = false;
        if (audioSource != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(crashSFX);
        }
        if(crashParticles != null) crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
