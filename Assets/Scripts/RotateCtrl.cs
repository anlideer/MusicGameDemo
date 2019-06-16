using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCtrl : MonoBehaviour
{
    public float updatedTime;
    public bool isAlive = true;
    Animator anim;
    float timecnt;
    public GameObject last;
    public GameObject next;
    bool setted = false;
    Vector3 lastTouch;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        lastTouch = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {

            // 判断next的到了判定线没有，要不要更新
            if (Mathf.Abs(next.GetComponent<LastIcon>().time - (Time.timeSinceLevelLoad - GM.startTime)) < 0.05f)
            {
                if (Mathf.Abs(transform.position.x - lastTouch.x) > 3f)
                {
                    ToSmall();
                }
                else
                {
                    // 更新一下，然后给出Prefect
                    next.GetComponent<LastIcon>().ShowPerfect();
                    last = next;
                    next = last.GetComponent<LastIcon>().next;
                    if (next == null)
                    {
                        ToSmall();
                    }
                }
            }
            // 跟着解析式继续移动
            else
            {
                Vector3 lastV = last.transform.position;
                Vector3 nextV = next.transform.position;
                if (lastV.x == nextV.x)
                {
                    Vector3 vec = transform.position;
                    vec.x = lastV.x;
                    transform.position = vec;
                }
                else
                {
                    Vector3 vec = transform.position;
                    vec.x = ((lastV.x - nextV.x) / (lastV.y - nextV.y)) * (vec.y - lastV.y) + lastV.x;
                    transform.position = vec;
                }
            }
            
        }
        else
        {
            if (!setted)
            {
                setted = true;
                if (next != null)
                {
                    next.GetComponent<LastIcon>().isAlive = false;
                }
            }
            if (timecnt + 0.5f < Time.timeSinceLevelLoad)
            {
                Destroy(gameObject);
            }
        }

    }

    public void ToSmall()
    {
        isAlive = false;
        anim.Play("LastToSmall");
        timecnt = Time.timeSinceLevelLoad;
    }

    public void Check(Vector3 v)
    {
        lastTouch = v;
    }

}
