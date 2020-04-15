using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIK3D : GenericSubTargetIK3D
{
    public AnimationCurve curve;
    private float defVlaue = 0.5f;


    private RaycastHit HitInfo;

    private Vector3 Point
    {
        get
        {
            if (process) return savePoint;
            Physics.Raycast(targetObj.position, Vector3.down, out HitInfo);
            var point = HitInfo.point;

            if ((point - savePoint).magnitude > defVlaue)
            {
                currPoint = point + (point - savePoint).normalized * defVlaue * 0.5f;

                StartCoroutine(processMove());
            }

            return savePoint;
        }
    }


    private Vector3 currPoint;
    private Vector3 savePoint;




    protected override void Update()
    {
        base.Update();

        Bones[0].position = transform.position;

        for (int i = 1; i < Bones.Count; i++)
            Bones[i].position = Bones[i - 1].position + Bones[i - 1].transf.forward * Bones[i - 1].lenght;


    }

    private bool process;

    public float Speed;

    private IEnumerator processMove()
    {
        process = true;

        var waiter = new WaitForFixedUpdate();
        var t = 0f;

        while (t < defVlaue)
        {
            savePoint = Vector3.LerpUnclamped(savePoint, currPoint, t / defVlaue);
            savePoint.y += curve.Evaluate(t / defVlaue);
            t += 0.1f * Speed;
            yield return waiter;
        }
        savePoint = Vector3.Lerp(savePoint, currPoint, t / defVlaue);
        process = false;

        defVlaue = Random.value;


    }

    public override Vector3 IKTarget => Point;
}

