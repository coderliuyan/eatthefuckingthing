using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour {

	List<string> talkMsg;

	Image image;
	Text t;
	int indexMsg = 0;

	public bool closePlayer = false;
	// Use this for initialization
	void Start () {
		talkMsg = new List<string> ();
		talkMsg.Add ("大鱼大肉?你想的美!");
		talkMsg.Add ("世界那么大,好吃的那么多!");
		talkMsg.Add ("森林里有什么美食,想知道吗?");

		image = transform.Find ("talkBg").GetComponent<Image>();
		t = transform.Find ("talkBg/talk").GetComponent<Text> ();

		InvokeRepeating ("AutoTalk", 0f, 3f);

	}
	void AutoTalk(){
		Debug.Log ("aaaaa");
		if(!closePlayer)
			StartCoroutine (AnimationMsg());
	}

	IEnumerator AnimationMsg(){
		image.gameObject.SetActive (false);
		yield return new WaitForSeconds (1f);
		image.gameObject.SetActive (true);
		if (indexMsg == 3)
			indexMsg = 0;
		t.text = talkMsg [indexMsg];
		indexMsg++;

		yield return new WaitForSeconds (2f);

		image.gameObject.SetActive (false);
	}


}
