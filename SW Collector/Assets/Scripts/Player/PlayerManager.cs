using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public float maxHealth = 100f;
    public GameObject holder;
    public Camera mainCam;
    public Text healthTracker;

    private float health = 0f;
    private bool isCarrying = false;
    private PowerConverter carriedObj;
    private Vector3 deathVelocity = Vector3.zero;

    private void Start()
    {
        health = maxHealth;
        healthTracker.text = health + " / " + maxHealth;
    }

    private void Update()
    {
        bool pressedE = Input.GetKeyDown(KeyCode.E);

        if (pressedE)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCam.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0)), out hit, 2f))
            {
                if (hit.transform.tag == "PowerConverter" && !isCarrying)
                {
                    carriedObj = hit.transform.GetComponent<PowerConverter>();
                    carriedObj.Grab(holder.transform);
                    isCarrying = true;
                }
                else if(hit.collider.tag == "Speeder" && isCarrying)
                {
                    Transform slotObj = hit.collider.transform.parent.Find("Slots");
                    if (carriedObj.Place(slotObj))
                    {
                        isCarrying = false;
                        FindObjectOfType<GameManager>().Score();
                    }
                }
                else if (isCarrying)
                {
                    carriedObj.Drop(mainCam);
                    isCarrying = false;
                }
            }
            else if (isCarrying)
            {
                carriedObj.Drop(mainCam);
                isCarrying = false;
            }


        }

        if(health <= 0f || transform.position.y < -15f)
        {
            FindObjectOfType<GameManager>().PlayerDied();
            GetComponent<Rigidbody>().freezeRotation = false;
            GetComponent<Rigidbody>().AddForce(deathVelocity);
            GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
            mainCam.GetComponentInChildren<BlasterShooting>().enabled = false;
        }
    }

    public void TakeDamage(float _damage, Vector3 _velocity)
    {
        health -= _damage;
        deathVelocity = -_velocity;
        healthTracker.text = health + " / " + maxHealth;
    }
}
