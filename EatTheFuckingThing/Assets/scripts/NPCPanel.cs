using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCPanel: MonoBehaviour {

    Dictionary<string, List<string>> talkMsgDic = new Dictionary<string, List<string>>();

    //所有的NPC
    List<GameObject> npcs = new List<GameObject>();

    //所有对话框的父级
    List<GameObject> talks = new List<GameObject>();

    //所有的对话显示的文本框
    List<Text> npcTalkLabels = new List<Text>();

	int npcIndex = -1;

    int taiCiIndex = 0;
	public bool closePlayer = false;

    public static NPCPanel npcManager;

    private void Awake()
    {
        if(npcManager == null)
        {
            npcManager = this;
        }
    }

    // Use this for initialization
    void Start () {
        //假数据 以后要从本地或者远程读取
        List<string> list1 = new List<string>();
        list1.Add ("大鱼大肉?你想的美!");
        list1.Add ("世界那么大,好吃的那么多!");
        list1.Add ("森林里有什么美食,想知道吗?");
        string name1 = "npc1";
        talkMsgDic.Add(name1,list1);

        List<string> list2 = new List<string>();
        list2.Add("你要什么?");
        list2.Add("想要进入食堂吗?");
        list2.Add("想大吃特吃就进去吧!");

        string name2 = "npc2";
        talkMsgDic.Add(name2, list2);


        //获取各个组件
        FetchAllTalkLabel();

    }


    void FetchAllTalkLabel(){
        npcTalkLabels.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            string pathNpc = "npc" + (i + 1);
            GameObject obj = transform.Find(pathNpc).gameObject;
            npcs.Add(obj);

            string pathParent = pathNpc + "/talk";
            GameObject objParent = transform.Find(pathParent).gameObject;
            talks.Add(objParent);
            objParent.SetActive(false);

            string path = pathParent + "/talkMsg";
            Text t = transform.Find(path).GetComponent<Text>();
            npcTalkLabels.Add(t);

        }

    }


    public void TalkToNPCWithGameObject(GameObject obj)
    {
        int index = -1;
        foreach(GameObject npc in npcs){
            if(npc.name == obj.name){
                index = npcs.IndexOf(npc);
            }
        }

        if(index != -1)
        {
            //有这个npc 进行对话
            //StopCoroutine(TalkToNPC(npcIndex));
            TalkToNPC(index);
        }

    }

    private void TalkToNPC(int index)
    {
        string npcName = "npc" + (index + 1);
        //检查是否是跟同一个npc对话
        if (index != npcIndex)
        {
            taiCiIndex = 0;
            //刚跟这个npc对话
            npcIndex = index;
            //对应npc的对话框显示
            talks[npcIndex].SetActive(true);
        }
        else if (taiCiIndex < talkMsgDic[npcName].Count-1)
        {
            //是连续的对话
            taiCiIndex++;
        }else{
            ClearTalkCache();
            return;
        }

        string taici = talkMsgDic[npcName][taiCiIndex];
        npcTalkLabels[npcIndex].text = taici;


    }

    void ClearTalkCache()
    {
        talks[npcIndex].SetActive(false);
        taiCiIndex = 0;
        npcIndex = -1;
    }

}



public class NPC : MonoBehaviour
{
    public bool closePlayer = false;



}
