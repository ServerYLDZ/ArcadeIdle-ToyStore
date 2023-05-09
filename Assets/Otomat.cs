using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otomat : MonoBehaviour
{
 
    public  List<GameObject> moneyList = new List<GameObject>();
    public int moneyPrefabIndex;
    public Transform moneyPoint;
    public GameObject aiPoint;
    public int MoneyIncreseCount=1;
    public int rawAdd=10;
    public float colmunSpace;
    public float rawSpace=10;
    public float zSpace = 10;
    public int maxDeskLimit = 30;
    
public float MoneyComplateTime=1;

    private void Awake() {
        GetComponent<MeshFilter>().mesh=GameManager.Instance.allMeshs.currentOtamatMesh;
    }
    IEnumerator GetMoney()
    {
        while (true)
        {
            int rawCount = moneyList.Count / rawAdd;
            int zCount = rawCount / 3;
           
               
                if (zCount < 3)// eger değilse sadece para miktarını arttır  daha fazla stacka atmana gerek yok
                {
                    GameObject obj = ObjectPool.Instance.GetPooledObject(moneyPrefabIndex);
                    obj.transform.position = new Vector3(moneyPoint.position.x + (rawCount%3 / rawSpace), moneyPoint.position.y + (float)moneyList.Count % rawAdd / colmunSpace, moneyPoint.position.z + zCount/zSpace);
                    moneyList.Add(obj);
                }
                else
                GameManager.Instance.extraMoney+=MoneyIncreseCount;


            

            yield return new WaitForSeconds(MoneyComplateTime);

        }
       

    }
    private void OnEnable()
    {
        TriggerMannager.OnMoneyCollect += CollectMoney;
    }
    private void OnDisable()
    {
        TriggerMannager.OnMoneyCollect -= CollectMoney;
    }

    private void Start()
    {
        StartCoroutine(GetMoney());
    }

    public void CollectMoney()
    {
        if (moneyList.Count > 0)
        {
            moneyList[moneyList.Count - 1].SetActive(false);
            moneyList.RemoveAt(moneyList.Count - 1);
            GameManager.Instance.Money+=MoneyIncreseCount;
            GameManager.Instance.SetMoneyText();
        }
        else
        {
            if (GameManager.Instance.extraMoney>0)
            {
                GameManager.Instance.Money += GameManager.Instance.extraMoney;
                GameManager.Instance.extraMoney = 0;
            }
        }

    }
    
}
