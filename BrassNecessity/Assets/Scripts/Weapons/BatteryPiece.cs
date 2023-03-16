using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BatteryPiece : MonoBehaviour
{
    private MeshRenderer mesh;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void SetMaterial(Material materialToSet)
    {
        if (mesh != null)
        {
            mesh.material = materialToSet;

        }
        else
        {
            mesh = GetComponent<MeshRenderer>();
            mesh.material = materialToSet;
        }
    }
}
