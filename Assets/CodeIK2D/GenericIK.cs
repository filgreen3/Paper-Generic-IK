using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericIK : MonoBehaviour
{
    public Transform targetObj;

    [SerializeField]
    protected List<Bone> Bones = new List<Bone>();

    public bool Active;


    public virtual Vector2 IKTarget => targetObj.position;

    protected virtual void Update()
    {
        if (!Active) return;
        for (int i = Bones.Count - 1; i >= 0; i--)
        {
            var item = Bones[i];

            Vector2 target;
            var t = 1;

            if (i < Bones.Count - 1)
                target = Bones[i + 1].position;
            else
                target = IKTarget;


            var dir = target - item.position;


            dir.Normalize();
            var angle = Mathf.Atan2(t * dir.y, dir.x) * 57.2f;


            item.rotation = angle;
            item.position = target - dir;
        }

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(IKTarget, 0.2f);
    }
}
