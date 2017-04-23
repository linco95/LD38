using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera2D : MonoBehaviour {

    private Transform target;


    // TODO: Apply smoothing and looking ahead etc.

	// Update is called once per frame
	void Update () {
        if (target == null) {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else { 
            transform.position = target.position - new Vector3(0.0f, 0.0f, 10.0f);
        }
	}
}
