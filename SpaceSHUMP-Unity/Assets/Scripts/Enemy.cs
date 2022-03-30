/**** 
 * Created by: Jason Alfrey
 * Date Created: March 21, 2022
 * 
 * Last Edited by: NA
 * Last Edited: March 21, 2022
 * 
 * Description: Enemy controler
****/

/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase] //forces selection of parent object
public class Enemy : MonoBehaviour
{
    /*** VARIABLES ***/

    [Header("Enemy Settings")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck; //reference to bounds check component
    
    //method that acts as a field (property)
    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }//end Awake()


    // Update is called once per frame
    void Update()
    {
       Move();

        //Check if bounds check exists and the object is off the bottom of the screne
        if(bndCheck != null && bndCheck.offDown)
        {
              Destroy(gameObject); //destory the object

        }//end if(bndCheck != null && !bndCheck.offDown)


    }//end Update()

    //Virtual methods can be overiden by child instances

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Debug.Log("Enemy Hit by Projectile Hero " + otherGO.name);
            Destroy(otherGO);
            Hero.SHIP.AddScore(score);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Enemy Hit by non-Projectile Hero " + otherGO.name);
        }
    }
}
