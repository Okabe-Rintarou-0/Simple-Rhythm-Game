using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Note 
{
    public float spawnTime;

    public float hitTime;

    public string keyMap;

    public int GetKeyIndex() 
    {
        for (int i = 0; i < keyMap.Length; i++) {
            if (keyMap[i] != '0') {
                return i;
            }
        }

        return -1;
    }

    public int GetKeyValue(int keyIndex) {
        return keyMap[keyIndex] - '0';
    }

    public Note(string keyMap, float hitTime) 
    {
        this.keyMap = keyMap;
        this.hitTime = hitTime;
    }
}

[Serializable]
public class Chart 
{
    public int bpm;

    public Dictionary<int, string> keyMap;

    public string title;

    public string author;

    public string difficulty;

    public List<Note> notes;
}

public class ChartParser
{
    private static Dictionary<int, string> ReadKeys(StreamReader sr) {
        Dictionary<int, string> keys = new();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (line.Length == 0)
            {
                break;
            }
            string[] splitted = line.Split(" ");
            if (splitted.Length == 2) {
                keys[int.Parse(splitted[0])] = splitted[1];
            }
        }
        return keys;
    }

    private static string ReadTitle(string line) {
        return line[6..];
    }

    private static string ReadAuthor(string line)
    {
        return line[7..];
    }

    private static int ReadBPM(string line) 
    {
        return int.Parse(line.Split()[1]);
    }

    private static string ReadDifficulty(string line) 
    {
        return line[11..];    
    }

    private static List<Note> ReadNotes(StreamReader sr) 
    {
        List<Note> notes = new();
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (line.Length == 0)
            {
                continue;
            }
            String[] splitted = line.Split(" ");
            if (splitted.Length == 2)
            {
                notes.Add(new Note(splitted[0], float.Parse(splitted[1])));
            }
        }
        return notes;
    }

    public static Chart ParseChartFile(string filepath) 
    {
        Chart chart = new();
        StreamReader sr = File.OpenText(filepath);
        string line;
        while ((line = sr.ReadLine()) != null) {
            if (line.Length == 0) {
                continue;
            }

            if (line == "KEYS")
            {
                chart.keyMap = ReadKeys(sr);
            }
            else if (line.StartsWith("DIFFICULTY")) 
            {
                chart.difficulty = ReadDifficulty(line);
            }
            else if (line.StartsWith("TITLE"))
            {
                chart.title = ReadTitle(line);
            }
            else if (line.StartsWith("AUTHOR"))
            {
                chart.author = ReadAuthor(line);
            }
            else if (line.StartsWith("BPM"))
            {
                chart.bpm = ReadBPM(line);
            }
            else if (line.StartsWith("NOTES"))
            {
                chart.notes = ReadNotes(sr);
            }
        }
        return chart;
    } 
}
