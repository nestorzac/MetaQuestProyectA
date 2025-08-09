using UnityEngine;

public class EnemyCollison : MonoBehaviour
{

    [SerializeField]
    private Health _health;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);
            _health.TakeDamage(10);
        }
    }



}
