using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField, Tooltip("Direction and distance of movement of the object")]
    private Vector3 movementVector;

    [SerializeField, Tooltip("Oscillation rate (units per second)")]
    private float speed;
    
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float movementFactor;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    private void Update()
    {
        if (speed <= 0) return;
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
