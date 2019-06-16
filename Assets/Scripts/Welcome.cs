using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Welcome : MonoBehaviour
{
    public GameObject startText;
    bool setted = false;

    // Start is called before the first frame update
    void Start()
    {
        setted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!setted && Time.timeSinceLevelLoad > 1f)
        {
            setted = true;
            startText.SetActive(true);
        }

        if (Input.touchCount!=0)
        {
            SceneManager.LoadScene("Cover");
        }
    }
}
