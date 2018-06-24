using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {

    public static DoorController current;
    public float doorNum = 0;

    public bool locked = true;

    void Awake()
    {
        current = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        HeroRabit rabit = collider.GetComponent<HeroRabit>();
        if(rabit != null)
        {
            if (doorNum != 0 && !locked)
                SceneManager.LoadScene("Level" + doorNum);

        }
    }
}
