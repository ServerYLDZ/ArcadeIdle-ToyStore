using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class SaveLoadManager : MonoSingleton<SaveLoadManager>
{
    // Start is called before the first frame update
  public string SUB_WORKER="/worker";


  public static List<Worker> workers=new List<Worker>();

  public GameObject workerPrefab;
private void Awake() {
   
       SceneManager.sceneLoaded += LoadState;// sahne yüklendiğinde otamatik load almaya yarar 
       SceneManager.sceneLoaded +=LoadWorkerState;
       SceneManager.sceneLoaded +=LoadPlayerState;

      SceneManager.sceneLoaded+=LoadWorker;

}

private void OnApplicationQuit() {
    //save işlemlerinin hepsi burda yapılcak geliştirme sürecine engel oluyor şu an
  SaveWorker();

}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            EraseAllData();
        }
    }

  public void SaveState() 
    { 
        string st = ""; 
         
         st+= GameManager.Instance.Money.ToString() +"|";

        PlayerPrefs.SetString("SaveState", st); 
    } 
    public void LoadState(Scene s, LoadSceneMode mode) 
    { 
        
        if (!PlayerPrefs.HasKey("SaveState"))
        return; 
        string[] data=  PlayerPrefs.GetString("SaveState").Split('|'); 
        
       GameManager.Instance.Money = int.Parse(data[0]); 
       GameManager.Instance.SetMoneyText();
       
       // xp = int.Parse(data[1]); 
        
        

       
    } 
    public void  SaveAsset(){
       /* BinaryFormatter formatter=new BinaryFormatter();
        string path= Application.persistentDataPath +SUB_ASSET;

         FileStream stream= new FileStream(path,FileMode.Create);
          AssetData data= new AssetData();
        formatter.Serialize(stream,data);
        stream.Close();*/

    }

    public void  LoadAsset (Scene s, LoadSceneMode mode) {
     /*  BinaryFormatter formatter=new BinaryFormatter();
        string path= Application.persistentDataPath +SUB_ASSET;

          if(File.Exists(path)){
            FileStream stream =new FileStream(path,FileMode.Open);
            AssetData data =formatter.Deserialize(stream) as AssetData;

            stream.Close();
            
        GameManager.Instance.CurrentDeskMesh=data.DeskMesh;
        //diğer atamlar burdan olcak
        
            }
            else{
                Debug.Log("Dosya Bulunamadı: "+ path);
            }
*/
    }
    public void SaveWorker () {
        BinaryFormatter formatter=new BinaryFormatter();
        string path= Application.persistentDataPath +SUB_WORKER;

    
    for(int i = 0; i <GameManager.Instance.workers.Length; i++) {
        if(GameManager.Instance.workers[i]){
            FileStream stream= new FileStream(path+i,FileMode.Create);
           WorkerData data= new WorkerData(GameManager.Instance.workers[i].GetComponent<Worker>());
            formatter.Serialize(stream,data);
           stream.Close();
             Debug.Log("kayit aldimmi");
        }
        
       
    }
       
    }
    public void LoadWorker (Scene s, LoadSceneMode mode) {
       BinaryFormatter formatter=new BinaryFormatter();
        string path= Application.persistentDataPath +SUB_WORKER;
        for(int i = 0; i < GameManager.Instance.wIndex; i++) {
            if(File.Exists(path+i)){
            FileStream stream =new FileStream(path+i,FileMode.Open);
            WorkerData data =formatter.Deserialize(stream) as WorkerData;

            stream.Close();
            Vector3 pos = new Vector3(data.position[0],data.position[1],data.position[2]);
        
          GameObject worker = Instantiate(workerPrefab, pos,Quaternion.identity);
        
         // worker.GetComponent<Worker>().index=data.index;
        worker.GetComponent<Worker>().index+=i;
          worker.GetComponent<Worker>().workerLevel=data.workerLevel;
           //worker moshi oyun başı kullan
          GameManager.Instance.workers[i]=worker;    

            }
            else{
                Debug.Log("Dosya Bulunamadi: "+ path +i);
            }
            
        }
        SaveWorker();
    }

  

    public void SaveWorkerState (){
        string st="";
        st += GameManager.Instance.workerPrice.ToString() +"|";
        st += GameManager.Instance.currentWorkerCapasity.ToString() +"|";
        st += GameManager.Instance.currentWorkerSpeed.ToString()+"|";
        st+=GameManager.Instance.wIndex.ToString()+"|";
        st+=GameManager.Instance.speedIndex.ToString()+"|";
        st+=GameManager.Instance.capasityIndex.ToString()+"|";
        st+=GameManager.Instance.capasityPrice.ToString() +"|";
        st+=GameManager.Instance.speedPirce.ToString()+"|";

        PlayerPrefs.SetString("WorkerState",st);
    }
    public void LoadWorkerState(Scene s, LoadSceneMode mode){
        if(!PlayerPrefs.HasKey("WorkerState"))
        return;
        string [] data=PlayerPrefs.GetString("WorkerState").Split('|');

    GameManager.Instance.workerPrice=int.Parse(data[0]);
    GameManager.Instance.currentWorkerCapasity=int.Parse(data[1]);
    GameManager.Instance.currentWorkerSpeed=float.Parse(data[2]);
    GameManager.Instance.wIndex=int.Parse(data[3]);
    GameManager.Instance.speedIndex=int.Parse(data[4]);
    GameManager.Instance.capasityIndex=int.Parse(data[5]);
    GameManager.Instance.capasityPrice=int.Parse(data[6]);
    GameManager.Instance.speedPirce=int.Parse(data[7]);
  
    }


    public void SavePlayerState (){
        string st="";
        st += GameManager.Instance.playerCurrentImproveTime.ToString() +"|";
        st += GameManager.Instance.playerImproveTimeIndex.ToString() +"|";
        st += GameManager.Instance.playerImproveTimePrice.ToString()+"|";
      
        st+=GameManager.Instance.playerCurrentCapasity.ToString()+"|";
        st+=GameManager.Instance.playerCapasityIndex.ToString()+"|";
        st+=GameManager.Instance.playerCapasityPrice.ToString()+"|";

           st+=GameManager.Instance.playerCurrentSpeed.ToString()+"|";
        st+=GameManager.Instance.playerSpeedIndex.ToString()+"|";
        st+=GameManager.Instance.playerSpeedPrice.ToString()+"|";
Debug.Log("save");
        PlayerPrefs.SetString("PlayerState",st);
    }
    public void LoadPlayerState(Scene s, LoadSceneMode mode){
        if(!PlayerPrefs.HasKey("PlayerState"))
        return;
        string [] data=PlayerPrefs.GetString("PlayerState").Split('|');

    GameManager.Instance.playerCurrentImproveTime=float.Parse(data[0]);
    GameManager.Instance.playerImproveTimeIndex=int.Parse(data[1]);
    GameManager.Instance.playerImproveTimePrice=int.Parse(data[2]);
    GameManager.Instance.playerCurrentCapasity=float.Parse(data[3]);
    GameManager.Instance.playerCapasityIndex=int.Parse(data[4]);
    GameManager.Instance.playerCapasityPrice=int.Parse(data[5]);
    GameManager.Instance.playerCurrentSpeed=float.Parse(data[6]);
    GameManager.Instance.playerSpeedIndex=int.Parse(data[7]);
    GameManager.Instance.playerSpeedPrice=int.Parse(data[8]);
    Debug.Log("load");
    }

    private void EraseAllData () {
        PlayerPrefs.DeleteAll();
        
        GameManager.Instance.allMeshs.currentDeskMatarial=GameManager.Instance.defaultMeshs.currentDeskMatarial;
          GameManager.Instance.allMeshs.currentOtamatMesh=GameManager.Instance.defaultMeshs.currentOtamatMesh;
          GameManager.Instance.allMeshs.currentStackMatarial=GameManager.Instance.defaultMeshs.currentStackMatarial;
            GameManager.Instance.allMeshs.basketMesh=GameManager.Instance.defaultMeshs.basketMesh;
         
        Debug.Log("delete all loaded");
    }

}
