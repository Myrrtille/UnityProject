using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour {

    public Button playButton;
    public Button settingsButton;

    void onPlay()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    void onSettings()
    {
        Debug.Log("settings");
    }

    // Use this for initialization
    void Start () {
        playButton.onClick.AddListener(onPlay);
        settingsButton.onClick.AddListener(onSettings);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
