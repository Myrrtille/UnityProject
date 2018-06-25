using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrc : MonoBehaviour {

    public static RedOrc current;

    public enum Mode
    {
        GoToA,
        GoToB,
        Dead,
        CarrotLeft,
        CarrotRight,
        Attack
    }

    public GameObject weapon;
    float last_carrot = 0;

    Mode mode = Mode.GoToA;

    Rigidbody2D myBody = null;
    Animator anim = null;
    public float speed = 2;
    public float walkingArea = 4;
    Vector3 scale_speed;
    Vector3 targetScale = Vector3.one;

    Vector3 pointA;
    Vector3 pointB;

    IEnumerator orcDie()
    {
        HeroRabit.current.jumpOfOrk();
        this.mode = Mode.Dead;
        this.anim.SetTrigger("die");
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }

    public void callAttack()
    {
        //this.anim.SetTrigger("attack");
        StartCoroutine(orkAttack());
        if (HeroRabit.current.isBig)
        {
            HeroRabit.current.isBig = false;
            HeroRabit.current.myBody.transform.localScale -= new Vector3(0.5F, 0.5F, 0);
        }
        else HeroRabit.current.callDeath();
    }

    float getDirection()
    {
        if (this.mode == Mode.Dead)
            return 0;

        Vector3 my_pos = this.transform.position;
        Vector3 rabit_pos = HeroRabit.current.transform.position;

        //attack
        if (rabit_pos.x > Mathf.Min(pointA.x, pointB.x)
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
        {
            mode = Mode.Attack;
            current = this;
        }
        //finish attack

        if (mode == Mode.Attack && !(rabit_pos.x > Mathf.Min(pointA.x, pointB.x))
            && rabit_pos.x < Mathf.Max(pointA.x, pointB.x))
        {
            mode = Mode.GoToA;
        }

        //if attack - move to rabit
        if (mode == Mode.Attack && !HeroRabit.current.isDead())
        {
            if (my_pos.x < rabit_pos.x)
                return 1;
            else
            {
                return -1;
            }
        }

        //if a or b reached
        if (this.mode == Mode.GoToA)
        {
            if (my_pos.x <= pointA.x)
            {
                //rotate();
                this.mode = Mode.GoToB;
            }
        }
        else if (this.mode == Mode.GoToB)
        {
            if (my_pos.x >= pointB.x)
            {
                //rotate();
                this.mode = Mode.GoToA;
            }
        }

        //get new direction
        if (this.mode == Mode.GoToA)
        {
            if (my_pos.x <= pointA.x)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        else if (this.mode == Mode.GoToB)
        {
            if (my_pos.x <= pointB.x)
            {
                return 1;
            }
            else return -1;
        }
        return 0;
    }

    // Use this for initialization
    void Start () {
        pointA = this.transform.position;
        pointB = pointA;

        if (walkingArea < 0)
            pointA.x += walkingArea;
        else
            pointB.x += walkingArea;

        myBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        LevelController.current.setStartPosition(this.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        float val = this.getDirection();
        Animator animator = GetComponent<Animator>();

        if(this.mode == Mode.Attack)
        {
            animator.SetBool("run", false);
            launchCarrot(val);
        }else if (Mathf.Abs(val) > 0){
            animator.SetBool("run", true);
            Vector2 vel = myBody.velocity;
            vel.x = val * speed;
            myBody.velocity = vel;
        }
        else
        {
            animator.SetBool("run", false);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (val < 0)
        {
            sr.flipX = false;
        }
        else if (val > 0)
        {
            sr.flipX = true;
        }

        this.transform.localScale = Vector3.SmoothDamp(this.transform.localScale, this.targetScale, ref scale_speed, 1.0f);
    }

    IEnumerator orkAttack()
    {
        this.anim.SetBool("attack", true);
        yield return new WaitForSeconds(0.5f);
        this.anim.SetBool("attack", false);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer("Rabit"))
            return;

        if (collider.transform.position.y > this.transform.position.y + 1.5)
        {
            StartCoroutine(orcDie());
        }
        else
        {
            callAttack();
        }
    }

    void launchCarrot(float direction)
    {
        if (Time.time - last_carrot > 2.0f)
        {
            this.last_carrot = Time.time;
            //Створюємо копію Prefab
            GameObject obj = GameObject.Instantiate(this.weapon);
            //Розміщуємо в просторі
            obj.transform.position = this.transform.position;
            //Запускаємо в рух
            Carrot carrot = obj.GetComponent<Carrot>();
            carrot.launch(direction);
        }
    }
    
}
