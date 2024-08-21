using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;
    
    Vector3 startingPosition;


    private void Start() {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }

        const float tau = Mathf.PI * 2;     // constant value of 6.283
        float cycles = Time.time / period;  // continually growing over time
        float rawSinWave = Mathf.Sin(cycles * tau);     // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f;    // recalculated to go from 0 to 1 so it's cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
