using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private Vector3 movementVector;
    [SerializeField] private float period = 2f;

    private Vector3 _startPosition;

    private const float Tau = Mathf.PI * 2;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (period <= Mathf.Epsilon) return;

        var cycles = Time.time / period;
        var rawSinWave = Mathf.Sin(cycles * Tau);
        var movementFactor = (rawSinWave + 1f) / 2f;

        var offset = movementVector * movementFactor;
        transform.position = _startPosition + offset;
    }
}