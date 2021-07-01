using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_AI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyexplosionprefab;

    private UIManager _uimanager;
    [SerializeField]
    private AudioClip _clip;

//------------------------------------------------------------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            //decrease life and destroy enemy
            Player player_script = other.GetComponent<Player>();
            if (player_script!= null)
            {
                player_script.Damage();
                Debug.Log("Lives left " + player_script.livesleft);

            }

        }
        

        else if (other.tag == "Laser")
        {
            //Laser laser_script = other.GetComponent<Laser>();
            if (other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject); //destroys laser

            _uimanager.UpdateScore(10); //updates score

        }

        Instantiate(_enemyexplosionprefab , transform.position , Quaternion.identity );  //intiates the enemy explosion 
        AudioSource.PlayClipAtPoint(_clip , Camera.main.transform.position , 1f ); //plays explosion sound instantiated it so that it plays before destroying this object
        Destroy(this.gameObject); //destroys enemy in both cases
        
    }

//-----------------------------------------------------------------------------------------------------------------------------------------------------


    void Start()
    {
        
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uimanager != null)
        {
            _uimanager.UpdateScore(0); //starts score with zero
        }


    }

    // Update is called once per frame------------------------------------------------------------------------------------------------
    void Update()
    {
     
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        //6.36 ,  -7.76 , 7.79
        if (transform.position.y <= -6.37f)
        {
            float randomX = Random.Range(-7.76f , 7.79f);
            transform.position = new Vector3( randomX , 6.36f , 0);
        }




    }
}
