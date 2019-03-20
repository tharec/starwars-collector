using UnityEngine;

public class BlasterBolt : MonoBehaviour {

    public GameObject impactEffect;
    private float blasterDamage = 0F;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);

        GameObject impactGo = Instantiate(impactEffect, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(impactGo, 1F);

        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage(blasterDamage, collision.relativeVelocity);
            Debug.Log("HIT");
        }
        else if(collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyManager>().TakeDamage(blasterDamage);
        }
    }

    public void setBlasterDamage(float newDamage)
    {
        blasterDamage = newDamage;
    }
}
