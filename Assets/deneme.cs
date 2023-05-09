using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class deneme : MonoSingleton<deneme>
{

    public GameObject[] SpawnPoints;
    public Transform[] mps;
    
    
    private float spawnWait;
    public float spawnMostWait;
    public float spawnLessWait;

   
    int randPoint;
    public GameObject spawnee;
    
   
   
    private void Start()
    {
        
        StartCoroutine(waitSpawner());
        Instantiate(spawnee, SpawnPoints[0].transform.position, SpawnPoints[0].transform.rotation);
        //sil deneme ama�l�
    
    }
    


    IEnumerator waitSpawner()
    {
        spawnWait = Random.Range(spawnLessWait, spawnMostWait);

        yield return new WaitForSeconds(spawnWait);

       

            //randPoint = Random.Range(0, 3);
            int index = Random.Range(0, SpawnPoints.Length);
            //int objindex = Random.Range(0, spawnee.Length);
            //Vector3 spawnPosition = new Vector3(Random.Range();
            
            Instantiate(spawnee, SpawnPoints[index].transform.position, SpawnPoints[index].transform.rotation);

        StartCoroutine(waitSpawner());
        
    }

   
}
