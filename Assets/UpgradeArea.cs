using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeArea : MonoBehaviour
{

    public GameObject UpgradeUI;
    public float time=.1f;


    public float timeLimit=2;
    public float currentTime;
    public float progress;
    public  Image ImageProgres;

 
    private void OnEnable() {
     currentTime=timeLimit;    
    }
   
    public void ProgressLoading(){
        if(currentTime>0){
    currentTime-=time;
   progress=currentTime/timeLimit;
   ImageProgres.fillAmount=progress;
        }
        else{
        
        Debug.Log("işlem yap"); 
        UpgradeUI.SetActive(true);
       currentTime=timeLimit;
        progress=currentTime/timeLimit;
         ImageProgres.fillAmount=progress;
        TriggerMannager.OnUpgrading-=ProgressLoading;
        }

      
    }
  
}
