using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public GameObject Perfect;
    public GameObject Good;
    public GameObject Miss;
    float timecnt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timecnt + 0.5f < Time.timeSinceLevelLoad)
        {
            Good.SetActive(false);
            Miss.SetActive(false);
            Perfect.SetActive(false);
        }
    }

    public void ShowPefect()
    {
        Good.SetActive(false);
        Miss.SetActive(false);
        Perfect.SetActive(true);
        timecnt = Time.timeSinceLevelLoad;
    }

    public void ShowGood()
    {
        Perfect.SetActive(false);
        Miss.SetActive(false);
        Good.SetActive(true);
        timecnt = Time.timeSinceLevelLoad;
    }

    public void ShowMiss()
    {
        Perfect.SetActive(false);
        Good.SetActive(false);
        Miss.SetActive(true);
        timecnt = Time.timeSinceLevelLoad;
    }


}
