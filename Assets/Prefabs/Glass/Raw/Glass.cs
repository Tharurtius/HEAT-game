using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private Material glass;
    [SerializeField] private int amount = Shader.PropertyToID("_Amount");
    // Start is called before the first frame update
    void Start()
    {
        amount = Shader.PropertyToID("_Amount");
        GetComponent<MeshRenderer>().material.CopyPropertiesFromMaterial(glass);
        StartCoroutine(Break());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Break()
    {
        yield return new WaitForSeconds(2);
        GetComponent<MeshRenderer>().material.SetFloat(amount, 0.8f);
        Debug.Log(GetComponent<MeshRenderer>().material.GetFloat(amount));
    }
}
