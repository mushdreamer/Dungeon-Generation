using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class SimpleRandomWalkMapGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    private SimpleRandomWalkData SimpleRandomWalkParameters;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = runRandomWalk();
        /*foreach (var position in floorPositions)
        {
            Debug.Log(position);
        }*/
        tilemapVisualizer.Clear();
        tilemapVisualizer.paintFloorTile(floorPositions);
    }

    protected HashSet<Vector2Int> runRandomWalk()
    {
        var currentPosition = startPosition;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        for (int i = 0; i < SimpleRandomWalkParameters.iterations; i++)
        {
            var path = ProceduraGenerationAlgorithm.SimpleRandomWalk(currentPosition, SimpleRandomWalkParameters.walkLength);
            floorPositions.UnionWith(path);
            if (SimpleRandomWalkParameters.startRandomlyEachIteration)
            {
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
            }
        }
        return floorPositions;
    }
}
