using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractionPoint : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Hero")
        {
            Hero hero = col.gameObject.GetComponent<Hero>();
            if(hero.Enemies1.Length == 0)
            {
                OnNoEnemies();
            }
        }
    }

    public void OnNoEnemies()
    {
        EventManager.Instance.PostNotification(Events.NEWLEVEL, this);
    }
}
