using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastIcon : MonoBehaviour
{
    public float time;
    public GameObject next;
    public bool isBegin = false;
    public bool isAlive = true;
    public bool clicked = false;
    GameObject UICtrl;
    bool setted = false;
    bool shown = false;
    bool added = false;



    // Start is called before the first frame update
    void Start()
    {
        UICtrl = GameObject.FindGameObjectWithTag("UICtrl");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            if (!setted)
            {
                setted = true;
                if (next != null)
                {
                    next.GetComponent<LastIcon>().isAlive = false;
                }

            }

            if (shown == false && (time - (Time.timeSinceLevelLoad - GM.startTime) )<  - 0.2f)
            {
                shown = true;
                UICtrl.GetComponent<UI>().ShowMiss();
                if (added == false)
                {
                    added = true;
                    GM.missNum++;
                }
            }
        }
        else
        {
            if (!clicked && Mathf.Abs(time - (Time.timeSinceLevelLoad - GM.startTime)) < 0.05f)
            {
                isAlive = false;
            }
        }
        if (Time.timeSinceLevelLoad - GM.startTime > time + 6f)
        {
            Destroy(gameObject);
        }


        // 下落
        Vector3 v = transform.position;
        v.y -= 10 * Time.deltaTime;
        transform.position = v;

        if (next != null)
        {
            LineRenderer line;
            if (gameObject.GetComponent<LineRenderer>() == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
                //只有设置了材质 setColor才有作用
                Shader s = Resources.Load("LineShader") as Shader;
                line.material = new Material(s);
                line.positionCount = 2;//设置两点
                line.startColor = line.endColor = Color.black; //设置直线起始点颜色
                line.startWidth = line.endWidth = 0.2f;//设置直线宽度

                //设置指示线的起点和终点
                line.SetPosition(0, transform.position);
                line.SetPosition(1, next.transform.position);
            }
            else
            {
                line = gameObject.GetComponent<LineRenderer>();
            }

            line.SetPosition(0, transform.position);
            line.SetPosition(1, next.transform.position);

        }
        
        /* 只是不知道还有没有用不太想删，不是恶意占行数！！！
        if (isAlive)
        {

            if (next!= null)
            {
                next.GetComponent<LastIcon>().rotateObj = rotateObj;
            }
            if (Mathf.Abs(time - (Time.timeSinceLevelLoad - GM.startTime)) < 0.01f)
            {
                if (!ok && rotateObj != null)
                {
                    if (Mathf.Abs(transform.position.x - rotateObj.transform.position.x) < 0.5f)
                    {
                        // 直接perfect吧........我好堕落
                        UICtrl.GetComponent<UI>().ShowPefect();
                        ok = true;
                        isAlive = false;
                        if (next == null)
                        {
                            rotateObj.GetComponent<RotateCtrl>().ToSmall();
                        }
                    }
                }
                if (rotateObj == null)
                {
                    isAlive = false;
                    UICtrl.GetComponent<UI>().ShowMiss();
                }
            }
            else if (!ok && time - (Time.timeSinceLevelLoad - GM.startTime) < 0)
            {
                isAlive = false;
                UICtrl.GetComponent<UI>().ShowMiss();
                rotateObj.GetComponent<RotateCtrl>().ToSmall();
            }
        }
        */
        

    }

    public void ShowPerfect()
    {
        clicked = true;
        UICtrl.GetComponent<UI>().ShowPefect();
        if (added == false)
        {
            added = true;
            GM.perfectNum++;
        }

        shown = true;
    }


}
