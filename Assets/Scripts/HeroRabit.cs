using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public static HeroRabit current;

    public Rigidbody2D myBody = null;
    Animator anim = null;

    public float speed = 2;
  //  float diff = Time.deltaTime;

    public bool isBig = false;

    bool isGrounded = false;
    bool JumpActive = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 0f;
    public float JumpSpeed = 0f;

    //private int health = 1;

    //Transform heroParent = null;

    void Awake()
    {
        current = this;
    }

    public void jumpOfOrk()
    {
        this.JumpActive = true;
        this.JumpTime += Time.deltaTime;
        if (this.JumpTime < this.MaxJumpTime)
        {
            Vector2 vel = myBody.velocity;
            vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
            myBody.velocity = vel;
        }
        this.JumpActive = false;
    }

    // Use this for initialization
    void Start()
    {
        myBody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        LevelController.current.setStartPosition(transform.position);

        //this.heroParent = this.transform.parent;
    }

    /*
    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }*/

    // Update is called once per frame
    void Update () {
        //[-1, 1]
        float value = Input.GetAxis("Horizontal");

        Animator animator = GetComponent<Animator>();

        if (Mathf.Abs(value) > 0){
            animator.SetBool("run", true);

            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }else{
            animator.SetBool("run", false);
        }
        
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0){
            sr.flipX = true;
        }else if (value > 0){
            sr.flipX = false;
        }

        Vector3 from = transform.position + Vector3.up * 0.3f;
        Vector3 to = transform.position + Vector3.down * 0.1f;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");

        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit){
            isGrounded = true;
        }else{
            isGrounded = false;
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        if (Input.GetButtonDown("Jump") && isGrounded){
            this.JumpActive = true;
        }

        if (this.JumpActive){
            //Якщо кнопку ще тримають
            if (Input.GetButton("Jump"))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
        
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        //Згадуємо ground check
        
        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                // SetNewParent(this.transform, hit.transform);
                this.transform.parent = hit.transform;
            }
            else
                this.transform.parent = null;
            isGrounded = true;
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            //SetNewParent(this.transform, this.heroParent);
            isGrounded = false;
        }
    }

    IEnumerator rabitDie()
    {
        this.anim.SetBool("die", true);
        yield return new WaitForSeconds(1);
        this.anim.SetBool("die", false);
        LevelController.current.onRabitDeath(this);
    }

    public void removeHealth(int i)
    {
        StartCoroutine(rabitDie());
        //health -= i;
    }

    int health = 3;

    public bool isDead()
    {
        return health == 0;
    }

    public void callDeath()
    {
        StartCoroutine(rabitDie());
    }
}

