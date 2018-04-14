using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public int id;
    public KeyCode leftInput;
    public KeyCode rightInput;
    public Color color;
}


public class ControllerScript : MonoBehaviour {

    //editor defined variables
    public GameObject carObject;

    //other
    public Dictionary<int, Player> playerList = new Dictionary<int, Player>();

	// Use this for initialization
	void Start () {

        Transform spawnPoint = GameObject.Find("SpawnPoint").transform;

        Player player1 = new Player();
        player1.id = 1;
        player1.leftInput = KeyCode.LeftArrow;
        player1.rightInput = KeyCode.RightArrow;
        player1.color = Color.red;
        playerList.Add(1, player1);

        Player player2 = new Player();
        player2.id = 2;
        player2.leftInput = KeyCode.A;
        player2.rightInput = KeyCode.D;
        player2.color = Color.blue;
        playerList.Add(2, player2);


        foreach (KeyValuePair<int, Player> c in playerList) {
            GameObject car = Instantiate(carObject, new Vector3(spawnPoint.position.x+c.Key, spawnPoint.position.y, 0), spawnPoint.rotation);
            car.GetComponent<Carmechanics>().rightInput = c.Value.rightInput;
            car.GetComponent<Carmechanics>().leftInput = c.Value.leftInput;
            car.GetComponent<SpriteRenderer>().color = c.Value.color;
            if(c.Key == 1) {
                car.GetComponentInChildren<Camera>().rect = new Rect(0, 0, .5f, 1);
            }
            else if(c.Key == 2) {
                car.GetComponentInChildren<Camera>().rect = new Rect(.5f, 0, .5f, 1);
            }
            
        }




    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
