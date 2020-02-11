using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIscripts : MonoBehaviour
{
                  .........//THIS SCRIPT CHANGES PLAYER HEALTH BAR AND NO OF KILLS.................

    Slider slider;
    PlayerHealth ph2;
    Scene scene;
    public Text Score;
    public float Kills=0;
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        ph2 = GameObject.Find("Arm").GetComponent<PlayerHealth>();
        slider = GetComponentInChildren<Slider>();
        scene = SceneManager.GetActiveScene();
        //Score = GetComponent<Text>();
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = ph2.P_health/100;
        DisplayScore();
    }
   public void ReplayScene()
   {
        SceneManager.LoadScene(scene.buildIndex);
   }
    void DisplayScore()
    {
        Score.text = "KILLS: " + Kills;
    }
    public void AppExit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }
}
