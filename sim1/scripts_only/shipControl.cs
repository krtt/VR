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


    public Material lineMat;


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
        lrY = go2.AddComponent<LineRenderer>();
        lrX = go3.AddComponent<LineRenderer>();

        // gameobject transform:
        // my new go-s get parented to existing gameobject which owns this script
        
        go2.transform.SetParent(gameObject.transform); // attaching child to parent
        go2.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation); // setting child position and rotation to match the parents

        go3.transform.SetParent(gameObject.transform);
        go3.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);

        lrZ.useWorldSpace = false;
        lrY.useWorldSpace = false;
        lrX.useWorldSpace = false;

        // TextOnScreen:
        //SomeLiveTextHere = GetComponent<Text>();


    }

    void FixedUpdate() {

        // 8th, AddForce + AddTorque try:

        float yaw = Input.GetAxis("Yaw"); // for yaw
        float roll = Input.GetAxis("Roll"); // roll >> change INPUT !!!
        float pitch = Input.GetAxis("Pitch");
        float forward = Input.GetAxis("Forward"); //thrust axis 

        float forceAmountForward = linearForceAmount * forward;
        float forceAmountZ = trqForce * roll;
        float forceAmountY = trqForce * yaw;
        float forceAmountX = trqForce * pitch;

        // my forcevect for visualization:
        

        // meaning: transform.forward is a shortcut to forward-directional axis (Z)
        forceVect = transform.forward * forceAmountForward;

        //// LineRenderer:
        lrZ.SetPosition(1, forceAmountForward * new Vector3(0, 0, 1));
        lrY.SetPosition(1, forceAmountY * new Vector3(0, 1, 0));
        lrX.SetPosition(1, forceAmountX * new Vector3(1, 0, 0));

        // usage: AddForce(float x, float y, float z, ForceMode mode = ForceMode.Force)
        rb.AddForce(forceVect);

        // meaning: yaw - rotating around up-axis (Y)
        rb.AddTorque(transform.up * forceAmountY);


        // meaning: rotating around right-axis (X) >> pitch | use 5th axis in Unity InputManager !!!
        rb.AddTorque(transform.right * forceAmountX);

        // roll:
        rb.AddTorque(transform.forward * forceAmountZ);

        // LineRenderer stuff 2:
        //Debug.DrawLine(Vector3.zero, new Vector3(1, 0, 0), Color.red);
        //Gizmos.DrawLine(Vector3.zero, new Vector3(1, 0, 0));


        //Gizmos.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));
        //Debug.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));



        // LiveText:
        //SomeLiveTextHere.text = mass.ToString(); // if enabled, movement doesn't work - why ???

        // attempt to roll:


        /* Notes for some working values for above :
         * forward movement with joystick ok
         * turning left-right with joystick ok 
         * pitch with joystick ok (found 5th axis, then working ok)
         * blue ship params: | rb: mass 1, drag 1, angulardrag 1, usegravity none | public: linearforceamount -1, mass 1, torqueforce 0.1 
         * black ship params: | rb: mass 1, drag 0, angulardrag 0.05, usegravity yes | public: linearforceamount -1, mass 1, torqueforce 0.1
         * */

    }

    //void OnDrawGizmos(){
    //Gizmos.DrawLine(transform.TransformPoint(new Vector3(0, 0, 0)), transform.TransformPoint(new Vector3(3, 5, 0)));
    //}
    //void OnPostRender()
    //{

    //    GL.Begin(GL.LINES);
    //    lineMat.SetPass(0);
    //    GL.Color(new Color(1f, 0f, 0f, 1f));
    //    GL.Vertex3(0f, 0f, 0f);
    //    GL.Vertex3(12f, 12f, 12f);
    //    GL.End();
    // }


}

// Update() vs. FixedUpdate():
//FixedUpdate should be used when applying forces, torques, or other physics-related functions - because it will be executed exactly in sync with the physics engine itself.
//Update() can vary out of step with the physics engine, either faster or slower, depending on how much of a load the graphics are putting on the rendering engine at any given time, which - if used for physics - would give correspondingly variant physical effects!
