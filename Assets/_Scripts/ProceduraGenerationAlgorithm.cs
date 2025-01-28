using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduraGenerationAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(startPosition);
        var previousPosition = startPosition; // previousPosition = (0,0)

        for (int i = 0; i < walkLength; i++)
        {
            var newPosition = previousPosition + Direction2D.getCardinalRandomDirection(); // eg. (0,0)+(0,1)=(1,1) Up
            path.Add(newPosition);
            previousPosition = newPosition;
        }
        return path; // eg. (0,0),(0,1),(1,1),(1,2)...  // Hashset won't record the same value
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.getCardinalRandomDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0;i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}

//存储方向坐标
public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //up
        new Vector2Int(1,0), //right
        new Vector2Int(0,-1), //down
        new Vector2Int(-1,0) //left
    };

    //获取随机方向坐标
    public static Vector2Int getCardinalRandomDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)]; // Count: Gets the number of elements contained in the List<T>.
    }
}
