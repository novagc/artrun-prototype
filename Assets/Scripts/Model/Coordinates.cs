using System;
using UnityEngine;

[Serializable]
public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }
    
    public bool InMap(int mapSize) => X.InRange(0, mapSize) && Y.InRange(0, mapSize);
    public bool InMap(int height, int width) => X.InRange(0, width) && Y.InRange(0, height); 
    
    public Vector2 ToVector2() => new Vector2(X, Y);

    public void Move(int x, int y)
    {
        X += x;
        Y += y;
    }
    
    public void Move(Coordinate coordinate)
    {
        Move(coordinate.X, coordinate.Y);
    }

    public void Move(Vector2 vector)
    {
        Move((int)vector.x, (int)vector.y);
    }
}