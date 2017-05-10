using UnityEngine;
using System.Collections;

public class FollowWand : MonoBehaviour {
    Rigidbody rb;
    public GameObject target;
    private Vector3 targetPosition;
    public float speed;
    public float closeDis;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        targetPosition = (target.transform.position - transform.position) * speed;
        if (Vector3.Distance(target.transform.position, transform.position) > closeDis)
        {
         
            rb.AddForce(targetPosition.x, targetPosition.y, targetPosition.z, ForceMode.VelocityChange);
            if (targetPosition.y < 0)
            {
                rb.AddForce(-Physics.gravity);
            }
            //rb.AddForce((targetPosition, ForceMode. ) * speed * Time.deltaTime);
        }
    }
}
