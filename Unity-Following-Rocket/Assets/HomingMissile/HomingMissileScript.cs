using System;
using UnityEngine;

public class HomingMissileScript : MonoBehaviour
{
    private Transform target;
    private new Rigidbody rigidbody;
    [SerializeField] private float rocketSpeed; // My default is 17
    
    private void Awake()
    {
        target = GameObject.FindWithTag("Target").transform; // Get the target transform with tag
        rigidbody = GetComponent<Rigidbody>(); // Get the rigidbody 
    }

    private void FixedUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        var currentPosition = transform.position; // Current position of missile
        var direction = (target.transform.position - currentPosition).normalized; // Get vector to rotate our missile
        
        transform.Rotate(-Vector3.Cross(direction, transform.up)); // Why I used cross?
        // https://docs.unity3d.com/ScriptReference/Vector3.Cross.html

        //transform.position += transform.up * Time.deltaTime * 2f; // Missile moves with transform, not good enough
        
        rigidbody.AddForce(transform.up * rigidbody.mass * rocketSpeed); // If we move with rigidbody, missile goes better
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // When rocket collides with target, explode
        {
            Destroy(this.gameObject);
        }
    }
}
