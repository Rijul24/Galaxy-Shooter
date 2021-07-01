using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Sprite[] lives_sprites; //stores the lives sprites , size = 4
    public Image livesImageDisplay; //gets access to image component
    public Text ScoreText; //ref to text 
    public GameObject Title_Screen;
    public GameObject TitleExtras;
    public GameObject Player_Lives;


    public int Score = 0;
    public int incr_score= 10;



    public void UpdateLives(int currLives , bool gameStarted)
    {
        if (gameStarted == true)
        {
        // only when title is hid , the  lives start to show

        Debug.Log("Player Lives: " + currLives);
        
        
        livesImageDisplay.sprite = lives_sprites[currLives]; // updates the sprite
        
        }



    }

    public void UpdateScore(int increment)
    {
        Score += increment ;

        ScoreText.text = "Score: " + Score; //updates score on screen


    }

    public void ShowTitle()
    {
        Title_Screen.SetActive(true);
    }

    public void HideTitle()
    {
        Title_Screen.SetActive(false);
        HideTitleElements();
        ScoreText.text =  "Score: " ; //resets Score

        Player_Lives.SetActive(true); //shows lives on screen

    }
    

    public void DelayandTitle()
    {
          StartCoroutine(createdelay());

    }
    public IEnumerator createdelay()
    {
        Debug.Log("Comes here , delay about to start");
        yield return new WaitForSeconds(3.0f);


        Player_Lives.SetActive(false); // hides lives from screen

        ShowTitle(); 
        ShowTitleElements();
    }


    public void ShowTitleElements()
    {
        TitleExtras.SetActive(true);
    }

    public void HideTitleElements()
    {
        TitleExtras.SetActive(false);
    }
}

