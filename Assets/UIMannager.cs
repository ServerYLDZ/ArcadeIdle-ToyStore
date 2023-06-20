using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIMannager : MonoBehaviour//obsever yani g�zlemcimiz
{
     [SerializeField] private TMP_Text MoneyText;
    [SerializeField] private TMP_Text SalaryTimeRemaining;
     [SerializeField] private TMP_Text SalaryCountText;
    public int salaryRemainTime;
    public int salaryMax=300;
   private void Start() {
    GameManager.Instance.OnCollectedMoney += SetMoneyText;
    salaryRemainTime=salaryMax;
    StartCoroutine(salaryTime());
   }
  

    void SetMoneyText(int money) {
if(money>=1000){

    string st ="";
   st+= (money/1000).ToString()+".";
   if((money%1000)<100){
    st+="0"+((money%1000)/10).ToString()+"K";
   }
   else{
    st+=((money%1000)/10).ToString()+"K";
   }
   
MoneyText.text = st;

}


else
    MoneyText.text = money.ToString();
    SaveLoadManager.Instance.SaveState();
    } 
    IEnumerator salaryTime(){
        SalaryCountText.text=  "-"+GameManager.Instance.workerSalaryCount.ToString();
        yield return new WaitForSeconds(1);
        if(salaryRemainTime>0){
            salaryRemainTime--;
            SalaryTimeRemaining.text=(salaryRemainTime/60).ToString()+":"+(salaryRemainTime%60).ToString();
        }
        else
        salaryRemainTime=salaryMax;
        
        StartCoroutine(salaryTime());
    }

}
