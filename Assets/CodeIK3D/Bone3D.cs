using UnityEngine;

[System.Serializable]
public class Bone3D
{
    public Transform transf;
    public float lenght;

    public Quaternion rotation { get => transf.rotation; set => transf.rotation = value; }
    public Vector3 position { get => transf.position; set => transf.position = value; }
}
