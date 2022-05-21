using UnityEngine;
public static class Utils
{
    public static bool InRangeInclusive(this int num, int min, int max) => num >= min && num <= max;
    public static bool InRangeInclusive(this float num, float min, float max) => num >= min && num <= max;
    public static bool InRangeInclusive(this int num, float min, float max) => num >= min && num <= max;
    public static bool InRangeInclusive(this float num, int min, int max) => num >= min && num <= max;
    
    public static bool InRange(this int num, int min, int max) => num >= min && num < max;
    public static bool InRange(this float num, float min, float max) => num >= min && num < max;
    public static bool InRange(this int num, float min, float max) => num >= min && num < max;
    public static bool InRange(this float num, int min, int max) => num >= min && num < max;

    public static Vector2 RotateTo180(this Vector2 coords, float maxX, float maxY) => new Vector2(maxX - coords.x, maxY - coords.y);
    public static Vector3 RotateTo180(this Vector3 coords, float maxX, float maxZ) => new Vector3(maxX - coords.x, coords.y, maxZ - coords.z);

    public static Vector2 WithoutY(this Vector3 coords) => new Vector2(coords.x, coords.z);
}
