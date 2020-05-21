using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericIKLineRender : MonoBehaviour
{
    [SerializeField] protected Transform target = null;
    [SerializeField] protected LineRenderer Bones = null;


    public virtual Vector3 IKTarget => target.position;

    protected virtual void Update()
    {
        return;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(IKTarget, 0.2f);
    }
}
