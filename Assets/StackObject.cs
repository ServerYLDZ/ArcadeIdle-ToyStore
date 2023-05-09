using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackObject : MonoBehaviour
{
    public float spawnTime = .5f;
    public Stack<GameObject> stackList=new Stack<GameObject>();
    [SerializeField] int maxStackSize = 30;
    [SerializeField] int maxRawCount = 10;
    [SerializeField] private int stackPrefabIndex;
    [SerializeField] int colmunSpace = 20;
    [SerializeField] float rawSpace = 20;
     [SerializeField] float zSpace = 10;
    public Transform exitPoint;
    public GameObject aiPoint;

   public bool isWorking=true;
    private void Awake() {
        
          GetComponent<MeshRenderer>().material=GameManager.Instance.allMeshs.currentStackMatarial;
    }
    void Start()
    {
        StartCoroutine(StackTrack());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RemoveLast();
        }
    }
    public void RemoveLast()
    {
        if (stackList.Count >0)
        {
            GameObject tmp= stackList.Pop();
            tmp.SetActive(false);

        }
    }
    

    // Update is called once per frame
   IEnumerator StackTrack()
    {
        while (true)
        {
           
            float stackcount = stackList.Count;
            int rawCount = stackList.Count /maxRawCount;         
            int zCount = rawCount / 3;
            if (isWorking)
            {

              GameObject  obj = ObjectPool.Instance.GetPooledObject(stackPrefabIndex);
               //new Vector3(exitPoint.position.x+(float)(rawCount/rawSpace),exitPoint.position.y+(stackcount%maxRawCount/colmunSpace), exitPoint.position.z); // eski birikme
              obj.transform.position =  new Vector3(exitPoint.position.x + (rawCount%3 / rawSpace),exitPoint.position.y + stackcount % maxRawCount / colmunSpace,exitPoint.position.z + zCount/zSpace);
              stackList.Push(obj);
              
                if (stackList.Count >= maxStackSize)
                    isWorking = false;

            }
           
            if(stackList.Count<maxStackSize)
                isWorking = true;

              
                yield return new WaitForSeconds(spawnTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Worker>()!=null)
        {
            other.GetComponent<Worker>().index++;
        } 
    }
}
