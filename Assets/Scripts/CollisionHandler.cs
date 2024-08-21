using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 3f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;

    AudioSource audioSource;
    Movement movement;

    bool isActive = true;


    void Start() {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.L)) {
            LoadNextLevel();
        }
        if (Input.GetKeyUp(KeyCode.C)) {
            isActive = !isActive;
        }

    }

    void OnCollisionEnter(Collision collision) {
        if (!isActive) { return; }


        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void LoadNextLevel() {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex == SceneManager.sceneCountInBuildSettings) {
            nextLevelIndex = 0;
        }

        SceneManager.LoadScene(nextLevelIndex);
    }

    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void StartCrashSequence() {
        movement.enabled = false;
        isActive = false;
        audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        Invoke("ReloadLevel", reloadDelay);
    }

    void StartSuccessSequence() {
        movement.enabled = false;
        isActive = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke("LoadNextLevel", reloadDelay);
    }
}
