using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour {


  

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            (collision.GetComponent<PlayerController>()).respawn();
        }
    }
}
