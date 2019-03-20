using UnityEngine;

public class DetectPlayer : MonoBehaviour {



    private void OnTriggerStay(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            transform.parent.rotation = Quaternion.LookRotation(-(other.transform.position - transform.parent.position).normalized);
        }
    }
}
