using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PI_UIManager : MonoBehaviour
{
  public TMP_Text timeText;
   public TMP_Text CapasityPriceText;
   public TMP_Text speedPriceText;
  public GameObject [] playerTimeCheckBoxs; //save+
  public GameObject [] playerCapasityCheckBoxs; //save+
    public GameObject [] playerSpeedCheckBoxs; //save+

private void Awake() {
  for(int i=0;i<GameManager.Instance.playerImproveTimeIndex;i++){
    playerTimeCheckBoxs[i].SetActive(true);
  }
   for(int i=0;i<GameManager.Instance.playerSpeedIndex;i++){
   playerSpeedCheckBoxs[i].SetActive(true);
  }
   for(int i=0;i<GameManager.Instance.playerCapasityIndex;i++){
    playerCapasityCheckBoxs[i].SetActive(true);
  }
}
      private void OnEnable() {
   SetPlayerTimeImprovePirce();
    SetPlayerCapasityPrice();
    SetPlayerSpeedPrice();
    GameManager.Instance.canMovePlayer=false;
  }
  private void OnDisable() {
      GameManager.Instance.canMovePlayer=true;
  }
  public void SetPlayerTimeImprovePirce(){

    if(GameManager.Instance.playerMaxImproveTime ==GameManager.Instance.playerImproveTimeIndex){
 timeText.text="Max";
 return;
    }
  
    if(GameManager.Instance.playerImproveTimePrice>=1000)
    timeText.text=(GameManager.Instance.playerImproveTimePrice/1000).ToString()+"K";
   

    else
    timeText.text=(GameManager.Instance.playerImproveTimePrice).ToString();

  }
  public void SetPlayerCapasityPrice(){
     if(GameManager.Instance.playerMaxCapasity==GameManager.Instance.playerCapasityIndex){
CapasityPriceText.text="Max";
return;
     }
      

    if(GameManager.Instance.playerCapasityPrice>=1000)
    CapasityPriceText.text=(GameManager.Instance.playerCapasityPrice/1000).ToString()+"K";
   
    else
   CapasityPriceText.text=(GameManager.Instance.playerCapasityPrice).ToString();
  }
    public void SetPlayerSpeedPrice(){

if(GameManager.Instance.playerMaxSpeed==GameManager.Instance.playerSpeedIndex){
 speedPriceText.text="Max";
 return;
}
     

    if(GameManager.Instance.playerSpeedPrice>=1000)
    speedPriceText.text=(GameManager.Instance.playerSpeedPrice /1000).ToString()+"K";
    
    else
   speedPriceText.text=(GameManager.Instance.playerSpeedPrice).ToString();
  }

}
