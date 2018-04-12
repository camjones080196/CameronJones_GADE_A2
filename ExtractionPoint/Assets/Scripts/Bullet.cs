using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    #region Variables
    public float speed;

    private Rigidbody2D bullet;
    private Vector2 direction;

    Hero hero;
    #endregion

    void Start () {
        hero = GameObject.FindGameObjectWithTag("Hero").GetComponent<Hero>();
        bullet = GetComponent<Rigidbody2D>();
        direction = SetDir(GameObject.Find("Player").GetComponent<PlayerController>().Forward, GameObject.Find("Player").GetComponent<PlayerController>().Direction);
	}
	
	void Update () {
        bullet.velocity = direction * speed * Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Enemy enemy = col.gameObject.GetComponent<Enemy>();
            Destroy(gameObject);
            enemy.Damage(5);
            hero.Score += 2;
        }
    }

    public Vector2 SetDir(bool forward, int direction)
    {
        if (forward && direction == 1)
            return Vector2.right;
        else if (!forward && direction == 1)
            return Vector2.left;
        else if (direction == 2)
            return Vector2.up;
        else if (direction == 3)
            return Vector2.down;
        else
            return Vector2.right;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
