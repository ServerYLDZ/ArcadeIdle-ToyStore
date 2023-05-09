using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollectAndDrop : MonoBehaviour
{
    public List<GameObject> collectList = new List<GameObject>();
    public int collectPrefabIndex = 0;
    public Transform collectPoint;
    public int collectLimit = 10;
    public int collectSpace = 20;

    private AIManager aiManger;
   private StackObject stackObj;
  private DeskArea deskArea;

    private void OnEnable()
    {
        aiManger = GetComponent<AIManager>();
        aiManger. OnCollectStarted += GetCollect;
        aiManger.OnDroppingStarted += DropCollect;


    }
    private void OnDisable()
    {
        aiManger.OnCollectStarted -= GetCollect;
         aiManger.OnDroppingStarted -= DropCollect;
    }
    private void Awake() {
        collectLimit=GameManager.Instance.currentWorkerCapasity;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Collectable")
        {
            stackObj = other.GetComponent<StackObject>();
         
         
         
           
        }
        if (other.tag == "DeskArea")
        {
            deskArea = other.GetComponent<DeskArea>();
            
        
         
        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {

            stackObj = null;
        }
        if (other.tag == "DeskArea")
        {
           
               deskArea = null;
        }


    }
    public void GetCollect()
    {

        if (collectList.Count <= collectLimit)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(collectPrefabIndex);// kendi stağine bir ekle
            obj.transform.parent = collectPoint;
            obj.transform.position = new Vector3(collectPoint.position.x, collectPoint.position.y + ((float)collectList.Count / collectSpace), collectPoint.position.z);
            collectList.Add(obj);
            if (stackObj != null)// stack den bir eksilt
            {
                stackObj.RemoveLast();
            }
        }


    }
    public void DropCollect()
    {
        if (collectList.Count > 0)
        {
            if (deskArea.deskList.Count <= deskArea.maxDeskLimit)
            {
               
               deskArea.GetDropping();
                RemoveLast();
            }

        }

    }
    public void RemoveLast()
    {
        if (collectList.Count > 0)
        {
            collectList[collectList.Count - 1].SetActive(false);
            collectList[collectList.Count - 1].transform.parent= ObjectPool.Instance.gameObject.transform;
         collectList.RemoveAt(collectList.Count - 1);


        }
    }
}
