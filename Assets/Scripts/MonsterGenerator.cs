using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGenerator : MonoBehaviour
{
    [Header("生成间隔")]
    public float generateInterval = 1f;

    [Header("生成怪物的父对象")]
    public Transform parent;

    [Header("当前生成怪物的移动速度")]
    public int currentMonsterMoveSpeed = 250;

    private long startTime;

    private int curNoteIdx;

    private int moveSpeed;

    private float moveNeedTime;

    private List<Note> notes;

    private GlobalConfig cfg;

    private Note curNote;

    public AudioSource audioSource;

    // for test only
    private string GetMonsterTypeFromNote(Note note) 
    {
        switch (note.GetKeyIndex()) 
        {
            case 0:
                return "Yellow";
            case 1:
                return "Green";
            case 2:
                return "Purple";
        }
        return "Dummy";
    }

    private void SpawnMonster(Transform parent, string type, int moveSpeed, Vector3 pos)
    {
        MonsterBase monster = MonsterPool.Instance.Fetch(type);
        monster.SetMoveSpeed(moveSpeed);
        monster.Spawn(parent, pos);
    }

    private void GenerateFirst() 
    {
        curNote = notes[curNoteIdx];
        audioSource.PlayOneShot(cfg.currentSong);
        startTime = TimeUtils.GetTimeStampMilliSecond();
    }

    private void FixedUpdate()
    {
        double elapsedTime = (TimeUtils.GetTimeStampMilliSecond() - startTime) / 1000.0;
        if (curNoteIdx < notes.Count && elapsedTime >= curNote.hitTime - moveNeedTime)
        {
            SpawnMonster(parent, GetMonsterTypeFromNote(curNote), moveSpeed, cfg.GetSpawnPos(curNote.GetKeyIndex()));
            ++curNoteIdx;
            if (curNoteIdx < notes.Count)
            {
                curNote = notes[curNoteIdx];
            }
        }
    }

    private void Start()
    {
        Chart chart = GlobalConfig.Instance.currentChart;
        cfg = GlobalConfig.Instance;
        curNoteIdx = 0;
        moveSpeed = cfg.monsterMoveSpeed;
        moveNeedTime = cfg.MoveDistance() / moveSpeed;
        notes = chart.notes;
        // skip some notes
        for (; curNoteIdx < notes.Count; curNoteIdx++)
        {
            if (notes[curNoteIdx].hitTime >= moveNeedTime)
            {
                break;
            }
        }

        GenerateFirst();
    }
}
