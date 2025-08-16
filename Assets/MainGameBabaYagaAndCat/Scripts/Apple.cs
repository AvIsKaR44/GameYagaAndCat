using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Apple : MonoBehaviour
{
    [SerializeField] private ParticleSystem wowEffect;
    [SerializeField] private GameObject apple;

    [SerializeField] private AudioClip appleSFX;

    public GameObject appleBox = null;
    public AudioSource audioSource;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && appleBox == null)
        {
            apple.SetActive(false);
            if (!wowEffect.isPlaying) wowEffect.Play();
            if (!audioSource.isPlaying) audioSource.PlayOneShot(appleSFX, .25f);
            appleBox = transform.parent.gameObject;
        }
    }

    private void Update()
    {
        if (appleBox != null && !wowEffect.isPlaying && !audioSource.isPlaying)
        {
            Destroy(appleBox);
        }
    }
}
