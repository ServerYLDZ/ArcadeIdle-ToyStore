using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOP : MonoBehaviour //subjectimiz gözlenen objemiz
{
    public GameObject missionArow;
    [SerializeField] private int MoneyAmount=0;
    public static event Action<int> OnCollectedMoney;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MoneyAmount++;
        OnCollectedMoney?.Invoke(MoneyAmount);// soru işareti null mu değilmi kontrollü yapar
        
    }
}
