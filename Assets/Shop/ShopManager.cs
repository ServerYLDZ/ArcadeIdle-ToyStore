using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public ShopItemSO[] shopItemSO;
    public GameObject[] shopItemObject;
    public ShopTemplate[] shopTemplates;

    public TMP_Text ShopTypeText;

    public Sprite[] TypeSprites;
    public Image ShopTypeImage;

    public TMP_Text totalMoneyText;
    void Start()
    {
        LoadShop();
    }

   
    void Update()
    {
        
    }

    public void LoadShop () {
        if(GameManager.Instance.Money>=1000)
        totalMoneyText.text=(float)GameManager.Instance.Money/1000+"k".ToString();
        else
        totalMoneyText.text=GameManager.Instance.Money.ToString();
        if(GameManager.Instance.CurrentShopTypeIndex==0){
        
             for(int i = 0; i < shopItemSO.Length; i++) {
                 if(shopItemSO[i].itemType==ShopItemSO.ItemType.Desk)   {
                    RefreshShop(i);
                   
                 }     
                  
                  
                 else 
                   shopItemObject[i].SetActive(false);


         
        }    
                    ShopTypeText.text="DeskArea";
                    ShopTypeImage.sprite=TypeSprites[0];

        }

        else if(GameManager.Instance.CurrentShopTypeIndex==1){
                 for(int i = 0; i < shopItemSO.Length; i++) {
                      if(shopItemSO[i].itemType==ShopItemSO.ItemType.stack){
                               RefreshShop(i);
                           
                      }
                           
                          else 
                   shopItemObject[i].SetActive(false);
        }  
                            ShopTypeText.text="StackArea";
                              ShopTypeImage.sprite=TypeSprites[1];
        }

        else if(GameManager.Instance.CurrentShopTypeIndex==2){
             for(int i = 0; i < shopItemSO.Length; i++) {
                     if(shopItemSO[i].itemType==ShopItemSO.ItemType.otomat){
                             RefreshShop(i);
                           
                     }
                      
                      else 
                   shopItemObject[i].SetActive(false);
         
        }  

                               ShopTypeText.text="Otomat";
                              ShopTypeImage.sprite=TypeSprites[2];
        }

        else if(GameManager.Instance.CurrentShopTypeIndex==3){
            for(int i = 0; i < shopItemSO.Length; i++) {
                 if(shopItemSO[i].itemType==ShopItemSO.ItemType.worker){
                        RefreshShop(i);
                       
                 }
                     
                        else 
                   shopItemObject[i].SetActive(false);
         
        }  
                         ShopTypeText.text="Worker";
                         ShopTypeImage.sprite=TypeSprites[3];
        }


    }
    public void RefreshShop(int i){
            shopItemObject[i].SetActive(true);
            shopTemplates[i].priceText.text=shopItemSO[i].Pirce.ToString();
            shopTemplates[i].nameText.text=shopItemSO[i].itemName;
            shopTemplates[i].itemSplash.sprite=shopItemSO[i].Splash;

            if(shopItemSO[i].isBuying){
                 shopTemplates[i].priceText.text="SOLD";
                 //tekrar kullanma
                 if(shopItemSO[i].isUse)
                 shopItemSO[i].isUse=false;
                 

            }
       
        
    }
    public void Buy(int index){

       if( GameManager.Instance.Money>=shopItemSO[index].Pirce &&!shopItemSO[index].isBuying){
        shopItemSO[index].isBuying=true;
        shopItemSO[index].isUse=true;
        GameManager.Instance.Money-=shopItemSO[index].Pirce;
        GameManager.Instance.SetMoneyText();
          shopTemplates[index].priceText.text="USE";
        LoadShop();
         Use(index);
        SaveLoadManager.Instance.SaveState();// money savelendi
       
        //gerçekten asatın alsın
       }
       if(shopItemSO[index].isBuying){
        Use(index);//kullandım
        
        LoadShop();
        shopItemSO[index].isUse=true;
        shopTemplates[index].priceText.text="USE";
        
       }
        
    }
    public void Use (int index) {
        if(GameManager.Instance.CurrentShopTypeIndex==0){
            for(int i = 0; i < GameManager.Instance.deskTransforms.Count; i++) {
                
                // GameManager.Instance.deskTransforms[i].gameObject.GetComponent<MeshFilter>().mesh=shopItemSO[index].ItemMesh;
                 //GameManager.Instance.allMeshs.currentDeskMesh= shopItemSO[index].ItemMesh;
                 GameManager.Instance.deskTransforms[i].gameObject.GetComponent<MeshRenderer>().material=shopItemSO[index].ItemMaterial;
                 GameManager.Instance.allMeshs.currentDeskMatarial=shopItemSO[index].ItemMaterial;
                
            }
        }
        else if(GameManager.Instance.CurrentShopTypeIndex==1){
                 for(int i = 0; i < GameManager.Instance.stackTransforms.Count; i++) {
                
                 //GameManager.Instance.stackTransforms[i].gameObject.GetComponent<MeshFilter>().mesh=shopItemSO[index].ItemMesh;
                // GameManager.Instance.allMeshs.currentStackMesh= shopItemSO[index].ItemMesh;
                 GameManager.Instance.stackTransforms[i].GetComponent<MeshRenderer>().material=shopItemSO[index].ItemMaterial;
                 GameManager.Instance.allMeshs.currentStackMatarial=shopItemSO[index].ItemMaterial;
                
            }
        }
        else if(GameManager.Instance.CurrentShopTypeIndex==2){
             for(int i = 0; i < GameManager.Instance.employeTransform.Count; i++) {
                
                 GameManager.Instance.otamatTransform[i].gameObject.GetComponent<MeshFilter>().mesh=shopItemSO[index].ItemMesh;
                 GameManager.Instance.allMeshs.currentOtamatMesh= shopItemSO[index].ItemMesh;
              
            }

        }
        else{
           
             for(int i = 0; i < GameManager.Instance.workers.Length; i++) {
                if(GameManager.Instance.workers[i]){
                
                if(shopItemSO[index].ItemMesh)
                GameManager.Instance.workers[i].GetComponent<Worker>().basket.SetActive(true);
                else
                GameManager.Instance.workers[i].GetComponent<Worker>().basket.SetActive(false);

                GameManager.Instance.workers[i].GetComponent<Worker>().basket.GetComponent<MeshFilter>().mesh=shopItemSO[index].ItemMesh;
                GameManager.Instance.allMeshs.basketMesh=shopItemSO[index].ItemMesh;
                //degisiklikler yapılcak
                }
              
              
                
            }

        }
    }
    public void CurrentTypeIndex (int index) {
        GameManager.Instance.CurrentShopTypeIndex=index;
        Debug.Log(index);
    }
    public void exitStore () {
        this.gameObject.SetActive(false);
        GameManager.Instance.canMovePlayer=true;
    }
}
