using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class SaveData
{
    public float BGMVolum=0.5f;
    public float EffectVolum=0.5f;
    public bool JoyStickControl=true;
    public bool XReverse=false;
    public bool YReverse=false;
    public Vector2 AccelSet;
    // public double[] Rank = new double[] { };
    public List<double> BallRank = new List<double>();
    public List<int> PincetRank = new List<int>();
}
