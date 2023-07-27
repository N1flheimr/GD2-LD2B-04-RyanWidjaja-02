using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BasePlayerUpgradeSO : ScriptableObject
{
    public Sprite UpgradeIcon;
    [TextArea]
    public string UpgradeName;
    [TextArea]
    public string Description;

    public abstract void Apply();
}
