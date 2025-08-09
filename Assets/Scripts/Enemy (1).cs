using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private string _target;
    [SerializeField]
    private float _runSpeed = 2f;
    [SerializeField]
    private string _runAnimationName;
    [SerializeField]
    private string _hitAnimationName;
    [SerializeField]
    private string _dieSoundName;
    [SerializeField]
    private string _dieAnimationName;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private UnityEvent _onInitialize;
    private bool _isRunning;
    private Vector3 _targetPosition;
    private Health _targetHealth;
    private Coroutine _damageCoroutine;
    private void OnEnable()
    {
        _isRunning = false;
        _onInitialize?.Invoke();
        Invoke("GetTarget", 0.5f);
    }
    private void GetTarget()
    {
        GameObject target = GameObject.FindGameObjectWithTag(_target);
        if (target != null && !_isRunning)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x,
                transform.position.y, target.transform.position.z);
            _targetPosition = targetPosition;
            _targetHealth = target.GetComponent<Health>();
            _isRunning = true;
            _animator.Play(_runAnimationName);
        }
    }
    private void Update()
    {
        if (_isRunning)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition,
                _runSpeed* Time.deltaTime);
            transform.LookAt(_targetPosition);
        }
    }
    public void TakeDamage()
    {
        if (_damageCoroutine != null)
        {
            return;
        }
        _damageCoroutine = StartCoroutine(TakeDamageCoroutine());
    }
    private IEnumerator TakeDamageCoroutine()
    {
        _isRunning = false;
        _animator.Play(_hitAnimationName);
        yield return new WaitForSeconds(0.5f);
        _animator.Play(_runAnimationName);
        _isRunning = true;
        _damageCoroutine = null;
    }
    public void Die()
    {
        StopAllCoroutines();
        _isRunning = false;
        StartCoroutine(DieCoroutine());
    }
    private IEnumerator DieCoroutine()
    {
        SoundManager.instance.Play(_dieSoundName);
        _animator.Play(_dieAnimationName);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        _isRunning = false;
        _targetHealth = null;
    }

}
