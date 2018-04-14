using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmechanics : MonoBehaviour {
    HingeJoint2D hingeJoint;
    bool isAnchoring = false;

    public KeyCode rightInput { get; set; }
    public KeyCode leftInput { get; set; }

    [Range(0,2)]
    public float speed = 1;

	// Use this for initialization
	void Start () {
        hingeJoint = GetComponent<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(rightInput)) {

            if(!isAnchoring) {
                //transform.RotateAround(transform.position, Vector3.forward, 1f);
                hingeJoint.enabled = true;
                hingeJoint.anchor = new Vector2(0.3f, 0);
                isAnchoring = true;
            }
            else {
                hingeJoint.enabled = false;
                hingeJoint.anchor = new Vector2(0f, 0f);
                isAnchoring = false;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
        }

        if (Input.GetKeyDown(leftInput)) {

            if (!isAnchoring) {
                //transform.RotateAround(transform.position, Vector3.forward, 1f);
                hingeJoint.enabled = true;
                hingeJoint.anchor = new Vector2(-0.3f, 0);
                isAnchoring = true;
            }
            else {
                hingeJoint.enabled = false;
                hingeJoint.anchor = new Vector2(0f, 0f);
                isAnchoring = false;
                GetComponent<Rigidbody2D>().angularVelocity = 0f;
            }
        }


    }

    

    private void FixedUpdate() {
        GetComponent<Rigidbody2D>().AddForce(transform.up * speed);


    }
}
