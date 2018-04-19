using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {

    #region Variables
    int boundaryDamage = 2;
    int xpos;
    int ypos;
    int Range = 21;

    public float damageWait;
    public float startDamageWait;
    public float shrinkWait;
    public float startShrinkWait;
    public float scale;
    float pointX;
    float pointY;

    bool boundaryDamaging = false;
    bool boundaryShrinking = true;
   
    Hero hero = new Hero();
    Enemy enemy = new Enemy();

    public GameObject extractionPoint;

    Vector3 extractionPointPos;
    #endregion

    #region Methods
    void Start ()
    {
        StartCoroutine(shrinkBoundary());                   //Starts the coroutine that shrinks the boundary
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hero")                                                                       //Detects if the player leaves or is caught outside the boundary   
        {
            boundaryDamaging = true;                        
            hero = col.gameObject.GetComponent<Hero>();
            StartCoroutine(damageHero());                                                                   //Starts the coroutine to damage the hero
        }

        if (col.gameObject.tag == "Enemy")                                                                      //Detects if the enemy is out of the boundary
        {
            enemy = col.gameObject.GetComponent<Enemy>();
            enemy.movingToBoundary = true;
            StopCoroutine(stopEnemy(enemy));
            StartCoroutine(enemy.moveToBoundary());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hero")                   //Detects if the player returns to the boundary   
        {
            boundaryDamaging = false;
            StopCoroutine(damageHero());                //Stops the coroutine to damage the hero
        }

        if (col.gameObject.tag == "Enemy")                  //Stops moving the enemy once it is inside the boundary
        {
            enemy = col.gameObject.GetComponent<Enemy>();
            StartCoroutine(stopEnemy(enemy));
        }
    }

    public IEnumerator damageHero()                //Method to apply damage to the hero
    {
        yield return new WaitForSeconds(startDamageWait);   //Waits 30 seconds before applying damage 

        while (boundaryDamaging)
        {
            hero.Damage(boundaryDamage);                    //Calls the heroes damage method to apply damage
            yield return new WaitForSeconds(damageWait);    //Waits 1 second before repeating
        }

    }

    IEnumerator shrinkBoundary()
    {
        yield return new WaitForSeconds(startShrinkWait);                                   //Waits 120 seconds before the first shrink
        while (boundaryShrinking)
        {
            
            xpos = Random.Range(Range, -Range);                                         //Determines the boundaries x position
            ypos = Random.Range(Range, -Range);                                         //Determines the boundaries y position
            transform.localScale += new Vector3(-scale, -scale, 1);
            transform.position = new Vector3(xpos, ypos, -3);

            yield return new WaitForSeconds(shrinkWait);

            Range += 10;                                                                   

            if (gameObject.transform.localScale.x == 14)                                    
            {
                boundaryShrinking = false;
                StopCoroutine(shrinkBoundary());
                pointX = Random.Range(transform.position.x + transform.localScale.x, transform.position.x - transform.localScale.x);
                pointY = Random.Range(transform.position.y + transform.localScale.y, transform.position.y - transform.localScale.y);
                extractionPointPos = new Vector3(pointX, pointY, -1);
                Instantiate(extractionPoint, extractionPointPos, gameObject.transform.rotation);
            }
        }
    }

    IEnumerator stopEnemy(Enemy enemy)
    {
        yield return new WaitForSeconds(5);

        enemy.movingToBoundary = false;
        StopCoroutine(enemy.moveToBoundary());
    }
    #endregion
}
