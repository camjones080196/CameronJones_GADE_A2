using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHitbox : MonoBehaviour {
    #region Variables
    Hero hero;
    #endregion



    #region Methods
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
    }

    private void OnTriggerEnter2D(Collider2D col)  //Method to detect collisions
    {
        if (col.gameObject.tag == "Enemy")           //If the colliders tag is Enemy then damage is done to that enemy
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>(); 
            enemy.Damage(3);
            hero.Score += 4;
        }
    }
    #endregion
}
