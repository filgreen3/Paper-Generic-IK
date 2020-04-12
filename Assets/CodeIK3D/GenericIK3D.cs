using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericIK3D : MonoBehaviour
{
    public Transform targetObj;

    [SerializeField]
    protected List<Bone3D> Bones = new List<Bone3D>();

    public bool Active;


    public virtual Vector3 IKTarget => targetObj.position;

    protected virtual void Update()
    {
        if (!Active) return;
        for (int i = Bones.Count - 1; i >= 0; i--)
        {
            var item = Bones[i];
            Vector3 target;
            if (i < Bones.Count - 1)
                target = Bones[i + 1].position;
            else
                target = IKTarget;

            var dir = target - item.position;
            dir.Normalize();

            item.transf.LookAt(target);
            item.position = target - dir;
        }

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(IKTarget, 0.2f);
    }
}
