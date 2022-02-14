﻿using System.Collections;
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
    public Vector2 AccelSet;
    // public double[] Rank = new double[] { };
    public List<double> BallRank = new List<double>();
    public List<int> PincetRank = new List<int>();
}
