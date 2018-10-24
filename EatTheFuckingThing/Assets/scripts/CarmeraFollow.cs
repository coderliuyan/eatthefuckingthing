using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraFollow : MonoBehaviour {

    public Transform playerTranform;
	// Use this for initialization
	void Start () {
        playerTranform = GameObject.FindWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if(playerTranform.position.x < 490f && playerTranform.position.x > 75f)
        {
            transform.position = new Vector3(playerTranform.position.x, playerTranform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, playerTranform.position.y, transform.position.z);
    }
}
