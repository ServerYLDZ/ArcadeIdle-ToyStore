using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employe : MonoBehaviour
{

  private void Awake() {
   
  }


   private void OnEnable() {
      GameManager.employeIncreseTimeCount-=.1f;
  
for(int i = 0; i < GameManager.Instance.stackTransforms.Count ; i++) {
     GameManager.Instance.stackTransforms[i].GetComponentInParent<StackObject>().spawnTime=GameManager. employeIncreseTimeCount;
}
 
    Debug.Log(GameManager. employeIncreseTimeCount);
   }
}
