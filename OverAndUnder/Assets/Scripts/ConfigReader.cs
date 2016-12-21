using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;

public class ConfigReader
{
    public string[] lines;
    static FileStream fs;
#if UNITY_EDITOR
    private static string path = /*Application.persistentDataPath +*/ "Assets/Config.txt";
#endif
#if !UNITY_EDITOR
    private static string path = Application.persistentDataPath + "Config.txt";
#endif


    private static ConfigReader instance = null;
    private ConfigReader()
    {
        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception)
        {

            lines = File.ReadAllLines("Config.txt");
        }
        
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
    public int getValueInt(string name)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string[] s = Regex.Split(lines[i], @"-\s");
            if (s[0] == name)
                return int.Parse(s[1]);
        }
        return 0;
    }
    public float getValueFloat(string name)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            string[] s = Regex.Split(lines[i], @"-\s");
            if (s[0] == name)
                return float.Parse(s[1]);
        }
        return 0;
    }
    public void changeValue(string name, int value)
    {
        int counter = 0;
        foreach (string s in lines)
        {
            string[] sp = Regex.Split(s, @"-\s");
            if (sp[0] == name)
            {
                lines[counter] = name + "- " + value;
            }
            counter++;
        }
        File.WriteAllLines(path, lines);
        try
        {
            lines = File.ReadAllLines(path);
        }
        catch (Exception)
        {

            lines = File.ReadAllLines("Config.txt");
        }
    }
}