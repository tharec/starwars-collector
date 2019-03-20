using UnityEngine;

public class PowerConverter : MonoBehaviour {

    public bool beingCarried = false;

    public void Grab(Transform _holder)
    {
        beingCarried = true;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = _holder;
        transform.rotation = _holder.transform.rotation;
        transform.position = _holder.transform.position;
        GetComponent<Collider>().enabled = false;
    }

    public void Drop(Camera _mainCam)
    {
        beingCarried = false;
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        transform.rotation = _mainCam.transform.rotation;
        transform.position = _mainCam.transform.position + _mainCam.transform.forward;
        GetComponent<Collider>().enabled = true;
    }

    public bool Place(Transform _object)
    {
        bool foundIt = false;
        int i = 0;
        while(i < _object.childCount && _object.GetChild(i).childCount > 0)
        {
            i++;
        }
        foundIt = i <= _object.childCount;

        if (foundIt)
        {
            beingCarried = false;
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = _object.GetChild(i);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.position = _object.GetChild(i).position;
            return true;
        }
        return false;
    }
}
