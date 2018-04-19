using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int ammo;

    private IEnumerator OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hero")
        {
            Hero hero = col.gameObject.GetComponent<Hero>();
            hero.HandGunAmmo += ammo;
            yield return new WaitForSeconds(1);
            Destroy(this.gameObject);
        }
    }
}
