using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cover : MonoBehaviour
{

    public GameObject panel;
    public Text sliderVal;
    public Slider slider;
    public GameObject levelChoosingPanel;
    public GameObject noThisLevel;

    bool shown = false;
    float timecnt;


    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("delay", 0.1f);
        noThisLevel.SetActive(false);
        shown = false;
    }

    // Update is called once per frame
    void Update()
    {
        sliderVal.text = slider.value.ToString();

        if(shown && timecnt + 1f < Time.timeSinceLevelLoad)
        {
            shown = false;
            noThisLevel.SetActive(false);
        }
    }

    public void EnterGame()
    {
        levelChoosingPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Setting()
    {
        panel.SetActive(true);
    }


    public void SetVal()
    {
        PlayerPrefs.SetFloat("delay", slider.value);
    }

    public void ClosePanel()
    {
        panel.SetActive(false);
    }

    public void ToLevel(int n)
    {
        if (n == 1)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            noThisLevel.SetActive(true);
            shown = true;
            timecnt = Time.timeSinceLevelLoad;
        }
    }

    public void CloseLevelChoosing()
    {
        levelChoosingPanel.SetActive(false);
    }
}
