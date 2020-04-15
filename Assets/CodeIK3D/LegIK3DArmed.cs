using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIK3DArmed : LegIK3D
{
    public Vector3 MoveView;
    public Transform Armor;

    protected override void Update()
    {
        base.Update();

        Armor.LookAt(Armor.position + MoveView);


    }
}

