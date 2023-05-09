using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyArea : MonoBehaviour
{
    public Image progressImg;
    public TMP_Text costText;
    public GameObject BuyObj, UIObj;
    public float cost, currentMoney, progress;
    public bool isBuyingComplate = false;
    public int decreseCount=1;
    
    public int index=0;
    public BuyArea instance;
    private void Awake() {
     
  
       string str=PlayerPrefs.GetString("BuyArea"+index);
       
       if(str=="True"){
        this.enabled = false;
        BuyObj.SetActive(true);
        UIObj.SetActive(false);
        isBuyingComplate=true;
        
        addGameManager();
       }
     
    }
    private void Start()
    {
        costText.text = cost.ToString();
    }
    public void Buy(int moneyAmount)
    {
        currentMoney += moneyAmount;
        progress = currentMoney / cost;
        costText.text = (cost - currentMoney).ToString();
        progressImg.fillAmount = 1-progress;
        if (progress >= 1)
        {
            BuyObj.SetActive(true);
            UIObj.SetActive(false);
            this.enabled = false;
            isBuyingComplate = true;
           PlayerPrefs.SetString("BuyArea"+index,isBuyingComplate.ToString());
              
         addGameManager();
               
            
     
        }

    }

    private void addGameManager(){
if (BuyObj.GetComponent<DeskArea>()==null && BuyObj.GetComponent<Otomat>()==null && BuyObj.GetComponent<Employe>()==null )//  o zaman stack nesnesidir ve işcilere yeni stak nesnesinin loaksyonunu bildirmemiz gerekir
                {
                   GameManager.Instance.stackTransforms.Add(BuyObj);
                  // GameManager.Instance.gm_stackObject.Add(BuyObj);
                    BuyObj.GetComponent<StackObject>().spawnTime=GameManager.employeIncreseTimeCount;
                      BuyObj.GetComponent<MeshRenderer>().material=GameManager.Instance.allMeshs.currentStackMatarial;
                }
                else if(BuyObj.GetComponent<StackObject>()==null && BuyObj.GetComponent<Otomat>()==null &&  BuyObj.GetComponent<Employe>()==null )
                {
                  GameManager.Instance.deskTransforms.Add(BuyObj);
                //  GameManager.Instance.gm_deskObject.Add(BuyObj);
                  
                BuyObj.GetComponent<MeshRenderer>().material=GameManager.Instance.allMeshs.currentDeskMatarial;
                  BuyObj.GetComponent<DeskArea>().MoneyComplateTime=GameManager.Instance.playerCurrentImproveTime;
                
                }
                if(BuyObj.GetComponent<Employe>()!=null){
                GameManager.Instance.employeTransform.Add(BuyObj);
              //otomat yanlış yazılmış employe olcak
                }
                if(BuyObj.GetComponent<Otomat>()!=null){
                  GameManager.Instance.otamatTransform.Add(BuyObj);
                      BuyObj.GetComponent<MeshFilter>().mesh=GameManager.Instance.allMeshs.currentOtamatMesh;
                }
    }
}
