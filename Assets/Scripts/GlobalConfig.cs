using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConfig : MonoBehaviour
{
    public static GlobalConfig Instance;

    private void Awake()
    {
        Instance = this;
        ReadChart();
    }

    [Header("打击点")]
    public List<RectTransform> hitPoints;

    [Header("生成点")]
    public List<RectTransform> spawnPoints;

    [Header("当前谱面")]
    public Chart currentChart;

    [Header("怪物移动速度")]
    public int monsterMoveSpeed;

    [Header("当前歌曲名")]
    public string currentSongName;

    [Header("当前歌曲")]
    public AudioClip currentSong;

    private float moveDistance;

    private void Start()
    {
        moveDistance = spawnPoints[0].anchoredPosition3D.x - hitPoints[0].anchoredPosition3D.x;
    }

    public float GetHitPosX() 
    {
        return hitPoints[0].anchoredPosition3D.x;
    }

    public float MoveDistance() 
    {
        return moveDistance;
    }

    public Vector3 GetSpawnPos(int index) 
    {
        return spawnPoints[index].anchoredPosition3D;
    }

    private void ReadChart() 
    {
        string path = Application.dataPath + $"/Charts/{currentSongName}.txt";
        try
        {
            Chart chart = ChartParser.ParseChartFile(path);
            currentChart = chart;
            Debug.Log("解析成功");
            currentSong = (AudioClip) Resources.Load($"AudioClips/{currentSongName}", typeof(AudioClip));
        }
        catch (Exception e)
        {
            Debug.LogError("发生错误：" + e);
        }
    }
}
