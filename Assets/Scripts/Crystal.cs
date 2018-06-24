using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnRabitHit(HeroRabit rabit)
    {
        string name = this.gameObject.name;
        if (name == "gem-1")
            LevelController.current.pickCrystal(1);
        else if (name == "gem-2")
            LevelController.current.pickCrystal(2);
        else if (name == "gem-3")
            LevelController.current.pickCrystal(3);
        this.CollectedHide();
    }
}
