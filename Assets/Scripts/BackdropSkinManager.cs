using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackdropSkinManager : MonoBehaviour
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
        
        if(sName == "Beach1")
            changeTo = skinImage[0];
        else if(sName == "Beach2")
            changeTo = skinImage[1];
        else if(sName == "Brighthotel")
            changeTo = skinImage[2];
        else if(sName == "Darkhotel")
            changeTo = skinImage[3];
        else if(sName == "City")
            changeTo = skinImage[4];
        else if(sName == "Darkroom")
            changeTo = skinImage[5];
        else if(sName == "Firework")
            changeTo = skinImage[6];
        else if(sName == "Hotel")
            changeTo = skinImage[7];
        else if(sName == "Road")
            changeTo = skinImage[8];
        else if(sName == "Sea")
            changeTo = skinImage[9];
        else if (sName == "Store")
            changeTo = skinImage[10];
        else if (sName == "Underwater")
            changeTo = skinImage[11];
        else
            return;
        
        sp.sprite = changeTo;
    }
}