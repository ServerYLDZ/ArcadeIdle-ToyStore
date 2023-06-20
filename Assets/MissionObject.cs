using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class MissionObject : MonoBehaviour
{
    public NavMeshAgent nma;
    public int lifeTime=10;
    public Animator anim;
    public bool isRecive=true;

     
    private void Awake()
    {
        GameManager.Instance.player.GetComponent<PlayerOP>().missionArow.SetActive(true);
        nma = GetComponent<NavMeshAgent>();
      
        StartCoroutine(lifeOrDead());
    }
    void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.player.GetComponent<PlayerOP>().missionArow.SetActive(false);
            Destroy(this.gameObject);
            anim.SetBool("Run", false);
           GameManager.Instance.vali += 10;
        }

        //mission completes
    }
    private void Update()
    {

       
          GameManager.Instance.player.GetComponent<PlayerOP>().missionArow.transform.LookAt(this.gameObject.transform.position);
        if (isRecive)
        {
            int index = Random.Range(0,deneme.Instance.mps.Length);
               anim.SetBool("Run",true);
               if(nma && deneme.Instance.mps[index])
               {
                 nma.SetDestination(deneme.Instance.mps[index].transform.position);
               }
               
                isRecive = false;
                StartCoroutine(setRechive());
            
       
        }



      
        
    }
    IEnumerator setRechive()
    {

        yield return new WaitForSeconds(2);
        if(!isRecive)
        isRecive = true;
      

    }

    IEnumerator lifeOrDead()
    {

        yield return new WaitForSeconds(lifeTime);
        GameManager.Instance.player.GetComponent<PlayerOP>().missionArow.SetActive(false);
        Destroy(this.gameObject);
        GameManager.Instance.vali -= 10;
        //mission faild
    }










}
