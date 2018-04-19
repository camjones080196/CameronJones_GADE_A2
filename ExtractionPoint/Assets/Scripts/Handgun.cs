using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour {

    public int ammo;
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Hero")
       {
            Hero hero = col.gameObject.GetComponent<Hero>();
            hero.OwnHandgun = true;
            hero.HandGunAmmo += ammo;
            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
       }
    }

}
