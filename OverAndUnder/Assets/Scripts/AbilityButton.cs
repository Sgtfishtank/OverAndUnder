using UnityEngine;
using System.Collections;

public class AbilityButton : MonoBehaviour
{
    public GameMaster.Abilitys buttontype;
    public GameMaster GM;
    public bool active;
    void Start ()
    {
        GM = GameObject.Find("Game Master").GetComponent<GameMaster>();
        active = false;
    }
    void Update ()
    {
        active = GM.selectingAbility;
    }
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!active)
            {
                GM.highlightLanes(buttontype);
                active = GM.selectingAbility;
            }
            else
            {
                active = GM.selectingAbility;
                GM.unlightLanes();

            }
        }
    }

}
