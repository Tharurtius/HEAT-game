using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    private NavMeshSurface navMesh;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshSurface>();
        if (navMesh == null)
            //aIAgent = gameObject.AddComponent(typeof(AIAgent)) as AIAgent;
            navMesh = gameObject.AddComponent<NavMeshSurface>();
    }
    public void RebakeNavMesh()
    {
        navMesh.BuildNavMesh();
    }
}
