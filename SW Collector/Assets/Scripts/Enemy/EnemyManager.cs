using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public float maxHealth = 100f;

    private float health = 0f;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
