using System.Collections;
using UnityEngine.Events;
using UnityEngine;


public class MyButton : MonoBehaviour {

    public UnityEvent signalOnClick = new UnityEvent();

    public void _onClick()
    {
        Debug.Log("my button on click");
        this.signalOnClick.Invoke();
    }

    // Use this for initialization
    void Start () {
        Debug.Log("MyButton.start");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
