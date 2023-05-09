using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpgrade : MonoBehaviour
{
  public TMP_Text PriceText;
   public TMP_Text CapasityPriceText;
   public TMP_Text speedPriceText;
  public GameObject [] workerCheckBoxs; //save+
  public GameObject [] workerCapasityCheckBoxs; //save+
    public GameObject [] workerSpeedCheckBoxs;//save+


private void Awake() {
  for(int i=0;i<GameManager.Instance.wIndex;i++){
workerCheckBoxs[i].SetActive(true);
  }
   for(int i=0;i<GameManager.Instance.speedIndex;i++){
  workerSpeedCheckBoxs[i].SetActive(true);
  }
   for(int i=0;i<GameManager.Instance.capasityIndex;i++){
 workerCapasityCheckBoxs[i].SetActive(true);
  }
}
  private void OnEnable() {
    SetWorkerTextPirce();
    SetWokerCapasityPrice();
    SetWorkerSpeedPrice();
    GameManager.Instance.canMovePlayer=false;
  }
  private void OnDisable() {
    GameManager.Instance.canMovePlayer=true;
  }
  public void SetWorkerTextPirce(){

    if(GameManager.Instance.maxWorkerCount==GameManager.Instance.wIndex){
 PriceText.text="Max";
 return;
    }
  
    if(GameManager.Instance.workerPrice>=1000)
    PriceText.text=(GameManager.Instance.workerPrice/1000).ToString()+"K";
   

    else
    PriceText.text=(GameManager.Instance.workerPrice).ToString();

  }
  public void SetWokerCapasityPrice(){
     if(GameManager.Instance.maxCapasityIncrese==GameManager.Instance.capasityIndex){
CapasityPriceText.text="Max";
return;
     }
      

    if(GameManager.Instance.capasityPrice>=1000)
    CapasityPriceText.text=(GameManager.Instance.capasityPrice/1000).ToString()+"K";
   
    else
   CapasityPriceText.text=(GameManager.Instance.capasityPrice).ToString();
  }
    public void SetWorkerSpeedPrice(){

if(GameManager.Instance.maxSpeedCount==GameManager.Instance.speedIndex){
 speedPriceText.text="Max";
 return;
}
     

    if(GameManager.Instance.speedPirce>=1000)
    speedPriceText.text=(GameManager.Instance.speedPirce /1000).ToString()+"K";
    
    else
   speedPriceText.text=(GameManager.Instance.speedPirce).ToString();
  }

   
}
