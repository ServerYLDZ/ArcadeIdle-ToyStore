using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMannager : MonoBehaviour
{

    public delegate void OnCollectArea();
    public delegate void OnDeskArea();
    public static event OnCollectArea OnCollectStarted;
    public static event OnDeskArea OnDroppingStarted;
    public delegate void OnMoneyArea();
    public static event OnMoneyArea OnMoneyCollect;
    public delegate void OnBuyArea();
    public static event OnBuyArea OnBuying;

       public delegate void OnUpgradeArea();
    public static event OnUpgradeArea OnUpgrading;

    bool isCollecting;
    bool isDropping;
    bool isMonneyCollecting;


    public static StackObject stackObj;
    public static DeskArea deskArea;
    public static BuyArea toBuyArea;
    public static UpgradeArea toUpgradeArea;
   public static Otomat  otomoatArea;
    

    private void Start()
    {
        StartCoroutine(CollectAndDrop());
        StartCoroutine(collectMoneyEnuamrator());// bunu diğerinin üzerine taşıyabilriim optimizasyon açısınsından
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="UpgradeArea"){
            toUpgradeArea=other.GetComponent<UpgradeArea>();
            TriggerMannager.OnUpgrading+=toUpgradeArea.ProgressLoading;
       
            
         }  
         
         
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Collectable") 
        {
            stackObj = other.GetComponent<StackObject>();
            
            isCollecting = true;
        }
        if (other.tag == "DeskArea")
        {
            deskArea = other.GetComponent<DeskArea>();
          
            isDropping = true;
        }
        if (other.tag == "MoneyArea")
        {
            if(other.GetComponentInParent<DeskArea>()!=null)
             deskArea = other.GetComponentInParent<DeskArea>();
             else
             otomoatArea =other.GetComponentInParent<Otomat>();

            isMonneyCollecting = true;

        }
        if (other.tag == "BuyArea")
        {
            toBuyArea = other.GetComponent<BuyArea>();
            OnBuying();
          

        }
        if(other.tag=="UpgradeArea"){
            toUpgradeArea=other.GetComponent<UpgradeArea>();
            if(OnUpgrading!=null)
                OnUpgrading();
            
         }
         
        
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable") 
        {
            isCollecting = false;
            stackObj = null;
        }
        if (other.tag=="DeskArea")
        {
            isDropping = false;
            deskArea = null;
        }
        if (other.tag == "MoneyArea")
        {
            isMonneyCollecting = false;
            otomoatArea=null;
            deskArea=null;
        }
        if (other.tag == "BuyArea")
        {
            toBuyArea = null;
           

        }
         if(other.tag=="UpgradeArea"){
             TriggerMannager.OnUpgrading-=toUpgradeArea.ProgressLoading;
                toUpgradeArea.currentTime=    toUpgradeArea.timeLimit;
              toUpgradeArea.progress=    toUpgradeArea.currentTime/ toUpgradeArea.timeLimit;
              toUpgradeArea.ImageProgres.fillAmount=    toUpgradeArea.progress;
              toUpgradeArea.UpgradeUI.SetActive(false);
            toUpgradeArea=null;

          
        }
        
    }
    IEnumerator CollectAndDrop()
    {
        while (true)
        {
            if (isCollecting)
            {
            
             OnCollectStarted();

            }
            if (isDropping)
            {
               
                OnDroppingStarted();
            }
          
            
            yield return new WaitForSeconds(.1f);
        }
    }
    IEnumerator collectMoneyEnuamrator(){
        while (true)
        {
           
            if (isMonneyCollecting)
            {
                if(deskArea!=null)
                deskArea.CollectMoney();
                else
                otomoatArea.CollectMoney();
                //OnMoneyCollect();
            }
            
            yield return new WaitForSeconds(.05f);
        }
    }
  
}
