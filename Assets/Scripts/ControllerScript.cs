using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player {
    public int id;
    public KeyCode leftInput;
    public KeyCode rightInput;
	public KeyCode upInput;
	public KeyCode downInput;
    public string controllerH;
    public string controllerV;

	public int hitPoints = 30;

	public void getDamage (int damage) {
		this.hitPoints = Mathf.Max (0, this.hitPoints - damage);
	}
}


public class ControllerScript : MonoBehaviour {

    //editor defined variables
    public GameObject carObject;
    public Sprite[] spriteList;
    

    //other
    public Dictionary<int, Player> playerList = new Dictionary<int, Player>();
    public int alivePlayers;

    // Use this for initialization
    void Start () {
        int playerCount = 2;

        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;
        if(GameObject.Find("CarryOver") != null) {
            playerCount = GameObject.Find("CarryOver").GetComponent<CarryOver>().playerCount;
        }

        alivePlayers = playerCount;



        Player player1 = new Player();
        player1.id = 1;
        player1.leftInput = KeyCode.A;
        player1.rightInput = KeyCode.D;
        player1.upInput = KeyCode.W;
        player1.downInput = KeyCode.S;
        player1.controllerH = "Horizontal2";
        player1.controllerV = "Vertical2";
        
        playerList.Add(1, player1);

        if(playerCount > 1) {
            Player player2 = new Player();
            player2.id = 2;
            player2.leftInput = KeyCode.LeftArrow;
            player2.rightInput = KeyCode.RightArrow;
            player2.upInput = KeyCode.UpArrow;
            player2.downInput = KeyCode.DownArrow;
            player2.controllerH = "Horizontal";
            player2.controllerV = "Vertical";
            playerList.Add(2, player2);
        }



        foreach (KeyValuePair<int, Player> c in playerList) {
			GameObject car = Instantiate(carObject, new Vector3(spawnPoint.position.x+(c.Key-1), spawnPoint.position.y, 0), spawnPoint.rotation);
            car.GetComponent<Carmechanics>().cont = this;
            car.GetComponent<Carmechanics>().id = c.Value.id;
            car.GetComponent<Carmechanics>().rightInput = c.Value.rightInput;
            car.GetComponent<Carmechanics>().leftInput = c.Value.leftInput;
			car.GetComponent<Carmechanics>().upInput = c.Value.upInput;
			car.GetComponent<Carmechanics>().downInput = c.Value.downInput;
            car.GetComponent<Carmechanics>().ControllerH = c.Value.controllerH;
            car.GetComponent<Carmechanics>().controllerV = c.Value.controllerV;
            car.GetComponent<SpriteRenderer>().sprite = spriteList[c.Key - 1];

            if(playerCount == 1) {
                    car.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 1);
            }
            else if(playerCount == 2) {

                if (c.Key == 1) {
                    car.GetComponentInChildren<Camera>().rect = new Rect(0, 0, .5f, 1);
                    car.GetComponentInChildren<Camera>().fieldOfView = 90;




                }
                else if(c.Key == 2) {
                    car.GetComponentInChildren<Camera>().rect = new Rect(.5f, 0, .5f, 1);
                    car.GetComponentInChildren<Camera>().fieldOfView = 90;
                }
            }

            
        }




    }

    public void CarDied(int id) {
        alivePlayers--;
        if(alivePlayers == 0) {
            SceneManager.LoadScene(0);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }
}
