using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 直接把关卡编辑器的粘过来了

[System.Serializable]
public class MyData
{
    public List<MusicIcon> iconList;    // 最后按pos排下序就行辽，没必要用dictionary了
}


[System.Serializable]
public class MusicIcon
{
    public bool type;   //  false - 点击式, true - 长按式
    public int track;   //  轨道编号
    public float pos;   //  在小节中的位置
    public int code;    //  唯一标识符
    public int lastPoint = 0;
    public int nextPoint = 0;   //  用唯一标识符来表示节点间的关系，因为貌似json不支持引用那种...
}

// 游戏中使用的类
public class Icon
{
    public bool type;   //  false - 点击式, true - 长按式
    public int track;   //  轨道编号
    public float time;
    public Icon lastPoint = null;
    public Icon nextPoint = null;
}



public class LoadJson : MonoBehaviour
{

    private MyData musicData;

    // Start is called before the first frame update
    void Start()
    {
        var jsonTextFile = Resources.Load<TextAsset>("myData") as TextAsset;
        musicData = JsonUtility.FromJson<MyData>(jsonTextFile.ToString());
        TransferData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 转换成更容易处理的形式
    private void TransferData()
    {
        GM.iconList = new List<Icon>();
        foreach(MusicIcon mi in musicData.iconList)
        {
            Icon tmp = new Icon();
            tmp.type = mi.type;
            tmp.track = mi.track;
            tmp.time = mi.pos * 60/ GM.bpm;
            //Debug.Log(tmp.time);
            GM.iconList.Add(tmp);
        }
        // 处理前后关系
        for (int i = 0; i < musicData.iconList.Count; i++)
        {
            MusicIcon tmp = musicData.iconList[i];
            if (tmp.lastPoint != 0)
            {
                int p = FindMusicIcon(tmp.lastPoint);
                if (p == -1)
                {
                    Debug.Log("Error in finding music icon.");
                }
                else
                {
                    GM.iconList[i].lastPoint = GM.iconList[p];
                }
            }
            if (tmp.nextPoint != 0)
            {
                int p = FindMusicIcon(tmp.nextPoint);
                if (p == -1)
                {
                    Debug.Log("Error in finding music icon.");
                }
                else
                {
                    GM.iconList[i].nextPoint = GM.iconList[p];
                }
            }

        }
    }

    private int FindMusicIcon(int code)
    {
        for (int i = 0; i < musicData.iconList.Count; i++)
        {
            if (musicData.iconList[i].code == code)
            {
                return i;
            }
        }
        return -1;
    }
}
