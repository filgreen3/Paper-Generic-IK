using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolowCamera : MonoBehaviour
{

    private Vector3 Offset;
    [SerializeField] private float mul = default;

    [SerializeField] private float downBorder = 7;
    [SerializeField] private float upBorder = 20;

    private float lerpTime;

    public static Vector3 zeroPoint = Vector3.zero;

    public Transform objectToFolow;

    // Update is called once per frame
    void FixedUpdate()
    {

        var pos = Vector3.Lerp(transform.position, objectToFolow.position + Offset, 0.55f);
        pos.z = -10;
        pos.y = pos.y < downBorder ? downBorder : pos.y > upBorder ? upBorder : pos.y;
        transform.position = pos;


    }


}