using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Carmechanics : MonoBehaviour {
	HingeJoint2D hingeJoint;
    Text HPText;
	public bool isAnchoring = false;
	public Vector2 direction = new Vector2(0,0);
    private bool checkpoint = false;
    private bool isDead;

	public int hitPoints = 30;

	public void getDamage (int damage) {
		this.hitPoints = Mathf.Max (0, this.hitPoints - damage);
	}

    float cableLenght = 8;

    Vector3 wallPoint;

    public int id;
    public KeyCode rightInput;
	public KeyCode leftInput;
	public KeyCode upInput;
    public KeyCode downInput;
    public string ControllerH;
    public string controllerV;

    //Set by controllerscript
    public ControllerScript cont;
    

    [Range(0,2)]
    public float speed = 1;

	// Use this for initialization
	void Start () {
		hingeJoint = GetComponent<HingeJoint2D>();
		HPText = transform.Find("Canvas/Hit Points").GetComponent<Text>();
        if(id == 2) {
            HPText.alignment = TextAnchor.UpperRight;
        }
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (hitPoints == 0 && !isDead) {
            //gameObject.SetActive (false);
            cont.CarDied(id);
            speed = 0;
            isDead = true;
        }

		direction = new Vector2(0,0);

		if (Input.GetKey (rightInput)) {
			direction = Vector2.right;
		} else if (Input.GetKey (leftInput)) {
			direction = Vector2.left;
		} else if (Input.GetKey (upInput)) {
			direction = Vector2.up;
			GetComponent<Rigidbody2D> ().AddForce (transform.up * 2);
		} else if (Input.GetKey (downInput)) {
			direction = Vector2.down;
		}

		if (direction.magnitude > 0) {

			RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.rotation * direction, cableLenght);
            Debug.Log(hits.Length);
            if (hits.Length > 1 && hits[1].collider != null)
            {
                RaycastHit2D hit = hits[1];
                if (hit.collider.gameObject.tag == "Wall")
                {
                    wallPoint = hit.point;
					hingeJoint.enabled = direction != Vector2.down;
                    hingeJoint.anchor = direction * hit.distance;
                    isAnchoring = true;
				} else if (hit.collider.gameObject.tag == "Car")
				{
					wallPoint = hit.point;
					hingeJoint.enabled = direction != Vector2.down;
					hingeJoint.anchor = direction * hit.distance;
					isAnchoring = true;
				}
            }
           
        } else {
			hingeJoint.enabled = false;
			hingeJoint.anchor = new Vector2(0f, 0f);
			isAnchoring = false;
			GetComponent<Rigidbody2D>().angularVelocity = 0;
		}

        if(isAnchoring && wallPoint != null)
        {
            DrawLine(wallPoint, Color.black);
        }

		if (HPText) {
			Debug.Log ("HP text found");
			HPText.text = hitPoints + " / 30 HP";
		}


        if (Input.GetKey(KeyCode.J)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


    }

    void DrawLine(Vector3 wall, Color color, float duration = 0.02f)
    {

        List<Vector2> newVerticies = new List<Vector2>();
        GameObject myLine = new GameObject();
        myLine.name = "Line";
        myLine.transform.position = wall;
        myLine.AddComponent<LineRenderer>();
        Rigidbody2D r2 = myLine.AddComponent<Rigidbody2D>();
        r2.gravityScale = 0;
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.SetColors(color, color);
        lr.SetWidth(0.05f, 0.05f);
        lr.SetPosition(0, wall);
        lr.SetPosition(1, new Vector3(transform.position.x, transform.position.y));
        GameObject.Destroy(myLine, duration);
        Debug.Log("DRAW");
    }

    private void FixedUpdate() {
		if (direction != Vector2.down) {
			GetComponent<Rigidbody2D> ().AddForce (transform.up * speed);
		} else {
			GetComponent<Rigidbody2D>().AddForce(-transform.up * speed*1.2f);
		}
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		Debug.Log ("Collision!");
		if (collision.gameObject.tag == "Wall") {
			getDamage (1);
		} else if (collision.gameObject.tag == "Car") {
			getDamage (2);
		}

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Checkpoint")
        {
            checkpoint = true;
            Debug.Log("checkpoint  passed");
        }

        if (collision.gameObject.tag == "Goal" && checkpoint == true)
        {
            Debug.Log("goal");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
          
        }
    }


}
