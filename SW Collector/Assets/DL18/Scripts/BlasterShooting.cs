using UnityEngine;

public class BlasterShooting : MonoBehaviour {

	public float damage = 0f;
    public float fireRate = 0F;
    public float inaccuracy = 0F;
    public float heatGenerating = 0F;
    public float cooling = 0F;
    private float blasterBeamForce = 400F;
    private float nextFire = 0F;
    private bool overheated = false;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject projectileObject;
    public GameObject gunEnd;
    public AudioSource[] fireSounds;

    private void Start()
    {
        SendMessage("SetCooling", cooling);
    }

    void Update () {
		if (Input.GetButton("Fire1") && Time.time > nextFire && !overheated) {
            nextFire = Time.time + 1F / fireRate;
            Shoot();
		}
	}
	
	void Shoot(){
		muzzleFlash.Play();
		fireSounds[Random.Range(0, fireSounds.Length)].Play();

        GameObject tempObj = Instantiate(projectileObject, gunEnd.transform.position, gunEnd.transform.rotation);

        tempObj.SendMessage("setBlasterDamage", damage);
        SendMessage("ModifyHeat", heatGenerating);

        tempObj.transform.Rotate(Vector3.left * 90);

        Rigidbody tempRig = tempObj.GetComponent<Rigidbody>();

        Vector3 iacc = new Vector3(Random.Range(-inaccuracy, inaccuracy), Random.Range(-inaccuracy, inaccuracy));

        tempRig.AddForce( (gunEnd.transform.forward * blasterBeamForce) + iacc);

        Destroy(tempObj, 1.5F);
    }

    public void OverheatGun(bool temp)
    {
        overheated = temp;
    }
}
