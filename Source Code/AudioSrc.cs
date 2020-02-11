using System.Collections;
using System.Collections.Generic;
using UnityEngine;

........................//STORING THE SOUNDS FROM PROJECT FOLDER AND USING AT PARTICULAR POINT OF TIME IN THE GAME................

public class AudioSrc : MonoBehaviour {
    public static AudioClip BombDrop, Deestroy, Death, click, click1, defeat, win,defeatmeme,winmeme,cric;
    static AudioSource audiosrc;
    public GameObject Mute;
    public GameObject Unmute;

    // Use this for initialization
    void Start () {
        BombDrop = Resources.Load<AudioClip>("Birddrop");
        Deestroy = Resources.Load<AudioClip>("destroyaudio");
        Death = Resources.Load<AudioClip>("Birdfunnysound");
        click = Resources.Load<AudioClip>("touch");
        defeat = Resources.Load<AudioClip>("defeatsound");
        win = Resources.Load<AudioClip>("win sound");
        defeatmeme = Resources.Load<AudioClip>("sad sound");
        winmeme = Resources.Load<AudioClip>("imtheone");
        cric = Resources.Load<AudioClip>("cricketsound");
      


        audiosrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		
	}
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Birddrop":
                audiosrc.PlayOneShot(BombDrop);
                break;
            case "destroyaudio":
                audiosrc.PlayOneShot(Deestroy);
                break;
            case "Birdfunnysound":
                audiosrc.PlayOneShot(Death);
                break;
            case "touch":
                audiosrc.PlayOneShot(click);
                break;
            case "defeatsound":
                audiosrc.PlayOneShot(defeat);
                break;
            case "win sound":
                audiosrc.PlayOneShot(win);
                break;
            case "sad sound":
                audiosrc.PlayOneShot(defeatmeme);
                break;
            case "imtheone":
                audiosrc.PlayOneShot(winmeme);
                break;
            case "cricketsound":
                audiosrc.PlayOneShot(cric);
                break;


        }
    }

}
