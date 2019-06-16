using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsIcon : MonoBehaviour
{
    public GameObject clickIcon;
    public GameObject lastIcon;
    public GameObject[] insPoints;
    List<bool> loaded = new List<bool>();
    int loadP = 0;

    bool inited = false;
    bool isEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        loaded = new List<bool>();
        loadP = 0;
        inited = false;
        isEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.isReady)
        {
            if (!inited)
            {
                inited = true;
                int cnt = GM.iconList.Count;
                while(cnt-- > 0)
                {
                    loaded.Add(false);
                }
            }

            if (!isEnd)
            {

                //Debug.Log(loaded.Count);
                //Debug.Log(loadP);
                //Debug.Log(loaded[loadP]);


                // 因为音符是遵循时间顺序的，所以从前往后就行，这里有个漏洞，前2s的音符无法进行加载
                //Debug.Log(GM.iconList[loadP].time - Time.timeSinceLevelLoad - GM.startTime - 2f);
                if (Mathf.Abs(GM.iconList[loadP].time - (Time.timeSinceLevelLoad - GM.startTime) - 2f) < 0.02f)  
                {
                    // 点击式
                    if (GM.iconList[loadP].type == false)
                    {
                        //Debug.Log("INS CLICK");
                        InsClick();
                    }
                    // 长按式
                    else
                    {
                        //Debug.Log("INS LAST");  
                        InsLast();
                    }
                    loadP++;
                }
                else if (GM.iconList[loadP].time - (Time.timeSinceLevelLoad - GM.startTime) - 2f < 0)
                {
                    loadP++;
                }


                if (loadP < loaded.Count && loaded[loadP])
                {
                    loadP++;
                }

                if (loadP >= loaded.Count)
                {
                    isEnd = true;
                }
            }
        }
    }

    // 点击式
    private void InsClick()
    {
        loaded[loadP] = true;
        Vector3 v = insPoints[GM.iconList[loadP].track].transform.position;
        v.z = -1;   // 为方便起见，点击和长按都放在-1吧
        v.y += (GM.iconList[loadP].time - (Time.timeSinceLevelLoad - GM.startTime)) * 10f;
        GameObject icon = Instantiate(clickIcon, v, transform.rotation);
        icon.GetComponent<ClickIcon>().time = GM.iconList[loadP].time;

    }

    // 长按式
    private void InsLast()
    {
        loaded[loadP] = true;
        Vector3 v = insPoints[GM.iconList[loadP].track].transform.position;
        v.z = -1;   // 为方便起见，点击和长按都放在-1吧
        v.y += (GM.iconList[loadP].time - (Time.timeSinceLevelLoad - GM.startTime)) * 10f;
        GameObject icon = Instantiate(lastIcon, v, transform.rotation);
        icon.GetComponent<LastIcon>().time = GM.iconList[loadP].time;
        icon.GetComponent<Animator>().Play("LastBigStatic");
        icon.GetComponent<LastIcon>().isBegin = true;
        Icon ic = GM.iconList[loadP];
        while (ic.nextPoint != null)
        {
            // 用1s-20f换算
            Icon next = ic.nextPoint;
            int i = loadP;
            for(; i < GM.iconList.Count; i++)
            {
                if (GM.iconList[i].Equals(ic.nextPoint))
                {
                    break;
                }
            }
            if (i == GM.iconList.Count)
            {
                Debug.Log("Error in finding icon");
            }
            else
            {
                loaded[i] = true;
                Vector3 nextV = insPoints[GM.iconList[i].track].transform.position;
                nextV.z = -1;
                nextV.y += (GM.iconList[i].time - (Time.timeSinceLevelLoad - GM.startTime)) * 10f;
                GameObject nextObj = Instantiate(lastIcon, nextV, transform.rotation);
                nextObj.GetComponent<LastIcon>().time = GM.iconList[i].time;
                icon.GetComponent<LastIcon>().next = nextObj;

                icon = nextObj;
                ic = ic.nextPoint;
            }
        }
    }
}
