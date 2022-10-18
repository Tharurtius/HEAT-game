using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    private void Start()
    {
        surfaces = GetComponentsInChildren<NavMeshSurface>();
    }
    public void MeshUpdate()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh(surfaces[i].navMeshData);
            Debug.Log("NavMesh updated!");
        }

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            MeshUpdate();
            Debug.Log("NavMesh updated");
        }

    }
}
