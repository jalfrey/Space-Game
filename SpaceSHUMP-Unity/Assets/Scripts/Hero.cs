/**** 
 * Created by: Jason Alfrey
 * Date Created: March 21, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 11, 2022
 * 
 * Description: Hero ship controller
****/

/** Using Namespaces **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase] //forces selection of parent object
public class Hero : MonoBehaviour
{
    /*** VARIABLES ***/
    #region PlayerShip Singleton
    static public Hero SHIP; //refence GameManager
    public AudioClip projectileSound; 
    AudioSource audioSource;

    //Check to make sure only one gm of the GameManager is in the scene
    void CheckSHIPIsInScene()
    {
        //Check if instnace is null
        if (SHIP == null)
        {
            SHIP = this; //set SHIP to this game object
        }
        else //else if SHIP is not null send an error
        {
            Debug.LogError("Hero.Awake() - Attempeeted to assign second Hero.SHIP");
        }
    }//end CheckGameManagerIsInScene()
    #endregion

    GameManager gm; //reference to game manager
    ObjectPool pool; // reference to pool

    [Header("Ship Movement")]
    public float speed = 10;
    public float rollMult = -45;
    public float pitchMult = 30;
    
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Space(10)]

    private GameObject lastTriggerGo; //reference to the last triggering game object
   
    [SerializeField] //show in inspector
    private float _shieldLevel = 1; //level for shields
    public int maxShield = 4; //maximum shield level
    
    //method that acts as a field (property), if the property falls below zero the game object is desotryed
    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, maxShield); //Min returns the smallest of the values, therby making max sheilds 4

            //if the sheild is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                Debug.Log(gm.name);
                gm.LostLife();
                
            }

        }
    }

    /*** MEHTODS ***/

    //Awake is called when the game loads (before Start).  Awake only once during the lifetime of the script instance.
    void Awake()
    {
        CheckSHIPIsInScene(); //check for Hero SHIP
    }//end Awake()

    //Start is called once before the update
    private void Start()
    {
        gm = GameManager.GM; //find the game manager
        pool = ObjectPool.POOL;
        audioSource = GetComponent<AudioSource>();
    }//end Start()

    // Update is called once per frame (page 551)
    void Update()
    {
        // player input
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        // Change the transform based on the axis
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;

        transform.position = pos;

        // Rotate the ship to create a dynamic feel
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }

    }//end Update()


    //Taking Damage
    private void OnTriggerEnter(Collider other)
    {
        // The transform root returns the topmost root in the hierarchy
        Transform rootT = other.gameObject.transform.root;
        Debug.Log("Triggered " + other.gameObject.name);
        GameObject obj = rootT.gameObject;

        if (obj == lastTriggerGo)
            return;

        // Set the trigger to the last trigger
        lastTriggerGo = obj;
        if (obj.tag == "Enemy")
        {
            Debug.Log("Triggered by Enemey " + other.gameObject.name);
            shieldLevel--;
            Destroy(obj);
        }
        else
        {
            Debug.Log("Triggered by non-Enemy " + other.gameObject.name);
        }

    }//end OnTriggerEnter()

    void TempFire()
    {
        GameObject projGO = pool.GetObject();

        if(projGO != null)
        {
            if (audioSource != null && projectileSound != null)
                audioSource.PlayOneShot(projectileSound);

            projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rb = projGO.GetComponent<Rigidbody>();
            rb.velocity = Vector3.up * projectileSpeed;
        }
    }

    public void AddScore(int value)
    {
        gm.UpdateScore(value);
    }
}
