using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Variables
    [SerializeField]
    private float speed = 2;
    private int direction = 1;
    private bool forward = true;
    private bool attacking = false;
    private float attackTimer = 0;
    private float attackCoolDown = 0.5f;

    [SerializeField] private GameObject thrown;
    [SerializeField] private Transform throwPos;
    [SerializeField] private Transform throwPos2;
    [SerializeField] private Transform throwPos3;

    public Collider2D attackTrigger1;
    public Collider2D attackTrigger2;
    public Collider2D attackTrigger3;

    Hero hero;
    private Animator anim;
    private static PlayerController instance = null;
    #endregion

    #region Get+Set
    public static PlayerController Instance
    {
        get
        {
            return instance;
        }
    }
    public int Direction
    {
        get
        {
            return Direction1;
        }

        set
        {
            Direction1 = value;
        }
    }

    public bool Forward
    {
        get
        {
            return forward;
        }

        set
        {
            forward = value;
        }
    }

    public int Direction1
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
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

    #endregion

    #region Methods
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        attackTrigger1.enabled = false;
        attackTrigger2.enabled = false;
        attackTrigger3.enabled = false;
    }

    void Start ()
    {
        anim = GetComponent<Animator>();
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
	}
	
	void Update ()
    {
        ResetValues();
        HandleInput();
        HandleAttacks();
	}

    void ResetValues()
    {
        anim.SetBool("HorizMove", false);
        anim.SetBool("UpMove", false);
        anim.SetBool("DownMove", false);
        anim.SetInteger("Attack", 0);
    }

    void HandleInput()
    {
        FlipPlayer();
        if(Input.GetKey("d"))
        {
            Direction = 1;
            transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            anim.SetBool("HorizMove", true);
            
        }

        if (Input.GetKey("a"))
        {
            Direction = 1;
            transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
            anim.SetBool("HorizMove", true);
            
        }

        if (Input.GetKey("w"))
        {
            Direction = 2;
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            anim.SetBool("UpMove", true);
            
        }

        if (Input.GetKey("s"))
        {
            Direction = 3;
            transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
            anim.SetBool("DownMove", true);
            
        }

        if(Input.GetKeyDown("v"))
        {
            hero.switchWeapon();
        }

        if (Input.GetKeyDown("h"))
        {
            hero.Heal();
        }
    }

    void HandleAttacks()
    {
        if(Input.GetKeyDown("space"))
        {
            if (hero.CurrentWeapon == "Handgun" && hero.HandGunAmmo > 0)
            {
                Shoot();
                hero.HandGunAmmo -= 1;
            }
            else if(hero.CurrentWeapon == "Knife")
            {
                Melee();
            }
        }

        if (attacking)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else
            {
                attacking = false;
                attackTrigger1.enabled = false;
                attackTrigger2.enabled = false;
                attackTrigger3.enabled = false;
            }
        }
    }

    void Shoot()
    {
        if(Forward && Direction == 1)
        {
            Instantiate(thrown, throwPos.position, Quaternion.identity);
        }
        else if(!Forward && Direction ==1)
        {
            GameObject app = Instantiate(thrown, throwPos.position, Quaternion.identity);
            Vector3 bulletScale= app.transform.localScale;
            bulletScale.x = -bulletScale.x;
            app.transform.localScale = bulletScale;
        }
        else if(Direction == 2)
        {
            Instantiate(thrown, throwPos2.position, Quaternion.Euler(0, 0, 90));
        }
        else if (Direction == 3)
        {
            Instantiate(thrown, throwPos3.position, Quaternion.Euler(0, 0, 90));
        }
    }

    void Melee()
    {
        switch (Direction)
        {
            case 1:
                if (anim.GetBool("HorizMove") == true)
                {
                    anim.SetInteger("Attack", 1);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger1.enabled = true;
                    }
                }
                else if (!anim.GetBool("HorizMove") && Direction == 1)
                {
                    anim.SetInteger("Attack", 4);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger1.enabled = true;
                    }
                }
                break;
            case 2:
                if (anim.GetBool("UpMove") == true)
                {
                    anim.SetInteger("Attack", 2);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger2.enabled = true;
                    }
                }
                else if (!anim.GetBool("UpMove") && Direction == 2)
                {
                    anim.SetInteger("Attack", 5);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger2.enabled = true;
                    }
                }
                break;
            case 3:
                if (anim.GetBool("DownMove") == true)
                {
                    anim.SetInteger("Attack", 3);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger3.enabled = true;
                    }
                }
                else if (!anim.GetBool("DownMove") && Direction == 3)
                {
                    anim.SetInteger("Attack", 6);
                    if (!attacking)
                    {
                        attacking = true;
                        attackTimer = attackCoolDown;
                        attackTrigger3.enabled = true;
                    }
                }
                break;
        }
    }


    void FlipPlayer()
    {
        float movement = Input.GetAxis("Horizontal");
        
        if((movement > 0 && !Forward) || (movement < 0  && Forward))
        {
            Vector3 playerScale = transform.localScale;
            playerScale.x = -playerScale.x;
            transform.localScale = playerScale;
            Forward = !Forward;
        }
    }
    #endregion
}
