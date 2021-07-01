using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] powerups ; //array of all powerups


    [SerializeField]
    private bool flag = true;

    private GameManager _gamemanager;



    void Start()
    {
        _gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        EnemySpawnOn(); //starts coroutine for enemy spawn
        PowerupSpawnOn(); // starts coroutine for powerups
    }


    public void StartSpawnroutines()
    {
        EnemySpawnOn(); //starts coroutine for enemy spawn
        PowerupSpawnOn(); // starts coroutine for powerups

    }

//=================================================enemy spawn================================================
    public void EnemySpawnOn()
    {
        StartCoroutine(Enemy_Spawn());
    }

    public IEnumerator Enemy_Spawn()
    {

        while(_gamemanager.gameOver == false)
        {
            float randomX = Random.Range(-7.76f , 7.79f); //chooses a random value to spawn
            Instantiate( _enemyShipPrefab , new Vector3( randomX , 6.36f , 0) , Quaternion.identity);
            yield return new WaitForSeconds(5.0f);

        }
    }

//===================================powerup spawn================================================

    public void PowerupSpawnOn()
    {
        StartCoroutine(Powerup_Spawn());
    }

    public IEnumerator Powerup_Spawn()
    {
        while(_gamemanager.gameOver == false)
        {
            int randomPowerup = Random.Range(0 , 3);
            float randomX = Random.Range(-7.76f , 7.79f); //chooses a random value to spawn
            Instantiate( powerups[randomPowerup] , new Vector3( randomX , 6.36f , 0) , Quaternion.identity);
            yield return new WaitForSeconds(5.0f);

        }
    }

//--------------------------------------------------------------------------------------------------------------------------------

}
