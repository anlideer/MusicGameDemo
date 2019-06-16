using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCtrl : MonoBehaviour
{
    public GameObject rotateObj;
    private List<GameObject> rotateList = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        Input.multiTouchEnabled = true;//开启多点触碰
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in rotateList)
        {
            if (obj == null)
            {
                rotateList.Remove(obj);
            }
        }

        int tcnt = Input.touchCount;
        int lastCnt = 0;
        if (tcnt >= 1)
        {
            for (int i = 0; i < tcnt; i++)
            {
                Touch touch = Input.touches[i];
                // 只识别一定y范围内的
                Vector3 v = Camera.main.ScreenToWorldPoint(touch.position);
                v.z = 0;
                if (v.y < -3)
                {
                    // 点击，点击式音符只识别这一种
                    if (touch.phase == TouchPhase.Began)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero, 1000, 1 << LayerMask.NameToLayer("Icon"));
                        if (hit && hit.collider.gameObject.tag == "ClickIcon")
                        {
                            hit.collider.gameObject.GetComponent<ClickIcon>().Click();
                        }
                    }

                    // 只要不是Ended，判断位置即可，仅针对长按式音符
                    if (touch.phase != TouchPhase.Ended)
                    {

                        RaycastHit2D hit = Physics2D.Raycast(v, Vector2.zero, 1000, 1 << LayerMask.NameToLayer("Icon"));
                        if (hit && hit.collider.gameObject.tag == "LastIcon")
                        {
                            if (hit.collider.gameObject.GetComponent<LastIcon>().isBegin && hit.collider.gameObject.GetComponent<LastIcon>().isAlive)
                            {
                                hit.collider.gameObject.GetComponent<LastIcon>().clicked = true;
                                hit.collider.gameObject.GetComponent<LastIcon>().ShowPerfect();
                                Vector3 tmpv = v;
                                tmpv.y = -3.97f;
                                tmpv.x = hit.collider.gameObject.transform.position.x;
                                GameObject obj = Instantiate(rotateObj, tmpv, transform.rotation);
                                obj.GetComponent<RotateCtrl>().Check(v);
                                obj.GetComponent<RotateCtrl>().last = hit.collider.gameObject;
                                obj.GetComponent<RotateCtrl>().next = hit.collider.gameObject.GetComponent<LastIcon>().next;
                                rotateList.Add(obj);
                            }
                        }
                        if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                        {
                            if (lastCnt >= rotateList.Count)
                            {
                                Debug.Log("Out of cnt in rotateList");
                            }
                            else
                            {
                                // 判断手指跟RotateObj的距离是不是太远了，太远就miss
                                rotateList[lastCnt].GetComponent<RotateCtrl>().Check(v);
                                lastCnt++;
                            }
                        }
                    }
                    

                }
            }
        }
    }


}
