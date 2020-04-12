using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKCharacter : MonoBehaviour
{
    public IKStructure[] IKs;
    public Rigidbody2D Rig;
    public Vector2 lowObject;

    public float a;
    public float move;
    public float jump;
    public float rote;


    float strange;

    float strange1;
    float strange2;



    private void Update()
    {

        Rig.rotation = (strange1- strange2) * rote;



        foreach (var item in IKs)
        {
            item.BodyPart.Active = strange > 0.1f;
        }



        if (Input.GetAxis("Horizontal") != 0)
        {
            Rig.AddForce(Input.GetAxis("Horizontal") * move * Vector2.right);
        }
        if (Input.GetAxis("Vertical") > 0 && strange1 != 0)
        {
            Rig.AddForce(jump * transform.up);
        }
    }

    private void FixedUpdate()
    {
        strange1 = 1f - Mathf.Clamp01(Physics2D.Raycast(Vector2.right * 0.2f + lowObject + Rig.position, Vector2.down).distance);
        strange2 = 1f - Mathf.Clamp01(Physics2D.Raycast(Vector2.left * 0.2f + lowObject + Rig.position, Vector2.down).distance);

        strange = (strange1 + strange2) / 2f;

        Rig.AddForce(Vector2.up * a * strange);

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lowObject + Rig.position, Rig.position + Vector2.up * strange);
    }
}

[System.Serializable]
public class IKStructure
{
    public GenericIK BodyPart;
    public Vector2 TargetPosition => BodyPart.IKTarget;

}
