using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _explosionprefab;

    [SerializeField]
    private GameObject _laserprefab;

    public bool canDoubleShot = false;

    [SerializeField]
    private GameObject _doubleshotprefab;

    [SerializeField]
    private GameObject _shieldprefab;

    public bool _isShieldEnabled = false;

    [SerializeField]
    private GameObject _shieldgameobj;

    public bool canSpeedBoost = false;
    private float boost = 0.0f;
    public int livesleft = 3;
    
    private float _canfire = 0.0f;
    [SerializeField]
    private float _fireRate = 0.25f;
    

    [SerializeField] //the variable remains private but visible in inspector
    private float _speed_ship = 10.00f ;

    private UIManager _uimanager;

    private GameManager _gamemanager;

    private spawn_manager _spawnmanager;
    private AudioSource _audiosource;

    [SerializeField]
    private GameObject[] _engines;

    private int _hitCount = 0;
    private int randomEngine;


/////////////Start is called once///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Start()
    {
        //current pos - default

        transform.position = new Vector3(0,0,0);

        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //this is for refreshing lives when starting
        /*if ( _uimanager != null) 
        {
            _uimanager.UpdateLives(livesleft);
        }*/

        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

         _spawnmanager = GameObject.Find("Spawn Manager").GetComponent<spawn_manager>();
        
        //because the player has spawned we have to start routine again
        if (_spawnmanager!= null)
        {
            _spawnmanager.StartSpawnroutines();
        }

        _audiosource = GetComponent<AudioSource>();

        _hitCount = 0;

    }



    // Update is called once per frame-------------------------------------------------------------------------------------------------
    private void Update()
    {

        Movement();

        if (Input.GetKeyDown(KeyCode.Space) ||  Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
        
    }




//------------------------------------shoot--------------------------------------------------------------------------------------------

    private void Shoot()
    {
        if (Time.time > _canfire)
        {
            

            _audiosource.Play();
            
             if (canDoubleShot == true)
            { 
                //power up
                
                /*Instantiate(_laserprefab , transform.position + new Vector3(-0.43f,1.31f,0) , Quaternion.identity );
                Instantiate(_laserprefab , transform.position + new Vector3(0.46f,1.31f,0) , Quaternion.identity ); */
                Instantiate(_doubleshotprefab , transform.position  , Quaternion.identity );

            
            }

        else
        {
            //spawn laser
            Instantiate(_laserprefab , transform.position + new Vector3(-0.67f,1.31f,0) , Quaternion.identity );
            Instantiate(_laserprefab , transform.position + new Vector3(0.693f,1.31f,0) , Quaternion.identity );
        }

        _canfire = Time.time + _fireRate;

        }

    }


//------------------------------------------- movement ------------------------------------------------------------------------
    private void Movement()
    {
        float horiz_Input = Input.GetAxis("Horizontal"); //returns -1 , 0 , 1 based on key pressed
        float vert_Input = Input.GetAxis("Vertical");


        if (canSpeedBoost == true)
        {
            boost = 1.5f;
        }
        else
        {
            boost = 1.0f;
        }

            //_speed_ship = _speed_ship * 1.5f ;
            //Debug.Log(_speed_ship);
            transform.Translate(Vector3.right * _speed_ship * horiz_Input * Time.deltaTime * boost);
            transform.Translate(Vector3.up * _speed_ship * vert_Input * Time.deltaTime * boost );
        
   
        //y axis bound
        if ( transform.position.y > -0.66f)
        {
            transform.position = new Vector3(transform.position.x , -0.66f , transform.position.z);
        }
        else if (transform.position.y < -4.17f)
        {
            transform.position = new Vector3(transform.position.x , -4.17f , 0);
        }

        //wrapping feature , x axis bound
        
        if (transform.position.x  < -10f)
        {
            transform.position = new Vector3(10.02f , transform.position.y , 0);
        } 
        else if (transform.position.x > 10.02f)
        {
            transform.position = new Vector3(-10f, transform.position.y , 0);
        }
        
   
    }


//------------------------------------------------------------------------------------------

    public void Damage()
    {



        if ( _isShieldEnabled ==  true)
        {
            _isShieldEnabled = false;
            _shieldgameobj.SetActive(false);
            return;
        }

        //works only if shield isnt active
         _hitCount++ ;
        //engine failures

        if (_hitCount == 1)
        {
            randomEngine = Random.Range(0 , 2); 
            _engines[randomEngine].SetActive(true);

        }
        else if (_hitCount == 2)
        {
            _engines[randomEngine - 1].SetActive(true); // active other engine failure

        }

        //-----------------
        livesleft -= 1;
        _uimanager.UpdateLives(livesleft , true); //updates sprites



        if (livesleft <1)
        {
            Instantiate( _explosionprefab , transform.position , Quaternion.identity); // plays animation for player explosion
            Destroy(this.gameObject ); //destroys player

            _gamemanager.gameOver = true;

           _uimanager.DelayandTitle(); //waits for 5 seconds after player gets destroyed and shows title screen

            
            //_uimanager.ShowTitle();
        }
        
    }

//--------------------------------shields--------------------------------------------
    public void EnableShields()
    {
        _isShieldEnabled = true;
        _shieldgameobj.SetActive(true); //shield visuals set to true
    }   


//--------------------------speed boost ---------------------------------------------------
    public void SpeedPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost=false;
    }

//--------------------------double shot boost ---------------------------------------------------
    public void DoubleShotPowerupOn()
    {
        canDoubleShot = true;
        StartCoroutine(DoubleShotPowerDownRoutine());
    }

    public IEnumerator DoubleShotPowerDownRoutine()
    { 
        yield return new WaitForSeconds(5.0f);
        canDoubleShot = false;
    }
//------------------------------------------------------------------------------------------

    public void CreateDelay()
    {
          StartCoroutine(delayAndTitle());

    }
    public IEnumerator delayAndTitle()
    {
        Debug.Log("Comes here , delay about to start");
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Comes here redeyes");
        _uimanager.ShowTitle(); 

    }



}

