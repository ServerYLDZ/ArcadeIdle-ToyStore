using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saller : MonoBehaviour
{

private Animator ator;
private DeskArea desk;

private void Start() {
    ator=GetComponent<Animator>();
    desk=GetComponentInParent<DeskArea>();
    StartCoroutine(StartWave());

}
private void Update() {
    if(desk.deskList.Count>0){
        ator.SetBool("empty",false);
    }
    else
         ator.SetBool("empty",true);
}

IEnumerator StartWave(){
    yield return new WaitForSeconds(10);
    ator.SetTrigger("wave");
    
}
}
