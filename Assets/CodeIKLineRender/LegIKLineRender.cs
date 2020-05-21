using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIKLineRender : GenericSubTargetIKLineRender
{
    public AnimationCurve curve;
    private Vector2 Point
    {
        get
        {
            if (process) return savePoint;
            var point = Physics2D.Raycast(target.position, Vector3.down).point;

            if ((point - savePoint).magnitude > 1)
            {
                currPoint = point + (point - savePoint).normalized * 0.75f;
                StartCoroutine(processMove());
            }

            return savePoint;
        }
    }


    private Vector2 currPoint;
    private Vector2 savePoint;




    protected override void Update()
    {
        base.Update();

        Bones.SetPosition(0, transform.position);

        for (int i = 1; i < Bones.positionCount; i++)
            Bones.SetPosition(i, Bones.GetPosition(i - 1) + (Bones.GetPosition(i) - Bones.GetPosition(i - 1)).normalized * Lenght);

        Bones.SetPosition(Bones.positionCount - 2, savePoint);
        Bones.SetPosition(Bones.positionCount - 1, Bones.GetPosition(Bones.positionCount - 2) + (Vector3.right * 0.4f));



    }

    private bool process;

    public float Speed;

    private IEnumerator processMove()
    {
        process = true;

        var waiter = new WaitForFixedUpdate();
        var t = 0f;

        while (t < 1f)
        {
            savePoint = Vector3.LerpUnclamped(savePoint, currPoint, t);
            savePoint.y += curve.Evaluate(t);
            t += 0.1f * Speed;
            yield return waiter;
        }
        savePoint = currPoint;
        process = false;
    }

    public override Vector3 IKTarget => Point;
}

