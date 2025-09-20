using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class Movement : MonoBehaviour, IDependency<GameStateTracker>

public class Movement : MonoBehaviour
{   
    [SerializeField, Tooltip("Action to activate the thrust")]
    private InputAction thrust;

    [SerializeField, Tooltip("Action for rotation")]
    private InputAction rotation;

    [SerializeField, Tooltip("Engine thrust force")]
    private float thrustStrength = 100f;

    [SerializeField, Tooltip("Rotation force")]
    private float rotationStrength = 100f;

    [SerializeField, Tooltip("Engine sound")]
    private AudioClip mainEngineSFX;

    [SerializeField, Tooltip("Main Engine Particle Effect")]
    private ParticleSystem mainEngineParticles;

    [SerializeField, Tooltip("Right Engine Particle Effect")]
    private ParticleSystem rightThrustParticles;

    [SerializeField, Tooltip("Left Engine Particle Effect")]
    private ParticleSystem leftThrustParticles;


    private Rigidbody rb;
    private AudioSource audioSource;

    [SerializeField, Tooltip("Engine thrust force")]
    private float thrustStrength = 100f;

    [SerializeField, Tooltip("Rotation force")]
    private float rotationStrength = 100f;

    [SerializeField, Tooltip("Engine sound")]
    private AudioClip mainEngineSFX;

    [SerializeField, Tooltip("Main Engine Particle Effect")]
    private ParticleSystem mainEngineParticles;

    [SerializeField, Tooltip("Right Engine Particle Effect")]
    private ParticleSystem rightThrustParticles;

    [SerializeField, Tooltip("Left Engine Particle Effect")]
    private ParticleSystem leftThrustParticles;

    private Rigidbody rb;
    private AudioSource audioSource;

    private GameStateTracker gameStateTracker;
    private bool controlsEnabled = false;

    public void Construct(GameStateTracker obj)
    {
        gameStateTracker = obj;
        gameStateTracker.Started += OnGameStarted;
        gameStateTracker.Completed += OnGameCompleted;
    }
      
   
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        DisableControls();

        if (gameStateTracker == null)
        {
            gameStateTracker = FindFirstObjectByType<GameStateTracker>();
            if (gameStateTracker != null)
            {
                gameStateTracker.Started += OnGameStarted;
                gameStateTracker.Completed += OnGameCompleted;
            }
        }
    }

  

    private void OnDestroy()
    {
        if (gameStateTracker != null)
        {
            gameStateTracker.Started -= OnGameStarted;
            gameStateTracker.Completed -= OnGameCompleted;
        }
    }

    private void OnGameStarted()
    {
        EnableControls();
    }
       
    private void OnGameCompleted()
    {
        DisableControls();
    }

    private void EnableControls()
    {
        controlsEnabled = true;
        thrust.Enable();
        rotation.Enable();
    }

    private void DisableControls()
    {
        controlsEnabled = false;
        thrust.Disable();
        rotation.Disable();

        StopThrusting();
        StopRotating();
    }    

    private void FixedUpdate()
    {
        if (!controlsEnabled) return;

        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSFX);
        }
        if (mainEngineParticles != null && !mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        if(Mathf.Approximately(rotationInput, 0))
        {
            StopRotating();
        }
        else if(rotationInput > 0)
        {
            RotateLeft();                        
        }
        else
        {
            RotateRight();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(rotationStrength);
        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(-rotationStrength);
        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
