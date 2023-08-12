using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    [SerializeField] private AudioSource carAudio;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;
    
    private Rigidbody carRb;
    private float currentSpeed;
    private float pitchFromCar;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        EngineSound();
    }

    void EngineSound()
    {
        currentSpeed = carRb.velocity.magnitude;
        pitchFromCar = carRb.velocity.magnitude / 50f;
        
        if(currentSpeed < minSpeed)
        {
            carAudio.pitch = minPitch;
        }
        if(currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }
        if(currentSpeed > maxSpeed)
        {
            carAudio.pitch = maxPitch;
        }
    }
}
