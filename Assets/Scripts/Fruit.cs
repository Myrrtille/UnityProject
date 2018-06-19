using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : Collectable {

    public int id = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnRabitHit(HeroRabit rabit)
    {
        LevelController.current.addFruits(id);
        this.CollectedHide();
    }
}
