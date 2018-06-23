using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {

    float curr_directon = 0;
    public float speed = 0.001f;

    protected override void OnRabitHit(HeroRabit rabit)
    {
        rabit.removeHealth(1);
        this.CollectedHide();
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(destroyLater());
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = this.transform.position;
        pos.x += Time.deltaTime + curr_directon * 0.1f;
        this.transform.position = pos;
	}

    public void launch(float direction)
    {
        this.curr_directon = direction;
        if(direction < 0)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    IEnumerator destroyLater()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }
}
