using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;
public class EnemyPathFinding : MonoBehaviour {

   

    public enum EnemyState{
        idle,
        walk,
        zhuiji,
        attack
    }
    [Header("敌人的状态")]
    public EnemyState enemyState = EnemyState.idle;

    [Header("移动的距离范围")]
    public int min;
    public int max;

    [Header("添加力的大小")]
    public int forceEnergy;
    //Tween tween;


    Coroutine coroutine;

    public Transform playertrans;

    [HideInInspector]
    public int enemyAttack = 10;

    private GameObject skill;

	// Use this for initialization
    void Start () {
        coroutine = StartCoroutine(enemyAction());
        playertrans = GameObject.FindWithTag("Player").transform;
        skill = transform.Find("skill").gameObject;

    }





    IEnumerator enemyAction(int rx = 0,int ry = 0)
    {
        //先让敌人ai 停下来
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;


        switch (enemyState)
        {

            case EnemyState.idle:
                {
                    if (rx == 0)
                    {
                        List<int> xlist = new List<int>();
                        xlist.Add(Random.Range(-1 * max, -1 * min));
                        xlist.Add(Random.Range(min, max));
                        int randomIndex = Random.Range(0, 2);
                        rx = xlist[randomIndex];
                    }



                    if (ry == 0)
                    {
                        List<int> ylist = new List<int>();
                        ylist.Add(Random.Range(-1 * max, -1 * min));
                        ylist.Add(Random.Range(min, max));
                        ry = ylist[Random.Range(0, 2)];
                    }

                    Vector3 v3 = new Vector3(transform.position.x - rx, transform.position.y - ry, transform.position.z);
                    GetComponent<Rigidbody2D>().AddForce((v3 - transform.position).normalized * forceEnergy);
                    yield return new WaitForSeconds(1f);

                    coroutine = StartCoroutine(enemyAction());

                }
                break;
            case EnemyState.zhuiji:
                {
                    GetComponent<Rigidbody2D>().AddForce((playertrans.position - transform.position).normalized * forceEnergy);

                    distance = Mathf.Abs((playertrans.position - transform.position).magnitude);
                    if (distance < 70)
                    {
                        //小于这个距离 开始攻击
                        AttackHero();
                    }

                    yield return new WaitForSeconds(1f);

                    coroutine = StartCoroutine(enemyAction());
                }
                break;
            case EnemyState.attack:
                {
                    //开始攻击
                    //Debug.Log("开始攻击");

                  
                    distance = Mathf.Abs((playertrans.position - transform.position).magnitude);
                    if (distance <= 70)
                    {
                        skill.gameObject.SetActive(true);
                        playertrans.GetComponent<Image>().color = Color.red;
                        yield return new WaitForSeconds(0.2f);
                        skill.gameObject.SetActive(false);
                        playertrans.GetComponent<Image>().color = Color.white;

                        if (PlayerActionManager.manager.hp >= enemyAttack)
                        {
                            PlayerActionManager.manager.hp -= enemyAttack;

                        }
                        else{
                            PlayerActionManager.manager.hp = 0;
                            //玩家 翘辫子
                        }
                    }
                    StatePanel.ChangeXuetiaoValue();
                    //Debug.Log("玩家血量为"  + PlayerActionManager.manager.hp);
                  
                    yield return new WaitForSeconds(1f);
                    enemyState = EnemyState.zhuiji;
                    coroutine = StartCoroutine(enemyAction());
                }
                break;

        }

    }

    private void AttackHero(){
        StopCoroutine(coroutine);
        Debug.Log("distance = " + distance);
        enemyState = EnemyState.attack;
        coroutine = StartCoroutine(enemyAction());
    }

    [Header("与玩家之间的距离")]
    public float distance;
	// Update is called once per frame
	void Update () {
       
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Debug.Log(collision.name);
        if(collision.tag == "Player")
        {
            Debug.Log("in !!!!!!!!");
            StopCoroutine(coroutine); 
            enemyState = EnemyState.zhuiji;
            coroutine = StartCoroutine(enemyAction());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("out !!!!!!!!");
            //StopAllCoroutines();
            StopCoroutine(coroutine);
            enemyState = EnemyState.idle;
            coroutine = StartCoroutine(enemyAction());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>()){
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
        //Debug.Log("!!!!!!!!!!!!!!!");
        //if (collision.transform.tag == "wall")
        //{
        //    //tween.Kill();
        //    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //    enemyState = EnemyState.idle;
        //    int rx, ry;
        //    if(collision.transform.position.x > transform.position.x){
        //        rx = Random.Range(min, max);
        //    }
        //    else
        //    {
        //        rx = Random.Range(-1 *max, -1 *min);
        //    }
        //    if(collision.transform.position.y > transform.position.y){
        //        ry = Random.Range(min, max);
        //    }
        //    else
        //    {
        //        ry = Random.Range(-1 * max, -1 * min);
        //    }

        //    StartCoroutine(enemyAction(rx,ry));
        //}
    }



}
