using UnityEngine;

public class Parallax : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public float animationSpeed = 0.5f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0f);
    }
}
