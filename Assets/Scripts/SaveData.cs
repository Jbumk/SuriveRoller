using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[SerializeField]
public class SaveData
{
    public float BGMVolum;
    public float EffectVolum;
    public bool JoyStickControl;
    public bool XReverse;
    public bool YReverse;
    // public double[] Rank = new double[] { };
    public List<double> Rank = new List<double>();
}
