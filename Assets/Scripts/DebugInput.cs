using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : MonoBehaviour {

    bool pressed;
    bool pressed2;
    string horizontal = "Horizontal";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(!pressed && Input.GetAxis(horizontal) > 0) {
            Debug.Log("-->");
            pressed = true;

        }

        if (!pressed && Input.GetAxis(horizontal) < 0) {
            Debug.Log("<--");
            pressed = true;
        }

        if (pressed && Input.GetAxis(horizontal) == 0) {
            pressed = false;
        }

        if (!pressed2 && Input.GetAxis("Horizontal2") > 0) {
            Debug.Log("2 -->");
            pressed2 = true;

        }

        if (!pressed2 && Input.GetAxis("Horizontal2") < 0) {
            Debug.Log("2 <--");
            pressed2 = true;
        }

        if (pressed2 && Input.GetAxis("Horizontal2") == 0) {
            pressed2 = false;
        }

       


    }
}
