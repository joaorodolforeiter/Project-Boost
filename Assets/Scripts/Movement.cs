using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 100f;
    [SerializeField] private float rotationThrust = 10f;
    [SerializeField] private AudioClip engineThrust;
    [SerializeField] private ParticleSystem mainThrustParticle;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    private bool _thrusting;
    private bool _movingLeft = false;
    private bool _movingRight = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void OnBoost()
    {
        _thrusting = !_thrusting;
    }

    private void OnMoveLeft()
    {
        _movingLeft = !_movingLeft;
    }
    private void OnMoveRight()
    {
        _movingRight = !_movingRight;
    }

    private void ProcessThrust()
    {
        if (_thrusting)
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    private void ProcessRotation()
    {
        if (_movingLeft)
        {
            ApplyRotation(rotationThrust);
        }
        else if (_movingRight)
        {
            ApplyRotation(-rotationThrust);
        }
    }


    private void StartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * (mainThrust * Time.deltaTime));
        if (!mainThrustParticle.isPlaying)
        {
            mainThrustParticle.Play();
        }

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(engineThrust);
        }
    }

    private void StopThrusting()
    {
        _audioSource.Stop();
        mainThrustParticle.Stop();
    }


    private void ApplyRotation(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        _rigidbody.freezeRotation = false;
    }
}