using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int mapWidth = 20, mapHeight = 20;
    [SerializeField]
    [Range(0,10)]
    private int offset = 1;
    [SerializeField]
    private bool randonWalkRooms = false;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithm.BinarySpacePartitioning
            (new BoundsInt((Vector3Int)startPosition, new Vector3Int(mapWidth, mapHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        floor = CreateSimpleRooms(roomsList);

        tilemapVisualizer.paintFloorTile(floor);
        FilltheBlankSpace.FilltheSpace(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (var rooms in roomsList)
        {
            for(int col = offset; col < rooms.size.x - offset; col++)
            {
                for (int row = offset; row < rooms.size.y - offset; row++)
                {
                    Vector2Int Position = (Vector2Int)rooms.min + new Vector2Int(col, row);
                    floor.Add(Position);
                }
            }
        }
        return floor;
    }
}
