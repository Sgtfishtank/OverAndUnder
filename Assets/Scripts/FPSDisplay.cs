using UnityEngine;
using System.Collections;
public class FPSDisplay : MonoBehaviour
{
    int w = 640, h = 1136;

    GUIStyle style = new GUIStyle();
    Rect rect;

    float deltaTime = 0.0f;

    private void Start()
    {
        rect = new Rect(0, 0, w, h * 2 / 30);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 2 / 30;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}