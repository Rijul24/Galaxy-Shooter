using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private int powerupID; //0 - double shot , 1- speed boost , 2-shields

    [SerializeField]
    private AudioClip _clip;

    //one of the colliding obj must have rigid body
    private  void OnTriggerEnter2D(Collider2D other)
    {
        //OTHER = the game obj which collided with this
        //Debug.Log("here" + other.name);

        if (other.tag == "Player") //obj with tag player only trigger this
        {
            Player player_script = other.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip , Camera.main.transform.position , 1f); //plays pickup sound

            if (player_script != null)
            {
                if (powerupID == 0)
                {
                //enables power up and starts coroutine
                 player_script.DoubleShotPowerupOn();
                }
                else if (powerupID ==1 )
                {
                 //enable speed boost
                player_script.SpeedPowerupOn();
                }
                else if (powerupID ==2 )
                {
                 //enable shield
                player_script.EnableShields();
                 }
            }

            //destroy
            Destroy(this.gameObject);
            //nothing below it will be called
        }



    }

    // Update is called once per frame
    void Update()
    {
    
    transform.Translate(Vector3.down * _speed * Time.deltaTime);    

    if (transform.position.y <= -6.37f)
    {
        Destroy(this.gameObject); //destroys off screen powerups

    }


    }
}
