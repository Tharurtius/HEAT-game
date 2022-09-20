using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    [SerializeField] private Material glass;
    [SerializeField] private int amount;
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private GameObject particles;
    void Start()
    {
        amount = Shader.PropertyToID("_Amount");
        mesh = GetComponentInChildren<MeshRenderer>();
        //StartCoroutine(Test());
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
            mesh.gameObject.SetActive(false);
            particles.SetActive(true);
            StartCoroutine(End());
        }
    }
    IEnumerator End()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }

    //IEnumerator Test()
    //{
    //    yield return new WaitForSeconds(3f);
    //    ShatterGlass();
    //    yield return new WaitForSeconds(3f);
    //    ShatterGlass();
    //}
}
