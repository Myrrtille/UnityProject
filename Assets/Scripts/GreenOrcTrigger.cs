using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenOrcTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(anim.GetBool("run"));
        if (collider.gameObject.layer != 10)
            return;

        Debug.Log("!!!!!!");
        Vector3 from = collider.transform.position + Vector3.up * 0.3f;
        Vector3 to = collider.transform.position + Vector3.down * 0.1f;
        int layer_id = 8 << LayerMask.NameToLayer("rabit");
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            //StartCoroutine(orcDie());
        }
        else
        {
            GreenOrc.current.callAttack();
        }
    }
}
