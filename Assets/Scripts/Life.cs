using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnRabitHit(HeroRabit rabit)
    {
        rabit.addHealth();
        this.CollectedHide();
    }
}
