/**** 
 * Created by: Jason Alfrey
 * Date Created: March 30, 2022
 * 
 * Last Edited by: NA
 * Last Edited: April 6, 2022
 * 
 * Description: Projectile behaviors.
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Variables 
    private BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bndCheck.offUp)
        {
            gameObject.SetActive(false);
            bndCheck.offUp = false; // Reset the boundary settings
        }
    }
}
