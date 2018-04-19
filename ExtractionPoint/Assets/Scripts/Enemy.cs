using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour {

    #region Variables
    public float maxhp = 20;
    private float hp;
    int attackDamage = 5;
    int attackRange = 15;
    int x;
    int y;
    float newX;
    float newY;
    public Transform player;
    public Transform boundary;
    Vector3 newPos;
    Hero hero = new Hero();
    private float maxSpeed = 1.5f;
    private float speed;
    public float startAttackWait;
    public float attackWait;
    public float step;
    public float dist;
    public bool attacking = false;
    public bool movingToPlayer = false;
    public bool movingToBoundary = false;
    public bool forward = true;
    private Animator anim;

    public Image healthBar;
    #endregion Variables

    #region Get+Set
    public float Maxhp
    {
        get
        {
            return maxhp;
        }

        set
        {
            maxhp = value;
        }
    }

    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            if (value <= maxhp)
                hp = value;
            else hp = maxhp;

            if (value <= 0)
            {
                hp = 0;
                OnNoHP();
            }
        }
    }

    public int AttackDamage
    {
        get
        {
            return attackDamage;
        }

        set
        {
            attackDamage = value;
        }
    }

    public int AttackRange
    {
        get
        {
            return attackRange;
        }

        set
        {
            attackRange = value;
        }
    }

    public float Step
    {
        get
        {
            return step;
        }

        set
        {
            step = value;
        }
    }

    public Vector3 NewPos
    {
        get
        {
            return newPos;
        }

        set
        {
            newPos = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }
    #endregion Get+Set

    #region Methods
    void Start ()
    {
        speed = maxSpeed;
        Hp = Maxhp;
        player = GameObject.FindGameObjectWithTag("Hero").transform;            //Finds the hero game object
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
        anim = GetComponent<Animator>();                                        //Gets the enemy's animator component
    }
	
	void Update ()
    {
        step = Speed * Time.deltaTime;                                          //Sets how fast the enemy can travel
        moveToPlayer();
        ResetValues();
    }

    void ResetValues()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Hero")                           //Detects if the player is colliding with the enemy
        {
            anim.SetBool("Move", false);
            anim.SetBool("Attack", false);
            speed = 0;
            attacking = true;
            hero = col.gameObject.GetComponent<Hero>();
            StartCoroutine(attackHero(hero));                       //Starts the coroutine to attack the hero
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Hero")                            //Detects if the player moves away from the enemy
        {
            anim.SetBool("Attack", false);
            speed = maxSpeed;
            attacking = false;
            StopCoroutine(attackHero(hero));                        //Stops the coroutine to attack the hero
        } 
    }

    public void moveToPlayer()
    {
        if (player != null)
        {
            dist = Vector2.Distance(gameObject.transform.position, player.transform.position);        //Determines the distance between the enemy and the player

            if (dist <= attackRange)                                                                  //Determines if the player is in the range of the enemy
            {
                movingToPlayer = true;
                transform.position = Vector2.MoveTowards(transform.position, player.position, step);  //Moves the enemy towards the player
                movingToBoundary = false;
                anim.SetBool("Move", true);                                                                         //Calls a method to change the animation state of the enemy
            }
            else
            {
                movingToPlayer = false;
                anim.SetBool("Move", false);
            }
        }
    }

    public IEnumerator moveToBoundary()
    {
        yield return new WaitForSeconds(5);

        boundary = GameObject.FindGameObjectWithTag("Boundary").transform;
       
        if (!movingToPlayer)
        {
            while (movingToBoundary)
            {
                if (boundary != null && this != null)
                {
                    newX = Random.Range(boundary.position.x + boundary.localScale.x, boundary.position.x - boundary.localScale.x);
                    newY = Random.Range(boundary.position.y + boundary.localScale.y, boundary.position.y - boundary.localScale.y);
                    NewPos = new Vector3(newX, newY, 1);
                    anim.SetBool("Move", true);
                    transform.position = Vector2.MoveTowards(transform.position, NewPos, step);

                    yield return null;
                }
                else
                {
                    boundary = GameObject.FindGameObjectWithTag("Boundary").transform;
                }
            }
        }
    }

    public void Damage(int damage)
    {
        this.Hp -= damage;
        healthBar.fillAmount = Hp / Maxhp;
        
    }

    public IEnumerator attackHero(Hero hero)
    {
        yield return new WaitForSeconds(startAttackWait);
        
        while (attacking)
        {
            anim.SetBool("Attack", true);
            hero.Damage(attackDamage);
            yield return new WaitForSeconds(attackWait);
        }

    }

   /* public void enemyAnim()
    {
        if (player.position.y > transform.position.y)
        {
            anim.SetBool("UpMove", true);
        }
        else if (player.position.y > transform.position.y)
        {
            anim.SetBool("DownMove", true);
        }
        else if(player.position.y == transform.position.y)
        {
            anim.SetBool("UpMove", false);
            anim.SetBool("DownMove", false);
        }

        if (player.position.x > transform.position.x)
        {
            forward = true;
            anim.SetBool("HorizMove", true);
        }
        else if(player.position.x < transform.position.x)
        {
            forward = false;
            anim.SetBool("HorizMove", true);
        }
        else if(player.position.x == transform.position.x)
        {
            anim.SetBool("HorizMove", false);
        }
    }
    */

    void FlipEnemy()
    {
        float movement = Input.GetAxis("Horizontal");

        if ((movement > 0 && !forward) || (movement < 0 && forward))
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = -playerScale.x;
            transform.localScale = playerScale;
            forward = !forward;
        }
    }

    public void OnNoHP()
    {
        hero.Score += 10;
        EventManager.Instance.PostNotification(Events.DEADENEMY, this);
    }
    #endregion
}

