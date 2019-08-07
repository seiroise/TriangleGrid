using UnityEngine;

/// <summary>
/// 三角格子の管理用クラス。
/// </summary>
public static class TriangleLattice
{
	/// <summary>
	/// 正三角形における斜辺と高さの比。√3/2
	/// </summary>
	private const float hypotenuseToHeight = 0.866025f;

	/// <summary>
	/// 正方格子上の座標を三角格子上の座標に変換する。
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="size">三角形の斜辺の長さ</param>
	/// <returns></returns>
	public static Vector3 SquareToTriangle(Vector3 pos, float size)
	{
		float x = pos.x / size;
		float y = pos.y / (size * hypotenuseToHeight);

		Vector3 tPos = new Vector3(
			x - 0.5f * y,
			y
		);

		// z軸は、xとyの端数の加算が1を超えているかどうかで
		tPos.z = (tPos.x - Mathf.Floor(tPos.x) + tPos.y - Mathf.Floor(tPos.y)) >= 1f ? 1f : 0f;
		return tPos;
	}

	/// <summary>
	/// 正方格子上の座標を三角格子上の座標に変換する。
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="size">三角形の斜辺の長さ</param>
	/// <returns></returns>
	public static Vector3Int SquareToTriangleInt(Vector3 pos, float size)
	{
		Vector3 tPos = SquareToTriangle(pos, size);
		return new Vector3Int(
			Mathf.FloorToInt(tPos.x),
			Mathf.FloorToInt(tPos.y),
			(int)tPos.z
		);
	}

	/// <summary>
	/// 三角格子上の座標を正方格子上の座標に変換する。
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="size">三角形の斜辺の長さ</param>
	public static Vector3 TriangleToSquare(Vector3Int pos, float size)
	{
		int x = (pos.x + 1) >> 1;
		int y = pos.y;

		return new Vector3(
			x * size,
			y * size * hypotenuseToHeight,
			0f
		);
	}

	/// <summary>
	/// 三角格子上の座標から回転を計算する。
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public static Quaternion CalcRotation(Vector3Int pos)
	{
		return Quaternion.identity;
	}
}