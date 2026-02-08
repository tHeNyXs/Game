using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSE_Test : CutsceneElementBase
{
    public override void Execute()
    {
        StartCoroutine(WaitAndAdvance());
        Debug.Log("Excuting " + name);
    }
}
