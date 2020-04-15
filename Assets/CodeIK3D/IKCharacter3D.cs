using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCharacter3D : MonoBehaviour
{
    public IKStructure3D[] IKs;
    public Rigidbody Rig;
    public Vector3 lowObject;

    public float a;
    public float move;
    public float jump;
    public float rote;


    float strange;

    float strange1;
    float strange2;



    private void Update()
    {

        //Rig.rotation = (strange1 - strange2) * rote;



        foreach (var item in IKs)
        {
            item.BodyPart.Active = strange > 0.1f;
        }



        if (Input.GetAxis("Horizontal") != 0)
        {
            Rig.AddTorque(Input.GetAxis("Horizontal") * move * Vector3.up);
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            Rig.AddForce(Input.GetAxis("Vertical") * move * transform.forward);
        }
    }

    private RaycastHit HitInfo;

    private void FixedUpdate()
    {
        Physics.Raycast(Vector3.right * 0.2f + lowObject + Rig.position, Vector3.down, out HitInfo);
        strange1 = 1f - Mathf.Clamp01(HitInfo.distance);
        Physics.Raycast(Vector3.left * 0.2f + lowObject + Rig.position, Vector3.down, out HitInfo);
        strange2 = 1f - Mathf.Clamp01(HitInfo.distance);

        strange = (strange1 + strange2) / 2f;

        Rig.AddForce(Vector2.up * a * strange);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(lowObject + Rig.position, Rig.position + Vector2.up * strange);
    }
}

[System.Serializable]
public class IKStructure3D
{
    public GenericIK3D BodyPart;
    public Vector3 TargetPosition => BodyPart.IKTarget;

}
