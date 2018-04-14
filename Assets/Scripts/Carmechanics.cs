using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmechanics : MonoBehaviour {
    HingeJoint2D hingeJoint;
    bool isAnchoring = false;

	// Use this for initialization
	void Start () {
        hingeJoint = GetComponent<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("space")) {

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
            }
        }

    }

    private void FixedUpdate() {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 2);


    }
}
