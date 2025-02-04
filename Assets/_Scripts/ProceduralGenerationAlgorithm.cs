using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PCG生成时的核心算法
public static class ProceduralGenerationAlgorithm
{
    public static HashSet<Vector2Int> SimpleRandomWalkforRoom(Vector2Int startPosition, int walkLength)
    {
        HashSet<Vector2Int> roompath = new HashSet<Vector2Int>();

        var recordpreviousroomPosition = startPosition; // previousPosition = (0,0)
        roompath.Add(recordpreviousroomPosition);

        for (int i = 0; i < walkLength; i++)
        {
            var findnewPosition = recordpreviousroomPosition + Direction2D.getCardinalRandomDirection();
            recordpreviousroomPosition = findnewPosition;
            roompath.Add(recordpreviousroomPosition);
        }
        return roompath; // eg. (0,0),(0,1),(1,1),(1,2)...  // Hashset won't record the same value
    }

    public static List<Vector2Int> SimpleRandomWalkforCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridorpath = new List<Vector2Int>();

        var direction = Direction2D.getCardinalRandomDirection();

        var recordpreviouscorridorPosition = startPosition;
        corridorpath.Add(recordpreviouscorridorPosition);

        for (int i = 0;i < corridorLength; i++)
        {
            var findnewPosition = recordpreviouscorridorPosition + direction;
            recordpreviouscorridorPosition = findnewPosition;
            corridorpath.Add(recordpreviouscorridorPosition);
        }

        return corridorpath;
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
