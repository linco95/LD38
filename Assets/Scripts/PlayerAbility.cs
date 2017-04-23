using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour {


    private void Update() {
        if (Input.GetAxis("Fire1") > 0.0f) {
            transform.localScale = new Vector2(2, 2);
        }
        else {
            transform.localScale = new Vector2(1, 1);

        }
    }
	
}
