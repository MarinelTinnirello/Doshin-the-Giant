using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateCreativeMode : MonoBehaviour
{
    public FixedButton button;          // FixedButton has Pointer events
    public GameObject GameObject;       // allows us to play with an object's scripts
    private ModifyTerrain modify;       // allows access to the variables to change

    void Update()
    {
        /** If button is pressed, turn on ModifyTerrain script **/
        if (button.isPressed)
        {
            (GameObject.GetComponent<ModifyTerrain>() as MonoBehaviour).enabled = true;
        }
    }
}
