using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    #region Variables
    PlayerController hero;
    Enemy enemy;
    #endregion
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "WaterTrigger")
        {
            hero = col.gameObject.GetComponentInParent<PlayerController>();
            hero.Speed -= 2;
        }
        if (col.gameObject.tag == "Enemy")
        {
            enemy = col.gameObject.GetComponent<Enemy>();
            enemy.Speed -= 2;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "WaterTrigger")
        {
            hero = col.gameObject.GetComponentInParent<PlayerController>();
            hero.Speed += 2;
        }
        if (col.gameObject.tag == "Enemy")
        {
            enemy = col.gameObject.GetComponent<Enemy>();
            enemy.Speed = 2;
        }
    }
}
