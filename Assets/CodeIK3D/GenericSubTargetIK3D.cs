using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSubTargetIK3D : GenericIK3D
{

    public float Weight;
    public Transform SubIKTarget;

    protected override void Update()
    {
        if (!Active) return;
        for (int i = Bones.Count - 1; i >= 0; i--)
        {
            var dir = Vector3.zero;
            var item = Bones[i];
            Vector3 target;
            if (i < Bones.Count - 1)
            {
                target = Bones[i + 1].position;
                dir = target - item.position;
            }
            else
            {
                target = IKTarget;
                dir = target - (item.position * Weight + SubIKTarget.position * (1 - Weight));
            }


            dir.Normalize();

            item.transf.LookAt(target);
            item.position = target - dir;
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(SubIKTarget.position, 0.2f);
    }
}
