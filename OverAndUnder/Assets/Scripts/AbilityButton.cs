using UnityEngine;
using System.Collections;

public class AbilityButton : MonoBehaviour
{
    public Abilitys.AbilitysEnum buttontype;
    public Abilitys AM;
    public bool active;
    public Color defaultcolorEmisson;
    public Color slowcolorEmission;
    public Color wallcolorEmisson;
    public Color multicolorEmisson;
    public Color switchcolorEmisson;
    public Color defaultcolor;
    public Color slowcolor;
    public Color wallcolor;
    public Color multicolor;
    public Color switchcolor;
    bool cd;
    

    void Start ()
    {
        AM = GameObject.Find("Game Master(Clone)").GetComponent<Abilitys>();
        active = false;
        ColorUtility.TryParseHtmlString("#00000000", out defaultcolorEmisson);
        ColorUtility.TryParseHtmlString("#32538A00", out slowcolorEmission);
        ColorUtility.TryParseHtmlString("#9D787800", out wallcolorEmisson);
        ColorUtility.TryParseHtmlString("#90769200", out multicolorEmisson);
        ColorUtility.TryParseHtmlString("#8B8A7600", out switchcolorEmisson);

        ColorUtility.TryParseHtmlString("#3C3C3CFF", out defaultcolor);
        ColorUtility.TryParseHtmlString("#6695FFFF", out slowcolor);
        ColorUtility.TryParseHtmlString("#F05050FF", out wallcolor);
        ColorUtility.TryParseHtmlString("#D841FBFF", out multicolor);
        ColorUtility.TryParseHtmlString("#7346E4FF", out switchcolor);
    }
    void Update ()
    {
    }
    public void setColor()
    {
        if(active)
        {
            switch (buttontype)
            {
                case Abilitys.AbilitysEnum.SLOW:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", slowcolorEmission);
                    break;
                case Abilitys.AbilitysEnum.WALL:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", wallcolorEmisson);
                    break;
                case Abilitys.AbilitysEnum.MULTIPLIER:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", multicolorEmisson);
                    break;
                case Abilitys.AbilitysEnum.SWITCH:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", switchcolorEmisson);
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", defaultcolorEmisson);
        }
    }
    public void isCD(bool CD)
    {
        cd = CD;
        active = false;
        if (!CD)
        {
            switch (buttontype)
            {
                case Abilitys.AbilitysEnum.SLOW:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_Color", slowcolor);
                    break;
                case Abilitys.AbilitysEnum.WALL:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_Color", wallcolor);
                    break;
                case Abilitys.AbilitysEnum.MULTIPLIER:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_Color", multicolor);
                    break;
                case Abilitys.AbilitysEnum.SWITCH:
                    transform.GetComponent<MeshRenderer>().material.SetColor("_Color", switchcolor);
                    break;
                default:
                    break;
            }
        }
        else
        {
            transform.GetComponent<MeshRenderer>().material.SetColor("_Color", defaultcolor);
        }
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!cd && !active)
            {
                AM.activateAbility(buttontype, 0);
                setColor();
                active = true;
            }
            else if(active)
            {
                AM.disableSlow();
                active = false;
            }
            /*else
            {
                active = AM.selectingAbility;
                AM.unlightLanes();
                setColor();
            }*/
        }
    }

}
