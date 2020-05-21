using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSubTargetIKLineRender : GenericIKLineRender
{

    public float Weight;
    public float Lenght;

    public Transform SubIKTarget;

    protected override void Update()
    {
        for (int i = Bones.positionCount - 2; i >= 0; i--)
        {
            Vector3 dir;
            Vector3 target;

            if (i < Bones.positionCount - 2)
            {
                target = Bones.GetPosition(i + 1);
                dir = target - Bones.GetPosition(i);

                if (i == Bones.positionCount - 3)
                {
                    dir = target - (Bones.GetPosition(i - 1) * Weight + SubIKTarget.position * (1 - Weight));
                }
            }
            else
            {
                target = IKTarget;
                dir = Vector3.zero;

            }

            dir.Normalize();

            var pos = target - dir * Lenght;
            pos.z = 0;
            Bones.SetPosition(i, pos);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(SubIKTarget.position, 0.2f);
    }
}
