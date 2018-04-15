using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryOver : MonoBehaviour {

    public int playerCount;

    private void Awake() {
        //GameServer will live forever across multiple dimensions and scenes
        DontDestroyOnLoad(this);

        //Ensures no duplicate GameServers
        if (FindObjectsOfType(GetType()).Length > 1) {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
