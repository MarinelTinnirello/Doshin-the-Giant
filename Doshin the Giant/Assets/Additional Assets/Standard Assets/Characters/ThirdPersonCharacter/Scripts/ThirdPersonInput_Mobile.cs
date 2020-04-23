using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonInput_Mobile : MonoBehaviour
{
    public FixedJoystick LeftJoystick;                      // Joystick prefab
    public FixedButton Button;                              // FixedButton has Pointer events
    protected ThirdPersonUserControl_Mobile Controller;     // gives access to PlayerController
    public GameObject GameObject;                           // allows us to play with an object's scripts

    /**** Before first frame update, grab 3rd Person Controller ****/
    void Start()
    {
        Controller = GetComponent<ThirdPersonUserControl_Mobile>();
    }

    // Update is called once per frame
    /**** Once per frame, set jump to Button press, and Joystick to read X and Y-axes ****/
    void Update()
    {
        Controller.m_Jump = Button.isPressed;
        Controller.horInput = LeftJoystick.input.x;
        Controller.verInput = LeftJoystick.input.y;

        /** If either Button or Joystick is pressed, set ModifyTerrain to false **/
        if ((Button.isPressed) || (LeftJoystick.isPressed))
        {
            (GameObject.GetComponent<ModifyTerrain>() as MonoBehaviour).enabled = false;
        }
    }
}
