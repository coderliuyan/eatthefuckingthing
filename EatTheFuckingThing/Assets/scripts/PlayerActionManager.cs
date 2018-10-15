using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerActionManager : MonoBehaviour {

	public Sprite[] playerMove;

	Animator animator;

	public enum PlayerState{
		idleForword  = 0,
		forword,
		idleLeft,
		left,
		idleBack,
		back,
		idleRight,
		right
	}

	public PlayerState playerState = PlayerState.idleForword;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

//		if (ETCInput.GetAxis ("Horizontal") < 0) {
////			Debug.Log ("hhhhhhhh");
//			animator.SetInteger ("playerState", 7);
//			playerState = PlayerState.right;
//		} else if (ETCInput.GetAxis ("Horizontal") > 0) {
//			animator.SetInteger ("playerState", 3);
//		} else if (ETCInput.GetAxis ("Vertical") > 0) {
//			animator.SetInteger ("playerState", 5);
//		} else if (ETCInput.GetAxis ("Vertical") < 0) {
//			animator.SetInteger ("playerState", 1);
//		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
	  Debug.Log ("-------开始碰撞------------");
	  Debug.Log(coll.gameObject.name);
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
	  Debug.Log("开始接触");
	  Debug.Log (collider.name);
		if (collider.name == "NPC1") {
			Debug.Log ("你要去森林吃屎吗");
			collider.gameObject.GetComponent<NPC> ().closePlayer = true;
		}

		if (collider.name == "npc2") {
			Debug.Log ("你要去被恶龙吃了吗?");

		}
	}

	void OnTriggerExit2D(Collider2D collider){
		collider.gameObject.GetComponent<NPC> ().closePlayer = false;
	}

	//移动的时候播放动画
	public void OnMove(Vector2 v){
		Debug.Log (v);

		if (v.x < 0  &&  Mathf.Abs(v.x) > Mathf.Abs(v.y)) {
			animator.SetInteger("playerState",7);
			playerState = PlayerState.right;
		} else if (v.x > 0 && Mathf.Abs(v.x) > Mathf.Abs(v.y)) {
			animator.SetInteger("playerState",3);
			playerState = PlayerState.left;
		} else
			if (v.y > 0) {
			animator.SetInteger ("playerState",5);
			playerState = PlayerState.back;
		} else if(v.y < 0){
			animator.SetInteger ("playerState",1);
			playerState = PlayerState.forword;
		}
	}

	//停止移动的时候停止动画
	public void OnMoveEnd(){
//		if (animator.GetInteger ("playerState") == 1) {
//			animator.SetInteger ("playerState", 0);
//			playerState = PlayerState.idleForword;
//		}
//
//		if (animator.GetInteger ("playerState") == 3) {
//			animator.SetInteger ("playerState", 2);
//			playerState = PlayerState.idleLeft;
//		}
		int current = animator.GetInteger("playerState");
		animator.SetInteger ("playerState",current - 1);



	}





}
