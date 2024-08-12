using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    Rigidbody rb;
    Vector3 startPosition;
    Quaternion startRotation;


    void Start() {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();

        if (Input.GetKey(KeyCode.R)) {
            ResetRocketPosition();
        }
        if (Input.GetKey(KeyCode.G))
        {
            ResetRocketRotation();
        }
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        }
    }

    void ProcessRotation() {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(rotation * Time.deltaTime * Vector3.back);
        rb.freezeRotation = false;
    }

    void ResetRocketPosition()
    {
        transform.SetPositionAndRotation(startPosition, startRotation);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ResetRocketRotation()
    {
        transform.rotation = startRotation;
    }
}