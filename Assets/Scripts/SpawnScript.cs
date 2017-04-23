using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpawnScript : MonoBehaviour {

    public GameObject playerPrefab;

	// Use this for initialization
	void Start () {
        Assert.IsNotNull(playerPrefab, "Player prefab not specified");
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
