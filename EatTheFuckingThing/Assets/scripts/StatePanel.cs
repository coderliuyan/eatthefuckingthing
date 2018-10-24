using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatePanel : MonoBehaviour {


    string xuetiaoPath = @"xuetiao";
    static Slider xuetiaoSlider;

	// Use this for initialization
	void Start () {
        Transform xuetiao = transform.Find(xuetiaoPath);
        xuetiaoSlider = xuetiao.GetComponent<Slider>();
	}

	// Update is called once per frame
	void Update () {
		
	}

    public static void ChangeXuetiaoValue(){
        xuetiaoSlider.value =1-  PlayerActionManager.manager.hp / 100.0f;
        Debug.Log(" xuetiaoSlider.value  " + xuetiaoSlider.value);
    }
}

//public class EventListener
//{
//    public delegate void OnPlayerHpChangedDelegate(int hp);

//    public event OnPlayerHpChangedDelegate OnHpChanged;
//    private int hp;
//    public int Hp
//    {
//        get
//        {
//            return Hp;
//        }
//        set
//        {
//            if (hp == value) return;
//            if (OnHpChanged != null)
//            {
//                OnHpChanged(PlayerActionManager.manager.hp);
//            }
//            hp = value;
//        }
//    }
//}
