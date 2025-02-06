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

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spacetoSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(spacetoSplit);

        while(roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if(room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if(Random.value < 0.5f)
                {
                    if(room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if(room.size.x >= minWidth &&  room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = Random.Range(1, room.size.x);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = Random.Range(1, room.size.y);

        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.y, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z), new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));

        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
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
        new Vector2Int(-1,0), //left
    };

    //获取随机方向坐标
    public static Vector2Int getCardinalRandomDirection()
    {
        return cardinalDirectionList[Random.Range(0, cardinalDirectionList.Count)]; // Count: Gets the number of elements contained in the List<T>.
    }
}
