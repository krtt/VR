using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shipControl : MonoBehaviour {

    Rigidbody rb;

    public float linearForceAmount = -1.0f;
    public float mass = 1; // unity tut
    public float trqForce = 0.1f;
    public Vector3 forceVect, forceVectVisualZ, forceVectVisualY, forceVectVisualX;

    //public Text SomeLiveTextHere;

    private LineRenderer lrZ;
    private LineRenderer lrY;
    private LineRenderer lrX;


    public Material lineMat; // attempt for line material


    private GameObject go2;
    private GameObject go3;



    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>(); // unity tut
        rb.mass = mass; // unity tut; Different Rigidbodies with large differences in mass can make the physics simulation unstable.

        //lineMat = new Material(Shader.Find("Assets/shaders/testmat"));

        go2 = new GameObject();
        go3 = new GameObject();


        // LineRenderers:
        lrZ = gameObject.AddComponent<LineRenderer>();
        lrY = go2.AddComponent<LineRenderer>(); // creating 2nd GameObject to use LineRender
        lrX = go3.AddComponent<LineRenderer>(); // creating 3rd GameObject to use LineRender

        lrZ.material.color = Color.red;
        lrZ.SetWidth(0.009f, 0.009f);

        lrY.material.color = Color.red;
        lrY.SetWidth(0.05f, 0.05f);

        lrX.material.color = Color.red;
        lrX.SetWidth(0.05f, 0.05f); // not ok

        // gameobject.transform (meaning: new created GameObjects get parented to previously existing GameObject which owns this script):

        go2.transform.SetParent(gameObject.transform); // attaching child to parent
        go2.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation); // setting child position and rotation to match the parents

        go3.transform.SetParent(gameObject.transform);
        go3.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);

        lrZ.useWorldSpace = false; // if true, line sticks to the ground in world space
        lrY.useWorldSpace = false;
        lrX.useWorldSpace = false;

        // TextOnScreen:
        //SomeLiveTextHere = GetComponent<Text>();


    }

    void FixedUpdate() {

        // roll, pitch, yaw and forward thrust to control the ship:

        float roll = Input.GetAxis("Roll");
        float pitch = Input.GetAxis("Pitch");
        float yaw = Input.GetAxis("Yaw");
        float forward = Input.GetAxis("Forward"); // thrust axis 
        // TODO: use getaxis in Update loop, not fixedupdate. Then use it in fixedupdate.

        // force amounts to be applied:

        float forceAmountForward = linearForceAmount * forward;
        float forceAmountZ = trqForce * roll;
        float forceAmountY = trqForce * yaw;
        float forceAmountX = trqForce * pitch;

        // transform.forward is a shortcut to forward-directional axis (Z in my case)
        forceVect = transform.forward * forceAmountForward;

        // LineRenderer:
        lrZ.SetPosition(1, forceAmountForward * new Vector3(0, 0, 1));
        lrY.SetPosition(1, forceAmountY * new Vector3(0, 1, 0));
        lrX.SetPosition(1, forceAmountX * new Vector3(1, 0, 0));
        // TODO: show vect gradient

        // usage: AddForce(float x, float y, float z, ForceMode mode = ForceMode.Force)
        rb.AddForce(forceVect);

        // Yaw - rotating around up-axis (Y)
        rb.AddTorque(transform.up * forceAmountY);

        // Pitch: rotating around right-axis (X) >> use 5th axis in Unity InputManager
        rb.AddTorque(transform.right * forceAmountX);

        // Roll: rotating around forward-axis (Z)
        rb.AddTorque(transform.forward * forceAmountZ);



        // LineRenderer stuff 2 lesson learned: *** Only ONE line renderer per game object is possible !!! ***
        
        
        // Attempts to draw debug lines and to use gizmos:
        //Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);
        //Gizmos.DrawLine(Vector3.zero, new Vector3(1, 0, 0));
        //Gizmos.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));
        //Debug.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));
        
        // LiveText:
        //SomeLiveTextHere.text = mass.ToString(); // if enabled, movement doesn't work - why ???

    }

    // Attempt GizmosDrawLine, Debug.Drawline:
    //void OnDrawGizmos(){
    //Gizmos.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));
    //}
    //void OnPostRender()
    //{

    // Attempt with OpenGL:
    //    GL.Begin(GL.LINES);
    //    lineMat.SetPass(0);
    //    GL.Color(new Color(1f, 0f, 0f, 1f));
    //    GL.Vertex3(0f, 0f, 0f);
    //    GL.Vertex3(12f, 12f, 12f);
    //    GL.End();
    // }


}

// Just remember this already:
// Update() vs. FixedUpdate():
//FixedUpdate should be used when applying forces, torques, or other physics-related functions - because it will be executed exactly in sync with the physics engine itself.
//Update() can vary out of step with the physics engine, either faster or slower, depending on how much of a load the graphics are putting on the rendering engine at any given time, which - if used for physics - would give correspondingly variant physical effects!
