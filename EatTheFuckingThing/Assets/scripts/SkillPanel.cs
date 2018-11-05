using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour {

    string normalBtnPath = @"normalBtn";
    Button normalBtn;
	// Use this for initialization
	void Start () {
        Init();
	}
	protected void Init()
    {
        normalBtn = transform.Find(normalBtnPath).GetComponent<Button>();

        normalBtn.onClick.AddListener(PlayerActionManager.manager.ClickNormalButton);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
