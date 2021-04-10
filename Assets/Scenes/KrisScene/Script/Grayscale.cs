using UnityEngine;

public class Grayscale : MonoBehaviour
{

    public Material grayscaleMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, grayscaleMaterial);
    }
}
