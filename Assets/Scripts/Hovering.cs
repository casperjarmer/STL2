using UnityEngine;

public class Hovering : MonoBehaviour
{
    private float floatAmplitude = 1.3f; // The height of the floating motion
    private float floatFrequency = 1.6f; // The speed of the floating motion

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        floatAmplitude = Random.Range(1.1f, 1.5f);
        floatFrequency = Random.Range(1.4f, 1.8f);
        // Store the initial position of the GameObject
        startPosition = transform.position + new Vector3(0f, (transform.localScale.y + floatAmplitude)/2+0.5f, 0f);
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        // Calculate the new Y position using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // Update the position of the GameObject
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
