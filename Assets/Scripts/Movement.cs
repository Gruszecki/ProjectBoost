using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip engineSound;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    
    Rigidbody rb;
    AudioSource audioSource;
    Quaternion startRotation;


    void Start() {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        startRotation = transform.rotation;
    }

    void Update() {
        ProcessThrust();
        ProcessRotation();

        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKey(KeyCode.G)) {
            ResetRotation();
        }
    }

    void ProcessThrust() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            StartThrusting();
        } else {
            StopThrusting();
        }
    }
    void ProcessRotation() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            RotateRight();
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            RotateLeft();
        } else {
            StopRotating();
        }
    }

    void StartThrusting() {
        rb.AddRelativeForce(mainThrust * Time.deltaTime * Vector3.up);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(engineSound);
            mainBooster.Play();
        }
    }

    void StopThrusting() {
        audioSource.Stop();
        mainBooster.Stop();
    }

    void ApplyRotation(float rotation) {
        rb.freezeRotation = true;
        transform.Rotate(rotation * Time.deltaTime * Vector3.back);
        rb.freezeRotation = false;
    }

    void RotateRight() {
        ApplyRotation(rotationThrust);
        if (!leftBooster.isPlaying) {
            leftBooster.Play();
        }
    }

    void RotateLeft() {
        ApplyRotation(-rotationThrust);
        if (!rightBooster.isPlaying) {
            rightBooster.Play();
        }
    }

    void StopRotating() {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    void ResetRotation() {
        transform.rotation = startRotation;
    }
}