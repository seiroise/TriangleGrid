using UnityEngine;

public sealed class Board : MonoBehaviour
{
    private const float T = 0.866025f;  // 斜辺と高さの比。

    public TriangleCell part;
    public float stride = 1f;
    public int xMin = -10;
    public int xMax = 10;
    public int yMin = -10;
    public int yMax = 10;

    private void OnEnable()
    {
        Build();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mPos = Input.mousePosition;
            mPos.z = -Camera.main.transform.position.z;
            mPos = Camera.main.ScreenToWorldPoint(mPos);
            Vector3Int tPos = SquareToTriangularInt(mPos);
            Debug.Log(mPos + "->" + tPos + ",");
        }
    }

    private void Build()
    {
        CleanUp();

        if (part == null) return;

        for (int x = xMin; x < xMax; ++x)
        {
            for (int y = yMin; y < yMax; ++y)
            {
                var p = Instantiate(part, transform);
                p.size = stride;
                Place(p.transform, new Vector3Int(x, y, 0), stride);
                p.text.text = SquareToTriangularInt(p.GetCenterPosition()).ToString();
            }
        }
    }

    private void CleanUp()
    {
        if (transform.childCount == 0) return;
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    /// <summary>
    /// 三角グリッド上の座標に配置する。
    /// 三角グリッドは平面だがxyzの3軸で表現する。
    /// </summary>
    /// <param name="position"></param>
    private void Place(Transform transform, Vector3Int position, float stride)
    {
        int xa = position.x & 1;            // 回転に対して影響。偶数 : 0, 奇数 : 180
        int xb = (position.x + 1) >> 1;     // 位置に対して影響。(1, 2, 2, 3, 3, 4, 4, ... ) という数列を生成。

        Vector3 pos = new Vector3(xb * stride + position.y * stride * 0.5f, position.y * stride * T);
        Quaternion rot = Quaternion.Euler(0f, 0f, 60f * xa);
        transform.rotation = rot;
        transform.position = pos;

        // 色分けしてみる。
        var sprite = transform.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = xa == 0 ? Color.white : Color.gray;
        }
    }

    /// <summary>
    /// 正方格子上の座標を三角格子上の座標に変換する。
    /// </summary>
    /// <param name="sPos"></param>
    /// <returns></returns>
    public Vector3 SquareToTriangular(Vector2 sPos)
    {
        float x = sPos.x / stride;
        float y = sPos.y / (stride * T);

        Vector3 tPos = new Vector3(
            x - 0.5f * y,
            y
        );

        // z軸は、xとyの端数の加算が1を超えているかどうかで
        tPos.z = (tPos.x - Mathf.Floor(tPos.x) + tPos.y - Mathf.Floor(tPos.y)) >= 1f ? 1f : 0f;
        return tPos;
    }

    /// <summary>
    /// 正方格子上の座標を三角格子上の座標に変換する
    /// </summary>
    /// <param name="sPos"></param>
    /// <returns></returns>
    public Vector3Int SquareToTriangularInt(Vector2 sPos)
    {
        Vector3 tPos = SquareToTriangular(sPos);
        return new Vector3Int(
            Mathf.FloorToInt(tPos.x),
            Mathf.FloorToInt(tPos.y),
            (int)tPos.z
        );
    }
}