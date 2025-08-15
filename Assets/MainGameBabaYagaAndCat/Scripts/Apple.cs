using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] ParticleSystem wowEffect;
    [SerializeField] GameObject apple;

    [SerializeField] AudioClip appleSFX;

    GameObject appleBox = null;
    AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !appleBox)
        {
            apple.SetActive(false);
            if (!wowEffect.isPlaying) wowEffect.Play();
            if (!audioSource.isPlaying) audioSource.PlayOneShot(appleSFX, .25f);
            appleBox = transform.parent.gameObject;
        }
    }

    void Update()
    {
        if (appleBox && !wowEffect.isPlaying && !audioSource.isPlaying)
        {
            Destroy(appleBox);
        }
    }
}
