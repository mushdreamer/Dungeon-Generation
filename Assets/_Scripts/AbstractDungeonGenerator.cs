using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;

    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero; //定义dungeon生成的初始点的位置为（0，0）


    //在每次调用时，unity都会先清除之前生成的dungeon然后在生成一个新的
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear(); //清除上一次调用时生成的dungeon
        RunProceduralGeneration(); //生成一个新的dungeon,由子类实现
    }

    protected abstract void RunProceduralGeneration();
}
