using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//生成corridor
public class CorridorFirstDungeonGenerator : SimpleRandomWalkMapGenerator
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1.0f)]
    private float roomPercent = 0.8f;
    
    //重写RunProceduralGeneration来生成Corridor
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        // 初始化地板坐标集合和潜在房间位置
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPosition = new HashSet<Vector2Int>();

        // 生成走廊
        CreateCorridors(floorPositions, potentialRoomPosition);

        // 生成房间
        HashSet<Vector2Int> roomPosition = createRoom(potentialRoomPosition);

        // 合并走廊和房间的坐标
        floorPositions.UnionWith(roomPosition);

        // 绘制到Tilemap
        tilemapVisualizer.paintFloorTile(floorPositions);
    }

    //基于潜在位置生成随机房间，复用父类的runRandomWalk方法。
    private HashSet<Vector2Int> createRoom(HashSet<Vector2Int> potentialRoomPosition)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        // 计算实际需要生成的房间数量（例如80%的潜在位置）
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomPosition.Count * roomPercent);

        // 随机选择房间位置（通过Guid.NewGuid()洗牌）
        List<Vector2Int> roomToCreate = potentialRoomPosition.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        // 对每个选中的位置生成房间
        foreach (var roomPosition in roomToCreate)
        {
            // 使用父类的runRandomWalk生成房间形状
            var roomFloor = runRandomWalk(SimpleRandomWalkParameters, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    //具体走廊生成方法
    private void CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPosition)
    {
        var currentPosition = startPosition; // 初始位置
        potentialRoomPosition.Add(currentPosition);

        for (int i = 0; i < corridorCount; i++)
        // 生成一条长度为corridorLength的直线走廊
        {
            var corridor = ProceduralGenerationAlgorithm.SimpleRandomWalkforCorridor(currentPosition, corridorLength);
            currentPosition = corridor[corridor.Count - 1];// 更新当前位置为走廊终点
            potentialRoomPosition.Add(currentPosition);// 记录终点为潜在房间位置
            floorPositions.UnionWith(corridor);// 将走廊坐标加入地板集合
        }
    }
}
