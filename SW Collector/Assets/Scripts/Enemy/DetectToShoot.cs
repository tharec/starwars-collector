using UnityEngine;

public class DetectToShoot : MonoBehaviour {

    public float damage = 0f;
    public float fireRate = 0F;
    public float inaccuracy = 0F;

    private float blasterBeamForce = 400F;
    private float nextFire = 0F;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject projectileObject;
    public GameObject gunEnd;
    public AudioSource[] fireSounds;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time > nextFire && other.transform.tag == "Player")
        {
            nextFire = Time.time + 1F / fireRate;
            ShootBlaster();
        }
    }

    void ShootBlaster()
    {
        muzzleFlash.Play();
        fireSounds[Random.Range(0, fireSounds.Length)].Play();

        GameObject tempObj = Instantiate(projectileObject, gunEnd.transform.position, gunEnd.transform.rotation);

        tempObj.SendMessage("setBlasterDamage", damage);

        tempObj.transform.Rotate(Vector3.left * 90);

        Rigidbody tempRig = tempObj.GetComponent<Rigidbody>();

        Vector3 iacc = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));

        tempRig.AddForce((gunEnd.transform.forward * blasterBeamForce) + iacc);

        Destroy(tempObj, 1.5F);
    }
}
