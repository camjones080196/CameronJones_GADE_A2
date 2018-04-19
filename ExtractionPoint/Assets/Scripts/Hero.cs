using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour {

    #region Variables
    public float maxhp = 100;
    private float hp;
    int healthPacks = 0;
    int handGunAmmo = 0;
    int score = 0;

    bool ownHandgun = false;

    string currentWeapon;

    public Text weaponText;
    public Text ammoText;
    public Text healthPackText;
    public Text hpText;
    public Text scoreText;

    GameObject[] Enemies;

    #endregion

    #region Get+Set
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

            if (value < 0)
            {
                hp = 0;
                OnNoHP();
            }
        }
    }

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

    public int HandGunAmmo
    {
        get
        {
            return handGunAmmo;
        }

        set
        {
            handGunAmmo = value;
        }
    }

    public int HealthPacks
    {
        get
        {
            return healthPacks;
        }

        set
        {
            healthPacks = value;
        }
    }

    public bool OwnHandgun
    {
        get
        {
            return ownHandgun;
        }

        set
        {
            ownHandgun = value;
        }
    }

    public string CurrentWeapon
    {
        get
        {
            return currentWeapon;
        }

        set
        {
            currentWeapon = value;
        }
    }

    public GameObject[] Enemies1
    {
        get
        {
            return Enemies;
        }

        set
        {
            Enemies = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    #endregion

    #region Methods
    void Start ()
    {
        CurrentWeapon = "Knife";
        hp = maxhp;
        Enemies1 = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	void Update ()
    {
        setScore();
        setHP();
        setAmmo();
        setWeapon();
        setHealthPacks();
        Enemies1 = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void Damage(int damage)
    {
        Hp -= damage;
    }

    public void switchWeapon()
    {
        if(CurrentWeapon == "Knife" && ownHandgun)
        {
            CurrentWeapon = "Handgun";
            Debug.Log(CurrentWeapon);
        }
        else if(CurrentWeapon == "Handgun")
        {
            CurrentWeapon = "Knife";
            Debug.Log(CurrentWeapon);
        }
    }

    public void Heal()
    {
        if(HealthPacks > 0)
        {
            Hp += 25;
            HealthPacks -= 1;
        }
    }

    public void setScore()
    {
        string score = ((int)Score).ToString();
        scoreText.text = "Score: " + score;
    }

    public void setHP()
    {
        string health = ((float)Hp).ToString();
        hpText.text = "Health: " + health;
    }

    public void setHealthPacks()
    {
        string healthPack = ((int)HealthPacks).ToString();
        healthPackText.text = "Health Packs: " + healthPack;
    }

    public void setAmmo()
    {
        string ammo = ((int)HandGunAmmo).ToString();
        ammoText.text = "Ammo: " + ammo;
    }

    public void setWeapon()
    {
        weaponText.text = "Weapon: " + CurrentWeapon;
    }

    public void OnNoHP()
    {
        EventManager.Instance.PostNotification(Events.DEADHERO, this);
    }
    #endregion
}
