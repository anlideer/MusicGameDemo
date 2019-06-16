using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float timecnt;

    // Start is called before the first frame update
    void Start()
    {
        timecnt = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timecnt + 1f)
        {
            Destroy(gameObject);
        }
    }
}
