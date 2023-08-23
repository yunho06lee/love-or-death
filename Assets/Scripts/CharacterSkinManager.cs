using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSkinManager : MonoBehaviour
{
    public Sprite[] skinImage;

    public SpriteRenderer sp;

    public void OnEnable()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = null;
    }

    public void changeSkin(string sName)
    {
        Sprite changeTo;
        
        if(sName == "NatsumiUniform")
            changeTo = skinImage[0];
        else if(sName == "NatsumiHoodie")
            changeTo = skinImage[1];
        else if(sName == "NatsumiFestival")
            changeTo = skinImage[2];
        else if(sName == "SakuraUniform")
            changeTo = skinImage[3];
        else if(sName == "SakuraBeach")
            changeTo = skinImage[4];
        else if(sName == "SakuraFestival")
            changeTo = skinImage[5];
        else if(sName == "KazuhaHoodie")
            changeTo = skinImage[6];
        else if(sName == "KazuhaBeach")
            changeTo = skinImage[7];
        else if(sName == "KazuhaFestival")
            changeTo = skinImage[8];
        else if(sName == "Himuro")
            changeTo = skinImage[9];
        else if (sName == "HimuroBeach")
            changeTo = skinImage[10];
        else if (sName == "none")
            changeTo = null;
        else
            return;
        
        sp.sprite = changeTo;
    }
}
