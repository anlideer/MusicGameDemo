using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    public static List<Icon> iconList;
    public AudioSource music;
    public static float musicLen;
    float musicNow = 0f;  //  音乐播放的进度 // 不一定需要，暂停可以用TimeScale实现
    public static float bpm = 63.745f;
    public static bool isReady = false;
    public static float startTime;
    public float delay = 0.1f;

    public static int perfectNum = 0;
    public static int goodNum = 0;
    public static int missNum = 0;

    public GameObject panel;
    public GameObject resPanel;
    public Text perfectText;
    public Text goodText;
    public Text missText;


    // Start is called before the first frame update
    void Start()
    {
        delay = PlayerPrefs.GetFloat("delay", 0.1f);
        musicLen = music.GetComponent<AudioSource>().clip.length;
        isReady = false;
        perfectNum = goodNum = missNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // 留1s用来保证加载完成
        if (!isReady && Time.timeSinceLevelLoad > 1f)
        {
            isReady = true;
            startTime = Time.timeSinceLevelLoad + delay;
            music.Play();
        }

        if (music.time == musicLen)
        {
            resPanel.SetActive(true);
            perfectText.text = perfectNum.ToString();
            goodText.text = goodNum.ToString();
            missText.text = missNum.ToString();
        }
    }


    // 我又把UI相关写GM里了，dbq......

    public void StopGame()
    {
        Time.timeScale = 0;
        musicNow = music.time;
        music.Stop();
        panel.SetActive(true);
    }

    public void Continue()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        music.time = musicNow;
        music.Play();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainScene");
    }
    public void Exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Cover");
    }
}
