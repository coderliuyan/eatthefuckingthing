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

	// Use this for initialization
    void Start () {
        coroutine = StartCoroutine(enemyMove());
        playertrans = GameObject.FindWithTag("Player").transform;
    }


    IEnumerator  enemyMove(int rx = 0,int ry = 0)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);

        //如果状态时是 idle 随机位置移动
        if (enemyState == EnemyState.idle)
        {
            enemyState = EnemyState.walk;
            if (rx == 0){
                List<int> xlist = new List<int>();
                xlist.Add(Random.Range(-1 * max, -1 * min));
                xlist.Add(Random.Range(min, max));
                int randomIndex =  Random.Range(0, 2);
                rx = xlist[randomIndex];
            }


                 
            if(ry == 0){
                List<int> ylist = new List<int>();
                ylist.Add(Random.Range(-1 * max, -1 * min));
                ylist.Add(Random.Range(min, max));
                ry = ylist[Random.Range(0, 2)];
            }

            Vector3 v3 = new Vector3(transform.position.x - rx, transform.position.y - ry, transform.position.z);
            GetComponent<Rigidbody2D>().AddForce((v3-transform.position ).normalized* forceEnergy);
            yield return new WaitForSeconds(1f);
            enemyState = EnemyState.idle;
            coroutine =  StartCoroutine(enemyMove());
            //tween = transform.DOMove(v3, 1.3f).SetEase(Ease.Linear);
            //tween.onComplete = delegate
            //{
            //    enemyState = EnemyState.idle;
            //    StartCoroutine(enemyMove());
            //};
        }

        //如果状态是 zhuiji  向player移动
        if(enemyState == EnemyState.zhuiji)
        {
            //enemyState = EnemyState.walk;
            GetComponent<Rigidbody2D>().AddForce((playertrans.position - transform.position).normalized * forceEnergy);
            yield return new WaitForSeconds(1f);
            //enemyState = EnemyState.idle;
            coroutine = StartCoroutine(enemyMove());


            //tween = transform.DOMove(playertrans.position, 1.3f).SetEase(Ease.Linear);
            //tween.onComplete = delegate
            //{
            //    enemyState = EnemyState.zhuiji;
            //    StartCoroutine(enemyMove());
            //};

        }

        
    }

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
            coroutine = StartCoroutine(enemyMove());
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
            coroutine = StartCoroutine(enemyMove());
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.GetComponent<Rigidbody2D>()){
        //    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        //}
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

        //    StartCoroutine(enemyMove(rx,ry));
        //}
    }
}
