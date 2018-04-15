using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMenu : MonoBehaviour {

    //editor var
    public string SceneName;

    private GameObject car;
	private bool wasAnchoring = false;

    CarryOver co;


    // Use this for initialization
    void Start () {
        car = GameObject.Find("car");
        co = GameObject.Find("CarryOver").GetComponent<CarryOver>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!car.GetComponent<Carmechanics> ().isAnchoring && !wasAnchoring) {
			transform.position = new Vector2 (transform.position.x, car.transform.position.y);
		} else {
			wasAnchoring = true;
		}
        
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "car") {
            switch(SceneName) {
                case "quit":
                    Application.Quit();
                    break;
                case "1p":
                    co.playerCount = 1;
                    SceneManager.LoadScene("lvl1");
                    break;
                case "2p":
                    co.playerCount = 2;
                    SceneManager.LoadScene("lvl1");
                    break;
                default:
                    SceneManager.LoadScene(SceneName);
                    break;
            }

        }
    }

    private void FixedUpdate() {
        
    }
}
