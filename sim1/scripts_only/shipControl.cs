using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipControl : MonoBehaviour {

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

		float h = Input.GetAxis("Horizontal"); // for yaw
        float v = Input.GetAxis("Vertical"); // for roll
        float p = Input.GetAxis("Pitch");

        // meaning: transform.forward is a shortcut to forward-directional axis (Z)
        forceVect = transform.forward * linearForceAmount * v;
        
        // usage: AddForce(float x, float y, float z, ForceMode mode = ForceMode.Force)
        rb.AddForce(forceVect);

        // meaning: h for rotating around up-axis (Y)
        rb.AddTorque(transform.up * trqForce * h);

        // meaning: p for rotating around right-axis (X) >> pitch | use 5th axis in Unity InputManager !!!
        rb.AddTorque(transform.right * trqForce * p);
        
        /* Notes for working values for above (blue ship works. black falls down) :
         * forward movement with joystick ok
         * turning left-right with joystick ok 
         * pitch with joystick ok (found 5th axis, then working ok)
         * blue ship params: | rb: mass 1, drag 1, angulardrag 1, usegravity none | public: linearforceamount -1, mass 1, torqueforce 0.1 
         * black ship params: | rb: mass 1, drag 0, angulardrag 0.05, usegravity yes | public: linearforceamount -1, mass 1, torqueforce 0.1
         * */
         
    }
}

// Update() vs. FixedUpdate():
//FixedUpdate should be used when applying forces, torques, or other physics-related functions - because it will be executed exactly in sync with the physics engine itself.
//Update() can vary out of step with the physics engine, either faster or slower, depending on how much of a load the graphics are putting on the rendering engine at any given time, which - if used for physics - would give correspondingly variant physical effects!
