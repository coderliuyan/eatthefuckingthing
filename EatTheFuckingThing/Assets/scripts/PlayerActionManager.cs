using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

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


    public static PlayerActionManager manager;

    public List<GameObject> closeNpcs = new List<GameObject>();



    private bool isAttack = false;
    private List<GameObject> enemys = new List<GameObject>();
    private GameObject skills;
    //提示框的相关部分
    Transform Ctrl;
    GameObject animationPanel;
    string animationPanelPath = @"AnimationPanel";
    string playerSliderPath = @"PlayerSlider";
    Slider playerSlider;

    public int hp = 100;

    private void Awake()
    {
        if(manager == null)
        {
            manager = this;
        }
    }

    // Use this for initialization
    void Start () {
		animator = GetComponent<Animator> ();
        Ctrl = GameObject.Find("Ctrl").transform;
        animationPanel = Ctrl.Find(animationPanelPath).gameObject;
        playerSlider = animationPanel.transform.Find(playerSliderPath).GetComponent<Slider>();

        //后面要改成是否是激活状态,更换不同的技能.点击不同的按钮激活不同的状态 要改!!!
        skills = transform.Find("skill1").gameObject;



	}
	
	// Update is called once per frame
	void Update () {


	}

    public void ClickNormalButton()
    {

        //if(enemys.Count == 0){
        //    isAttack = false;
        //}

        if(closeNpcs.Count > 0 && !isAttack)
        {
            NPCPanel.npcManager.TalkToNPCWithGameObject(closeNpcs[0]);
        }

        if(isAttack && enemys.Count> 0 )
        {
            //在攻击范围里 进行攻击 还需判定是范围攻击还是单体攻击 目前先是单体攻击吧 进入攻击范围的第一个敌人

            Debug.LogWarning("我也干他妈的");
            StartCoroutine(PlayerAttack(enemys[0]));
        }

    }


    IEnumerator  PlayerAttack(GameObject enemy){
        skills.SetActive(true);
        enemy.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(0.2f);
        skills.SetActive(false);
        enemy.GetComponent<Image>().color = Color.white;
    }




	void OnCollisionEnter2D(Collision2D coll) {
	  //Debug.Log ("-------开始碰撞------------");
	  //Debug.Log(coll.gameObject.name);
	}


    [Header("距离")]
    public float npcDistance = 30f;
    public float enemyDistance = 70f;


	void OnTriggerEnter2D(Collider2D col) 
    {
        Debug.Log("开始接触");
        Debug.Log (col.transform.parent.name);

        if(col.transform.parent.name == "door")
        {
            StartCoroutine(ChangeScene(col.gameObject));
        }

        if (col.transform.parent.name == "enemy" )
        {
            isAttack = true;
            enemys.Add(col.gameObject);
        }


        if (col.transform.parent.name == "NPCPanel" ){
            closeNpcs.Add(col.gameObject);
        }

       




	}

    IEnumerator ChangeScene(GameObject col){
        animationPanel.SetActive(true);
        playerSlider.enabled = false;
        while(playerSlider.value < 1f)
        {
            playerSlider.value += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


        playerSlider.value = 0;
        //yield return new WaitForSeconds(1.3f);
        animationPanel.SetActive(false);
        string doorPath ="portal/" + col.name + "Pos";
        Transform tran = transform.parent.Find(doorPath);
        transform.position = tran.position;
    }


	void OnTriggerExit2D(Collider2D col){
       
        if(closeNpcs.Contains(col.gameObject)){
            closeNpcs.Remove(col.gameObject);
        }

        if (enemys.Contains(col.gameObject))
        {
            enemys.Remove(col.gameObject);
        }

    }

	//移动的时候播放动画
	public void OnMove(Vector2 v){

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
	public void OnMoveEnd()
    {
		int current = animator.GetInteger("playerState");
		animator.SetInteger ("playerState",current - 1);
	}





}
