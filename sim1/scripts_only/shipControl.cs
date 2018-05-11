using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipControl : MonoBehaviour {

    /*
    float rollSpeed = 100.0f; // rolling sideways
    float pitchSpeed = 100.0f; // flip back/forth
    float yawSpeed = 100.0f; // turn left/right
    */
    Rigidbody rb;
    public float linearForceAmount = -1.0f;
    public float mass = 1; // unity tut
    public float trqForce = 0.1f;
    public Vector3 forceVect;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody> (); // unity tut
		rb.mass = mass; // unity tut; Different Rigidbodies with large differences in mass can make the physics simulation unstable.
    }

	void FixedUpdate () {

		// 8th, AddForce + AddTorque try:

		float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        forceVect = transform.forward * linearForceAmount * v;
        
        // usage: AddForce(float x, float y, float z, ForceMode mode = ForceMode.Force)
        rb.AddForce(forceVect);

        rb.AddTorque(transform.up * trqForce * h);
        
        /* Notes for working values for above (blue ship works. black falls down) :
         * forward movement with joystick ok
         * turning left-right with joystick ok 
         * blue ship params: | rb: mass 1, drag 1, angulardrag 1, usegravity none | public: linearforceamount -1, mass 1, torqueforce 0.1 
         * black ship params: | rb: mass 1, drag 0, angulardrag 0.05, usegravity yes | public: linearforceamount -1, mass 1, torqueforce 0.1
         * */






        //		// 4th, rotation while moving (but needs AddForce as I don't want to disable gravity):
        //		rb.AddForce (0, 9.81f, 0); // constant floating
        //		// use addForce instead:
        //
        //
        //		// bypasses physics:
        //		transform.position += transform.forward * Time.deltaTime; // * speed; // very slow if no speed (ok)
        //		transform.Rotate(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal")); // rotation only



        // 3rd, simple rotation:
        //		if(Input.GetKeyDown(KeyCode.LeftArrow)){
        //			transform.Rotate (0, -90.0f, 0); // turns 90 deg immediately
        //		}



        // 2nd, simple x, y movement while floating:

        //		rb.AddForce (0, 9.81f, 0); // constant floating
        //		float horizontal = Input.GetAxis ("Horizontal");
        //		float vertical = Input.GetAxis ("Vertical"); // It's not vertical! It's forward/backward.
        ////		float upDn = Input.GetAxis("???"); // what can i use here for 3rd axis so i can avoid using GetKey... and transform.Translate ?
        //		Vector3 movement = new Vector3 (horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        //		rb.MovePosition (transform.position + movement);



        // 1st try, simple up, down, left, right with arrow keys:

        //		rb.AddForce (0, 9.81f, 0); // constant floating
        //
        //		if (Input.GetKey("right")) {
        //			transform.Translate (0.1f, 0, 0);
        //		}
        //		if (Input.GetKey("left")) {
        //			transform.Translate (-0.1f, 0, 0);
        //		}
        //		if (Input.GetKey("up")) {
        //			transform.Translate (0, 0, 0.1f);
        //		}
        //		if (Input.GetKey("down")) {
        //			transform.Translate (0, 0, -0.1f);
        //		}
        //		if (Input.GetKey("w")) {
        //			transform.Translate (0, 0.1f, 0);
        //		}
        //		if (Input.GetKey("z")) {
        //			transform.Translate (0, -0.1f, 0);
        //		}
        //
        //		 Something that I don't want:
        //
        //		if (Input.GetKey(KeyCode.Space)){
        //			rb.AddForce (0, 10.8f, 0); // going up when holding space - not exactly ok gravity usage
        //		}
    }
}

// Update() vs. FixedUpdate():
//FixedUpdate should be used when applying forces, torques, or other physics-related functions - because it will be executed exactly in sync with the physics engine itself.
//Update() can vary out of step with the physics engine, either faster or slower, depending on how much of a load the graphics are putting on the rendering engine at any given time, which - if used for physics - would give correspondingly variant physical effects!
