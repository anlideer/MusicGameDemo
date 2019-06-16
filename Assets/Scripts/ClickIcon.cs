using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIcon : MonoBehaviour
{
    public GameObject bomb;
    public float time;
    Animator anim;
    bool isAlive = true;
    GameObject UICtrl;
    bool added = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UICtrl = GameObject.FindGameObjectWithTag("UICtrl");
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.timeSinceLevelLoad - GM.startTime) > time + 4f)
        {
            Destroy(gameObject);
        }
        // 下落
        Vector3 v = transform.position;
        v.y -= 10 * Time.deltaTime;
        transform.position = v;

        if (isAlive)
        {
            if (time - (Time.timeSinceLevelLoad - GM.startTime) < -0.2f)
            {
                isAlive = false;
                UICtrl.GetComponent<UI>().ShowMiss();
                if (added == false)
                {
                    added = true;
                    GM.missNum++;
                }
            }
        }
    }

    public void Click()
    {
        if (isAlive)
        {
            isAlive = false;
            // 判定
            if (Mathf.Abs(time - (Time.timeSinceLevelLoad - GM.startTime)) < 0.05f)
            {
                UICtrl.GetComponent<UI>().ShowPefect();
                if (!added)
                {
                    added = true;
                    GM.perfectNum++;
                }
            }
            else
            {
                UICtrl.GetComponent<UI>().ShowGood();
                if (!added)
                {
                    added = true;
                    GM.goodNum++;
                }
            }

            anim.Play("ClickBomb");
            Instantiate(bomb, transform.position, transform.rotation);
        }
    }
}
