//
using UnityEngine;

public class Shatter : MonoBehaviour
{
    [SerializeField] private Material glass;
    [SerializeField] private int amount;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject particles;
    private NavMeshUpdate _navMeshUpdate;
    private bool trigger = false;
    [SerializeField] private GameObject obstacle;
    //NavMeshUpdate navMeshUpdate;
    void Start()
    {
        amount = Shader.PropertyToID("_Amount");
        mesh = GetComponentInChildren<MeshRenderer>();

        if (_navMeshUpdate == null)
            _navMeshUpdate = gameObject.AddComponent<NavMeshUpdate>();
        /*if (navMeshUpdate == null)
            navMeshUpdate = gameObject.AddComponent<NavMeshUpdate>(); */
        //StartCoroutine(Test());
    }

    private void Update()
    {
        if(trigger)
        {
            trigger = false;
            _navMeshUpdate.MeshUpdate();
            Debug.Log("Trigger");
        }
    }
    public void ShatterGlass()
    {
        if (mesh.material.GetFloat(amount) < 0.7f)
        {
            mesh.material.CopyPropertiesFromMaterial(glass);
            mesh.material.SetFloat(amount, 0.8f);
        }
        else
        {
            Destroy(mesh.gameObject);
            trigger = true;
            particles.SetActive(true);
            /*
            * if (enemy is on glass floor)
            *   turn off its <NavMeshAgent>
            *   turn on its <RigidBody> gravity and disable its kinematics
            *   check velocity until it reaches zero (it lands on surface)
            *   if (enemy has landed)
            *       turn off gravity and enable kinematics
            *       turn on <NavMeshAgent>
            *   endif
            * endif
            */



            //StartCoroutine(End());
            if (obstacle != null)
            {
                obstacle.SetActive(true);
            }
        }
    }


    //IEnumerator End()
    //{
    //    yield return new WaitForSeconds(15f);
    //    Destroy(gameObject);
    //}


    //IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(3f);
    //    ShatterGlass();
    //    yield return new WaitForSeconds(3f);
    //    ShatterGlass();
    //}
}
