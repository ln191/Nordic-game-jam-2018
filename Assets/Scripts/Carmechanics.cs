using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmechanics : MonoBehaviour {
    HingeJoint2D hingeJoint;
    bool isAnchoring = false;
    float cableLenght = 3;

	// Use this for initialization
	void Start () {
        hingeJoint = GetComponent<HingeJoint2D>();
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown("space")) {

            if(!isAnchoring) {
                //transform.RotateAround(transform.position, Vector3.forward, 1f);
                //hingeJoint.enabled = true;

                //Debug.DrawRay(transform.position, Vector2.right, Color.green);

                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right, cableLenght);
                Debug.Log(hits.Length);
                    if (hits.Length > 1 && hits[1].collider != null)
                    {
                        RaycastHit2D hit = hits[1];
                        if (hit.collider.gameObject.tag == "Wall")
                        {
                            Debug.Log("hit");
                            Debug.Log(hit.collider.gameObject.name);
                            hingeJoint.enabled = true;
                            //hingeJoint.anchor = hit.point - new Vector2(transform.position.x, transform.position.y);
                            hingeJoint.anchor = new Vector2(hit.point.x - transform.position.x, hit.point.y - transform.position.y);
                            Debug.Log((hit.point.x - transform.position.x) + "  " + (hit.point.y - transform.position.y));
                            isAnchoring = true;
                        }
                        else
                        {
                            //hingeJoint.enabled = false;
                            //hingeJoint.anchor = new Vector2(0.3f, 0);
                            //isAnchoring = true;
                        }
                    }

                //hingeJoint.anchor = new Vector2(0.3f, 0);
                //isAnchoring = true;
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
