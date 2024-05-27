using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRender;
    private Color _color;

    private void Start()
    {
        SetColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
    }

    public void SetColor(Color color)
    {
        _meshRender.material.color = color;
    }

}
