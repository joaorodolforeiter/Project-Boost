using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private ParticleSystem collisionParticle;
    [SerializeField] private ParticleSystem successParticle;
    
    private AudioSource _audioSource;

    private bool _isTransitioning;
    private bool _collisionDisabled;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        DebugCheats();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning) return;
        if (_collisionDisabled) return;
        switch (other.gameObject.tag)
        {
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        successParticle.Play();
        _isTransitioning = true;
        _audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        _audioSource.PlayOneShot(successSound);
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void StartCrashSequence()
    {
        collisionParticle.Play();
        _isTransitioning = true;
        _audioSource.Stop();
        GetComponent<Movement>().enabled = false;
        _audioSource.PlayOneShot(collisionSound);
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void DebugCheats()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            _collisionDisabled = !_collisionDisabled;
        }
    }

}
