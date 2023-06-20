using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]

public class ProgressBar : MonoBehaviour
{

    [Header("Title Setting")]
    public string Title;
    public Color TitleColor;
    public Font TitleFont;
    public int TitleFontSize = 10;

    [Header("Bar Setting")]
    public Color BarColor;   
    public Color BarBackGroundColor;
    public Sprite BarBackGroundSprite;
    [Range(1f, 100f)]
    public int Alert = 20;
    public int half = 50;
    public Color BarAlertColor;
    public Color BarHalfColor;

    [Header("Sound Alert")]
    public AudioClip sound;
    public bool repeat = false;
    public float RepeatRate = 1f;

    private Image bar, barBackground;
    private float nextPlay;
    private AudioSource audiosource;

    private float barValue;
    public float BarValue
    {
        get { return barValue; }

        set
        {
            value = Mathf.Clamp(value, 0, 100);
            barValue = value;
            UpdateValue(barValue);

        }
    }

        

    private void Awake()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
        barBackground = GetComponent<Image>();
       
        barBackground = transform.Find("BarBackground").GetComponent<Image>();
       
    }

    private void Start()
    {
       
    
        
        

        bar.color = BarColor;
        barBackground.color = BarBackGroundColor; 
        barBackground.sprite = BarBackGroundSprite;

        UpdateValue(barValue);


    }

    void UpdateValue(float val)
    {
        bar.fillAmount = val / 100;
        


        if (Alert >= val)
        {
            bar.color = BarAlertColor;
          
        }
      else if (val>Alert )
        {
            bar.color = BarColor;
         
        }
       

    }


    private void Update()
    {
        UpdateValue(GameManager.Instance.vali);
        if (!Application.isPlaying)
        {           
            UpdateValue(100);
           

            bar.color = BarColor;
            barBackground.color = BarBackGroundColor;

            barBackground.sprite = BarBackGroundSprite;           
        }
       
    }

}
