using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipControl : MonoBehaviour {

    Rigidbody rb;
    //public float thrust = 100.0f;
    //public float speed = 100.0f;
    //public float rotationSpeed = 100.0f;
    public float fwdSpeedMultiplier = -1.0f; // vt. miks nii väike
    public float mass=1; // unity tut
    //public float g = 9.81f;
    /*
    float rollSpeed = 100.0f; // rolling sideways
    float pitchSpeed = 100.0f; // flip back/forth
    float yawSpeed = 100.0f; // turn left/right
    */

    public Vector3 forcevect;
    public float trqForce =0.1f;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> (); // unity tut
		rb.mass = mass; // unity tut; Different Rigidbodies with large differences in mass can make the physics simulation unstable.
    }
	
	// Update is called once per frame
	void FixedUpdate () {
//	void Update () {


		// 6th, AddForce version + AddTorque try:

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
//		float trqForce = (DestinationRotation - CurrentLocation) * 0.1f; // from unity forums. but how to use destination and currentLocation?
		//trqForce = 0.1f; // test values: 10, 0.1f,  

        // usage: AddForce(float x, float y, float z, ForceMode mode = ForceMode.Force)

        forcevect= transform.forward * fwdSpeedMultiplier *v;
        rb.AddForce(forcevect, ForceMode.Force);
        

        //rb.AddForce(0, g * mass, 0); // const floating
        //rb.AddForce(transform.forward * thrust); // falling down
        //rb.AddForce(0, mass * Time.deltaTime * speed * g, 0);
       //rb.AddForce(0, 0, g * mass * Time.deltaTime * speed*v);
        
        rb.AddTorque(transform.up * trqForce * h);
        
    }
}
