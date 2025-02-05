using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FilltheBlankSpace
{
    public static void FilltheSpace(HashSet<Vector2Int> floorpositions, TilemapVisualizer tilemapVisualizer)
    {
        var blankSpace = FindtheBlankSpace(floorpositions, Direction2D.cardinalDirectionList);

        foreach(var position in blankSpace)
        {
            tilemapVisualizer.paintSingleTile(position);
        }
    }

    private static HashSet<Vector2Int> FindtheBlankSpace(HashSet<Vector2Int> floorpositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> blankPosition = new HashSet<Vector2Int>();

        foreach (var position in floorpositions)
        {
            foreach (var direction in directionList)
            {
                var checkPosition = position + direction;
                if(floorpositions.Contains(checkPosition) == false)
                {
                    blankPosition.Add(checkPosition);
                }
            }
        }

        return blankPosition;
    }


}