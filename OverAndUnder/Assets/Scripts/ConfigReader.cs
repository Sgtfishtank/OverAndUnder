using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;

public class ConfigReader
{
    public string[] lines;

    private static ConfigReader instance = null;
    private ConfigReader()
    {
        lines = File.ReadAllLines("Config.txt");
    }
    public static ConfigReader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ConfigReader();
            }
            return instance;
        }
    }
    public int getValue(string name)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string[] s = Regex.Split(lines[i], @"-\s");
            if (s[0] == name)
                return int.Parse(s[1]);
        }
        return 0;
    }
    /*void Start() { }
    void Update() { }*/
}
