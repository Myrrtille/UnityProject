using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Collectable {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected override void OnRabitHit(HeroRabit rabit)
    {
        this.CollectedHide();
        if (!rabit.isBig)
        {
            rabit.isBig = !rabit.isBig;
            rabit.myBody.transform.localScale += new Vector3(0.5F, 0.5F, 0);
        }
    }
}
