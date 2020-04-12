using UnityEngine;

[System.Serializable]
public class Bone
{
    public Transform transf;
    public float lenght;

    public float rotation { get => transf.rotation.eulerAngles.z; set => transf.eulerAngles = Vector3.forward * value; }
    public Vector2 endPoint => position + (Vector2.right * Mathf.Cos(rotation / 57.2f) + Vector2.up * Mathf.Sin(rotation / 57.2f)) * lenght;
    public Vector2 position { get => (Vector2)transf.position; set => transf.position = value; }
}
