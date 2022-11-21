using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [Header("π÷ŒÔ‘§…Ë")]
    public StringGameObjectMap monsterPrefabMap;

    private readonly object Lock = new object();

    private Dictionary<string, Queue<MonsterBase>> qMap = new Dictionary<string, Queue<MonsterBase>>();

    public static MonsterPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    public MonsterBase Fetch(string monsterType) {
        MonsterBase monster = null;
        lock (Lock) {
            if (qMap.ContainsKey(monsterType) && qMap[monsterType].Count > 0)
            {
                monster = qMap[monsterType].Dequeue();
            }
        } 
        
        // If there is no such monster in the pool now, instantiate one.
        if (monster == null) {
            GameObject monsterGO = Instantiate(monsterPrefabMap[monsterType], new Vector3(0, 0, 0), Quaternion.identity);
            monster = monsterGO.GetComponent<MonsterBase>();
        }

        return monster;
    }

    public void Return(MonsterBase monster) {
        string monsterType = monster.Type();
        lock (Lock)
        {
            if (!qMap.ContainsKey(monsterType)) 
            {
                qMap.Add(monsterType, new Queue<MonsterBase>());
            }
            qMap[monsterType].Enqueue(monster);
        }
    }
}
