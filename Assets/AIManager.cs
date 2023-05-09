using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public delegate void OnDeskArea();
    public  event OnCollectArea OnCollectStarted;
    public  event OnDeskArea OnDroppingStarted;

  

    bool isCollecting;
    bool isDropping;







    private void Start()
    {
        StartCoroutine(CollectAndDrop());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Collectable")
        {
            //stackObj = other.GetComponent<StackObject>();

            isCollecting = true;
        }
        if (other.tag == "DeskArea")
        {
           
             
           // deskArea = other.GetComponent<DeskArea>();
            isDropping = true;
        }
        
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Collectable")
        {
            isCollecting = false;
            //stackObj = null;
        }
        if (other.tag == "DeskArea")
        {
           
            isDropping = false;
         //   deskArea = null;
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
}
