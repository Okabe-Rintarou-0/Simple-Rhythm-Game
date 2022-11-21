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

    [Header("�����")]
    public List<RectTransform> hitPoints;

    [Header("���ɵ�")]
    public List<RectTransform> spawnPoints;

    [Header("��ǰ����")]
    public Chart currentChart;

    [Header("�����ƶ��ٶ�")]
    public int monsterMoveSpeed;

    [Header("��ǰ������")]
    public string currentSongName;

    [Header("��ǰ����")]
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
            Debug.Log("�����ɹ�");
            currentSong = (AudioClip) Resources.Load($"AudioClips/{currentSongName}", typeof(AudioClip));
        }
        catch (Exception e)
        {
            Debug.LogError("��������" + e);
        }
    }
}
