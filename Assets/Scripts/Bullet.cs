using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody component is missing from the bullet GameObject.");
        }
    }
    private void OnEnable()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }
}
