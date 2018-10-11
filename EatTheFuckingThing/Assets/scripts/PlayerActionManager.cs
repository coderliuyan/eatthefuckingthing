using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerActionManager : MonoBehaviour {


	Text msg ;
//	GameObject msgParent;
	// Use this for initialization
	void Start () {
		msg = GameObject.Find ("msgText").GetComponent<Text>();
		msg.gameObject.SetActive (false);
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
			msg.gameObject.SetActive (true);
			msg.text = "你要去森林吃屎吗";

		}

		if (collider.name == "npc2") {
			Debug.Log ("你要去被恶龙吃了吗?");
			msg.gameObject.SetActive (true);
			msg.text = "你要去被恶龙吃了吗?";
		}
	}



}
