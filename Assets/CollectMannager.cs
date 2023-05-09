using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectMannager : MonoBehaviour
{

   public  List<GameObject> collectList = new List<GameObject>();
   public int collectLevel=1;
    public int collectPrefabIndex=0;
    public  Transform collectPoint;
    public int collectLimit=10;
    public int collectSpace=20;
    GameObject obj;

    private void OnEnable()
    {
       TriggerMannager.OnCollectStarted += GetCollect;
        TriggerMannager.OnDroppingStarted += DropCollect;
       
       
    }
    private void OnDisable()
    {
       TriggerMannager.OnCollectStarted -= GetCollect;
        TriggerMannager.OnDroppingStarted -= DropCollect;
    }
 
 private void Start() {
     collectLimit=(int)GameManager.Instance.playerCurrentCapasity;
     Debug.Log("capasity"+GameManager.Instance.playerCurrentCapasity);
 }
    public void GetCollect()
    {
        
        if (collectList.Count <= collectLimit)
        {
            obj = ObjectPool.Instance.GetPooledObject(collectPrefabIndex);// kendi stağine bir ekle
            obj.transform.parent = collectPoint;
            obj.transform.position = new Vector3(collectPoint.position.x, collectPoint.position.y+((float)collectList.Count/collectSpace), collectPoint.position.z);
           obj.transform.rotation=collectPoint.rotation;
            collectList.Add(obj);
            if (TriggerMannager.stackObj != null)// stack den bir eksilt
            {
                TriggerMannager.stackObj.RemoveLast();
            }
        }

        
    }
    public void DropCollect()
    {
        if (collectList.Count>0)
        {
            if (TriggerMannager.deskArea.deskList.Count<=TriggerMannager.deskArea.maxDeskLimit)
            {
                TriggerMannager.deskArea.GetDropping();
                RemoveLast();
            }
           
        }

    }
    public void RemoveLast()
    {
        if (collectList.Count >0)
        {
            collectList[collectList.Count - 1].SetActive(false);
            collectList[collectList.Count - 1].transform.parent= ObjectPool.Instance.gameObject.transform;
            collectList.RemoveAt(collectList.Count-1);
            

        }
    }
   
}
