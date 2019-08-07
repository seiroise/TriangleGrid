using UnityEngine;
using TMPro;

public sealed class TriangleCell : MonoBehaviour
{
    // 左下の頂点から中心に向かうベクトル。
    private static readonly Vector3 dirToCenter = new Vector3(Mathf.Cos(Mathf.PI / 6f), Mathf.Sin(Mathf.PI / 6f));

    public TextMeshPro text;
    public Vector3Int coordinate;

    public float size = 0f;

    public Vector3 GetCenterPosition()
    {
        return transform.position + transform.rotation * dirToCenter * size * 0.5f;
    }

	public void Snap(float size)
	{

	}
}