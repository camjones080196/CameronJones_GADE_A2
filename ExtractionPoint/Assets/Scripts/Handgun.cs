using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handgun : MonoBehaviour {
    
    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Hero")
       {
            Hero hero = col.gameObject.GetComponent<Hero>();
            hero.OwnHandgun = true;
            hero.HandGunAmmo += 50;
            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
       }
    }

}
