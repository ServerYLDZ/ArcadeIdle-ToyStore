using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    public int Money,extraMoney;//save+
    public GameObject player;
    public ShopMeshSO allMeshs;
    public ShopMeshSO defaultMeshs;
    public int CurrentShopTypeIndex=0;
     public static float employeIncreseTimeCount=1;
    public bool canMovePlayer=true;
    public  event Action<int> OnCollectedMoney;


    public GameObject UpgradeUI; //save+
    public GameObject _piUI;//save+
    
    public GameObject shopUI;
public List<GameObject> otamatTransform;   
public List<GameObject> employeTransform; // save +
    [Header ("WORKER STAFF")]
    [Space(5)]

    #region  Worker için tanimlamalarin tamami

    public List<GameObject> Spawners;
    public List<GameObject> stackTransforms;//save +
    public List<GameObject> deskTransforms; //save+
    [SerializeField] public Transform wSpawnPoint;
    [SerializeField] private GameObject wPrefab;
   
    public int currentWorkerCapasity=5; //save+
    public int wIndex=0; //save+
    public int workerPrice=1000; //save+
    public int maxWorkerCount=4; 
    public int maxCapasityIncrese=4;
    public int capasityPrice=100; //save+
    public int capasityIndex=0; //save+

    public int maxSpeedCount=3; 
public float currentWorkerSpeed=3.5f; //save+
    public int speedIndex=0; //save+
    public int speedPirce=100; //save+


    public GameObject[] workers; //save and spwan or dont destroy on load  ++++++++++++++
   #endregion
    [Header ("PLAYER STAFF")]
    [Space(5)]
    #region  Player için Tanımlamalar
      
      public int playerMaxSpeed=3; 
      public float playerCurrentSpeed=3.5f; //save+
      public int playerSpeedIndex=0; //save+
      public int playerSpeedPrice=200; //save+

       public int playerMaxCapasity=4;
      public float playerCurrentCapasity=15; //save+
      public int playerCapasityIndex=0; //save+
      public int playerCapasityPrice=200; //save+

     public int playerMaxImproveTime=4;
      public float playerCurrentImproveTime=1f; //save+
      public int playerImproveTimeIndex=0; //save+
      public int playerImproveTimePrice=400; //save+

    #endregion
    [Header ("ADS STAFF")]
    [Space(5)]
    public bool [] adsShow;
    public float resetTime=300;
    public float showTime=120;


 public float workerSalaryCount;
 public float borc;
    public int vali = 100;

public void AdsControl () {
    for(int i = 0; i < adsShow.Length; i++) {
      if(adsShow[i]==false){
          GetComponent<InterstitialAd>().LoadAd();
        StartCoroutine(ShowInterAdd(i));
        break;
      }
    }
}
IEnumerator ShowInterAdd(int i){
  yield return new WaitForSeconds(showTime);
  GetComponent<InterstitialAd>().ShowAd();
  adsShow[i]=true;
  AdsControl();
}
IEnumerator ResetInterAdd(){
  yield return new WaitForSeconds(resetTime);
  for(int i = 0; i < adsShow.Length; i++) {
    adsShow[i]=false;
  }
  AdsControl();
  StartCoroutine(ResetInterAdd());
}
    private int workerCount(){
  int syc=0;
  for(int i = 0; i < workers.Length; i++) {
    if(workers[i]!=null)
    syc++;
  }
  return syc;
 }
    
    private void Start() {
       SetMoneyText();
       StartCoroutine(DecreseSalary());

        workerSalaryCount =workerCount()+1 *50;
        AdsControl();
         StartCoroutine(ResetInterAdd());
    }
  IEnumerator DecreseSalary(){
  
    yield return new  WaitForSeconds(300);// 5 dk ara ile maas kesintisi
    
    workerSalaryCount=workerCount()+1*50;
    if(Money<workerSalaryCount+borc){
      Debug.Log("para yetersiz mutluluk düsücek");
            vali -= 10;
      borc+=workerSalaryCount-Money;
      Money=0;
      SetMoneyText();
    }
    else{
      
      Money-= (int)workerSalaryCount;
      Debug.Log("isci massi ödendi"+workerSalaryCount);
      SetMoneyText();


    }
    StartCoroutine(DecreseSalary());
  }


    private void OnEnable()
    {
        SetMoneyText();
        TriggerMannager.OnBuying += BuySamething;
        workers=new GameObject[maxWorkerCount];
       
    
    
    }
    private void Update() {
      SetMoneyText();
      if(Input.GetMouseButtonDown(1)){
        employeIncreseTimeCount-=.1f;
        for(int i = 0; i < GameManager.Instance.stackTransforms.Count ; i++) {
     GameManager.Instance.stackTransforms[i].GetComponentInParent<StackObject>().spawnTime=GameManager. employeIncreseTimeCount;
          }
        Debug.Log(employeIncreseTimeCount);
      }
    }
    private void OnDisable()
    {
        TriggerMannager.OnBuying -= BuySamething;
    }
    private void Awake() {
       SetMoneyText();
    }
  

  public void CreateWorker(){
    
    if(wIndex<maxWorkerCount && Money>=workerPrice){
        Money-=workerPrice;
        workerPrice=workerPrice*2;
        SetMoneyText();
        
        UpgradeUI.GetComponent<UIUpgrade>().SetWorkerTextPirce();
        UpgradeUI.GetComponent<UIUpgrade>().workerCheckBoxs[wIndex].SetActive(true);
     workers[wIndex]=Instantiate(wPrefab,wSpawnPoint.transform).gameObject;
    workers[wIndex].GetComponent<AICollectAndDrop>().collectLimit=currentWorkerCapasity;
    workers[wIndex].GetComponent<Worker>().navMeshAgent.speed=currentWorkerSpeed;
     workers[wIndex].GetComponent<Worker>().basket.GetComponent<MeshFilter>().mesh=GameManager.Instance.allMeshs.basketMesh;
     if(GameManager.Instance.allMeshs.basketMesh)
       workers[wIndex].GetComponent<Worker>().basket.SetActive(true);
     else 
       workers[wIndex].GetComponent<Worker>().basket.SetActive(false);
       
     workers[wIndex].GetComponent<Worker>().index=wIndex;
     
      
      SaveLoadManager.Instance.SaveWorker();
     wIndex++;
     SaveLoadManager.Instance.SaveWorkerState();
     if(maxWorkerCount==wIndex)
       UpgradeUI.GetComponent<UIUpgrade>().PriceText.text="Max";

    }

  }

  public void IncreseWorkerCpasity () {

    if(Money>capasityPrice && capasityIndex<maxCapasityIncrese){
    Money-=capasityPrice;  
    currentWorkerCapasity+=3;  
    SetMoneyText();
    capasityPrice *=2;
    UpgradeUI.GetComponent<UIUpgrade>().SetWokerCapasityPrice();
    UpgradeUI.GetComponent<UIUpgrade>().workerCapasityCheckBoxs[capasityIndex].SetActive(true);

        capasityIndex++;
         

          for(int i = 0; i <workers.Length; i++) {
        if(workers[i]!=null){
       workers[i].GetComponent<AICollectAndDrop>().collectLimit=currentWorkerCapasity;
        }

    

         if(maxCapasityIncrese==capasityIndex)
       UpgradeUI.GetComponent<UIUpgrade>().CapasityPriceText.text="Max";
     
    }
    }
      SaveLoadManager.Instance.SaveWorkerState();
    
   }

   public void IncreseSpeed(){

if(Money>speedPirce  && speedIndex<maxSpeedCount){
    Money-=speedPirce;  
    currentWorkerSpeed+=1;  
     speedPirce *=2;
    SetMoneyText();
    UpgradeUI.GetComponent<UIUpgrade>().SetWorkerSpeedPrice();
    UpgradeUI.GetComponent<UIUpgrade>().workerSpeedCheckBoxs[speedIndex].SetActive(true);

       speedIndex++;
        

          for(int i = 0; i <workers.Length; i++) {
        if(workers[i]!=null){
       workers[i].GetComponent<Worker>().navMeshAgent.speed=currentWorkerSpeed;
        }
 
    }
if(maxSpeedCount==speedIndex)
       UpgradeUI.GetComponent<UIUpgrade>().speedPriceText.text="Max";
       
    }
      SaveLoadManager.Instance.SaveWorkerState();
      
   }

public void PlayerSpeedIncrese() {
    if(playerSpeedIndex<playerMaxSpeed&& Money>=playerSpeedPrice){
   Money-=playerSpeedPrice;
    SetMoneyText();
    playerSpeedPrice*=2;
     _piUI.GetComponent<PI_UIManager>().SetPlayerSpeedPrice();
      _piUI.GetComponent<PI_UIManager>().playerSpeedCheckBoxs[playerSpeedIndex].SetActive(true);
    
     playerSpeedIndex++;
     
   

    player.GetComponent<PlayerController>().speed+=1;
    playerCurrentSpeed=    player.GetComponent<PlayerController>().speed;
    if(playerMaxSpeed==playerSpeedIndex)
    _piUI.GetComponent<PI_UIManager>().speedPriceText.text="Max";
      
    }
 SaveLoadManager.Instance.SavePlayerState();
      

}
public void PlayerCapasityIncrese(){
 if(Money>playerCapasityPrice &&playerCapasityIndex<playerMaxCapasity){
    Money-=playerCapasityPrice;  
       playerCapasityPrice *=2;
    SetMoneyText();
    _piUI.GetComponent<PI_UIManager>().SetPlayerCapasityPrice();
    _piUI.GetComponent<PI_UIManager>().playerCapasityCheckBoxs[playerCapasityIndex].SetActive(true);
    

       playerCapasityIndex++;
      

    player.GetComponent<CollectMannager>().collectLimit+=10;
    playerCurrentCapasity=player.GetComponent<CollectMannager>().collectLimit;
         
      if(playerMaxCapasity==playerCapasityIndex)
        _piUI.GetComponent<PI_UIManager>().CapasityPriceText.text="Max";

    }
    
        SaveLoadManager.Instance.SavePlayerState();
}
public void PlayerImproveTime () {
    if(Money>playerImproveTimePrice&&playerImproveTimeIndex<playerMaxImproveTime){
    Money-=playerImproveTimePrice;  
    playerCurrentImproveTime-=0.1f;
    playerImproveTimePrice *=2;
    SetMoneyText();
    _piUI.GetComponent<PI_UIManager>().SetPlayerTimeImprovePirce();
    _piUI.GetComponent<PI_UIManager>().playerTimeCheckBoxs[playerImproveTimeIndex].SetActive(true);
    

      playerImproveTimeIndex++;
      

    for(int i = 0; i < deskTransforms.Count; i++) {
        
        deskTransforms[i].GetComponentInParent<DeskArea>().MoneyComplateTime=playerCurrentImproveTime;
    }
         
      if(playerMaxImproveTime==playerImproveTimeIndex)
        _piUI.GetComponent<PI_UIManager>().timeText.text="Max";

           SaveLoadManager.Instance.SavePlayerState();
    }
 
}
public void exitHrUI(){
    UpgradeUI.SetActive(false);
}
public void exitPiUI(){
    _piUI.SetActive(false);
}

   public void BuySamething()
    {
        if (TriggerMannager.toBuyArea!=null)// satın alma alanınaysam
        {
         
            if (Money>=1 &&!TriggerMannager.toBuyArea.isBuyingComplate)
            {
                if(Money>=TriggerMannager.toBuyArea.decreseCount){
               TriggerMannager.toBuyArea.Buy(TriggerMannager.toBuyArea.decreseCount);//azalma miktarı kadar azalsın ileride yüksek fiyatlarda uzun süreli beklemeyi azaltır
                Money-=TriggerMannager.toBuyArea.decreseCount;
                SetMoneyText();
                }
                else{
                TriggerMannager.toBuyArea.Buy(1);//azalma miktarı kadar azalsın ileride yüksek fiyatlarda uzun süreli beklemeyi azaltır
                Money-=1;
                SetMoneyText();

                }
            
           
            }
            
        }

    }
    public void SetMoneyText()
    {
        
        OnCollectedMoney?.Invoke(Money);
     
    }
    public void OpenShopUI () {
    
      shopUI.SetActive(true);
      canMovePlayer=false;
      
    }
   

}
