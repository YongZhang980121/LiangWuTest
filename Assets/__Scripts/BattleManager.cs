using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Transform canvasTransform;
    private void Awake()
    {
         Application.targetFrameRate = 120;
         Global.battleManager = this;
    }
}
