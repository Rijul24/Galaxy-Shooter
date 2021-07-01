using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;

    public GameObject player;

    private UIManager _uimanager;

    private void Start()
    {
        _uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Update()
    {
        if ( gameOver == true)
        { 
            //on title screen
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(player , Vector3.zero , Quaternion.identity);
                gameOver = false;

                _uimanager.HideTitle(); //starts game and hides title

                _uimanager.UpdateLives( 3, true);



            }
        

        }
    }



}
