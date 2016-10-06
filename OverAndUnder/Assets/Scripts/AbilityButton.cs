using UnityEngine;
using System.Collections;

public class AbilityButton : MonoBehaviour
{
    public GameMaster.Abilitys buttontype;
    public GameMaster GM;
    public bool active;
    public Color defaultcolor;
    public Color slowcolor;
    public Color wallcolor;
    public Color multicolor;
    public Color neutcolor;

    void Start ()
    {
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        active = false;
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolor);
        ColorUtility.TryParseHtmlString("#32538A00", out slowcolor);
        ColorUtility.TryParseHtmlString("#9D787800", out wallcolor);
        ColorUtility.TryParseHtmlString("#90769200", out multicolor);
        ColorUtility.TryParseHtmlString("#8B8A7600", out neutcolor);
    }
    void Update ()
    {
        if (active)
        {
            active = GM.selectingAbility;
            setColor();
        }
    }
    void setColor()
    {
        if(active)
        {
            switch (buttontype)
            {
                case GameMaster.Abilitys.SLOW:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", slowcolor);
                    break;
                case GameMaster.Abilitys.WALL:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", wallcolor);
                    break;
                case GameMaster.Abilitys.MULTIPLIER:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", multicolor);
                    break;
                case GameMaster.Abilitys.NEUTRAL:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", neutcolor);
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultcolor);
        }
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!active)
            {
                GM.highlightLanes(buttontype);
                active = GM.selectingAbility;
                setColor();
            }
            else
            {
                active = GM.selectingAbility;
                GM.unlightLanes();
                setColor();
            }
        }
    }

}
