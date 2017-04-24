using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour {

    public void respawnPlayer() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().respawn(); ;
    }
}
