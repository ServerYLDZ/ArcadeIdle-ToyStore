using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;
using DG.Tweening;


public class Worker : MonoBehaviour
{
    public AICollectAndDrop collectManager;
    public NavMeshAgent navMeshAgent;
    public int workerLevel=0;
    public GameObject basket;
    public Animator anim;
   
   public bool isStop;


    public int index;
private void Awake() {
    navMeshAgent=GetComponent<NavMeshAgent>();

   basket.GetComponent<MeshFilter>().mesh=GameManager.Instance.allMeshs.basketMesh;
     if(GameManager.Instance.allMeshs.basketMesh)
       basket.SetActive(true);
     else 
       basket.SetActive(false);
    navMeshAgent.speed=GameManager.Instance.currentWorkerSpeed;
}
    private void OnEnable()
    {
      
     
    }
    private void Update()
    {
         
  if (GameManager.Instance.vali == 0)
            {
                navMeshAgent.SetDestination(this.transform.position);
                anim.SetBool("Walk",false);
                isStop=true;
                Debug.Log("0");

            }
   else{
        if (collectManager.collectList.Count>=collectManager.collectLimit )
        {
               
                int rand = index % GameManager.Instance.deskTransforms.Count;
               
              
                    float dis= Vector3.Distance(this.transform.position,GameManager.Instance.deskTransforms[rand].transform.position);
                   
                     navMeshAgent.SetDestination(GameManager.Instance.deskTransforms[rand].transform.position);
                    transform.DOMove(GameManager.Instance.deskTransforms[rand].transform.position,(float)dis*3/GameManager.Instance.currentWorkerSpeed);
                    
                    
                   
                  
                    anim.SetBool("Walk", true);
                     isStop=false;
                   
                  

               
            
         

        }
        else if (collectManager.collectList.Count == 0 )
        {
           
                
           
                int rand = index % GameManager.Instance.stackTransforms.Count;

                    float dis=Vector3.Distance(this.transform.position,GameManager.Instance.stackTransforms[rand].transform.position);
                    navMeshAgent.SetDestination(GameManager.Instance.stackTransforms[rand].transform.position);
                    transform.DOMove(GameManager.Instance.stackTransforms[rand].transform.position,(float)dis*3/GameManager.Instance.currentWorkerSpeed);
                 
                     
                  
                    anim.SetBool("Walk", true);
                      isStop=false;
                  
                
               
            
                    
            
        }
   }         
        
     
    }

}
