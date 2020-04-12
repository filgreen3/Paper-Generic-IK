using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSubTargetIK : GenericIK
{

    public float Weight;
    public Transform SubIKTarget;

    protected override void Update()
    {
        if (!Active) return;
        for (int i = Bones.Count - 1; i >= 0; i--)
        {
            var item = Bones[i];

            Vector2 target;
            var dir = Vector2.zero;
            var t = 1;

            if (i < Bones.Count - 1)
            {
                target = Bones[i + 1].position;
                dir = target - item.position;
            }
            else
            {
                target = IKTarget;
                dir = target - (item.position * Weight + (Vector2)SubIKTarget.position * (1 - Weight));
            }

            dir.Normalize();
            var angle = Mathf.Atan2(t * dir.y, dir.x) * 57.2f;


            item.rotation = angle;
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
