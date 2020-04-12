using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIK : GenericSubTargetIK
{
    public AnimationCurve curve;
    private float defVlaue=0.5f;




    private Vector2 Point
    {
        get
        {
            if (process) return savePoint;

            var point = Physics2D.Raycast(targetObj.position, Vector2.down).point;

            if (Mathf.Abs( point.x - savePoint.x)> defVlaue)
            {
                currPoint = point;
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

        Bones[0].position = transform.position;

         for (int i = 1; i < Bones.Count; i++)
             Bones[i].position = Bones[i - 1].endPoint;
    }

    private bool process;

    public float Speed;

    private IEnumerator processMove()
    {
        process = true;

        var waiter=new WaitForFixedUpdate();
        var t = 0f;

        while (t < defVlaue)
        {
            savePoint = Vector2.Lerp(savePoint, currPoint, t / defVlaue);
            savePoint.y += curve.Evaluate(t/defVlaue);
            t += 0.1f*Speed;
            yield return waiter;
        }
        savePoint = Vector2.Lerp(savePoint, currPoint, t / defVlaue);
        process = false;
        defVlaue = Random.value;


    }


    public override Vector2 IKTarget => Point;



}

