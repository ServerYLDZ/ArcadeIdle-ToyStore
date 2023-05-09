using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{

    public ProgressBar Pb;
  

    // Update is called once per frame
    void Update()
    {

        Pb.BarValue = GameManager.Instance.vali;
           

    }
}
