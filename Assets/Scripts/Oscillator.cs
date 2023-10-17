using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    Vector3 startingPosition;
    [SerializeField] float period = 2f;

    private void Start() 
    {
        startingPosition = transform.position;    
    }
    private void Update() 
    {
        if(period <= Mathf.Epsilon) { return; }

        float cycles = Time.time / period;

        const float tau = Mathf.PI / 2;
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSinWave + 1)/2;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
