using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName ="ShopMenu",menuName ="ScriptableObjet/NewItem",order =1)]
public class ShopItemSO :ScriptableObject
{
    public enum ItemType {
        Desk,
        stack,
        worker,
        otomat
    }

    
    public string itemName="Nameless";
    public ItemType itemType;
    public int Pirce=0;
    public Sprite Splash;

    public Mesh ItemMesh;
    public Material ItemMaterial;
    public bool isBuying=false;
    public bool isUse=false;

    
    
}
