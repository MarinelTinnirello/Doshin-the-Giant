using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreativeModeInput : MonoBehaviour
{
    public float delay = 0.3f;              // delay after hitting button
    public ModifyTerrain modify;            // allows access to modifiers
    public GameObject GameObject;           // allows us to play with an object's scripts
    public FixedButton button;              // stops you from modifying land when trying to input modifiers

    public GameObject inputStrength;        // strength modifier
    public GameObject inputXBase;           // XBase modifier
    public GameObject inputZBase;           // ZBase modifier
    public GameObject inputWidth;           // width base modifier
    public GameObject inputHeight;          // height base modifier

    public GameObject panelStrengthLess;    // error panel for strength less than 0.0001
    public GameObject panelStrengthGreater; // error panel for strength greater than 0.002
    public GameObject panelXWidth;          // error panel for XBase exceeding WidthBase
    public GameObject panelXHeight;         // error panel for XBase exceeding HeightBase
    public GameObject panelZWidth;          // error panel for ZBase exceeding WidthBase
    public GameObject panelZHeight;         // error panel for ZBase exceeding HeightBase

    void Update()
    {
        /** If button is pressed, turn on ModifyTerrain script **/
        if (button.isPressed)
        {
            /* error panel handlers */
            strengthCheck();
            xBase();
            zBase();

            Invoke("setTrue", delay);
        }
    }

    void setTrue()
    {
        (GameObject.GetComponent<ModifyTerrain>() as MonoBehaviour).enabled = true;
    }

    /***** Handlers for error panel checks *****/
    void strengthCheck()
    {
        if (button.isPressed)
        {
            if (modify.strength > 0.002)
            {
                panelStrengthLess.SetActive(true);
                Debug.Log("Please choose a value below 0.001");
            }
            if (modify.strength < 0.00001)
            {
                panelStrengthGreater.SetActive(true);
                Debug.Log("Please choose a value above 0.00001");
            }
        }
    }

    void xBase()
    {
        if (button.isPressed)
        {
            if (modify.xBase >= modify.widthBase)
            {
                panelXWidth.SetActive(true);
                Debug.Log("XBase needs be less than the WidthBase");
            }
            if (modify.xBase >= modify.heightBase)
            {
                panelXHeight.SetActive(true);
                Debug.Log("XBase needs be less than the HeightBase");
            }
        }
    }

    void zBase()
    {
        if (button.isPressed)
        {
            if (modify.zBase >= modify.widthBase)
            {
                panelZWidth.SetActive(true);
                Debug.Log("ZBase needs be less than the WidthBase");
            }
            if (modify.zBase >= modify.heightBase)
            {
                panelZHeight.SetActive(true);
                Debug.Log("ZBase needs be less than the HeightBase");
            }
        }
    }

    /***** Handlers for modifiers *****/
    public void strength(string input)
    {
        float temp = float.Parse(input);

        modify.strength = temp;
    }

    public void xBase(string input)
    {
        int temp = int.Parse(input);

        modify.xBase = temp;
    }

    public void zBase(string input)
    {
        int temp = int.Parse(input);

        modify.zBase = temp;
    }

    public void width(string input)
    {
        int temp = int.Parse(input);

        modify.widthBase = temp;
    }

    public void height(string input)
    {
        int temp = int.Parse(input);

        modify.heightBase = temp;
    }
}
