using UnityEngine;

public class ItemShaderScript : MonoBehaviour
{
    public GameObject lightSource;
    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        material.SetVector("_LightPoint", lightSource.transform.position);
        material.SetFloat("_LightIntensity", lightSource.GetComponent<Light>().intensity);
    }
}
