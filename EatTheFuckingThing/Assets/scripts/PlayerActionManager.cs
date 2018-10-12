using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerActionManager : MonoBehaviour {

	public Sprite[] playerMove;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D coll) {
	  Debug.Log ("-------开始碰撞------------");
	  Debug.Log(coll.gameObject.name);
	}

	void OnTriggerEnter2D(Collider2D collider) {
	  Debug.Log("开始接触");
	  Debug.Log (collider.name);
		if (collider.name == "npc1") {
			Debug.Log ("你要去森林吃屎吗");

		}

		if (collider.name == "npc2") {
			Debug.Log ("你要去被恶龙吃了吗?");

		}
	}

	public void OnMove(Vector2 v){
		Debug.Log (v);
		if (v.x > 0) {
			GetComponent<Image> ().overrideSprite = playerMove [0];
		} else if (v.x < 0) {
			GetComponent<Image> ().overrideSprite = playerMove [1];
		} else if (v.y < 0) {
			GetComponent<Image> ().overrideSprite = playerMove [2];
		} else {
			GetComponent<Image> ().overrideSprite = playerMove [3];
		}
	}



}
