using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public void Click()
    {
        string city = GameObject.Find("CitySearch").GetComponent<Text>().text;
        //Debug.Log(city);
        autoload sn = gameObject.GetComponent<autoload>();
        sn.UpdateCity();
    }
}
