using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGameHealthAndShieldBar : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] Slider shieldBar;
    // Start is called before the first frame update
    public void setHelathSlidersMax(float value)
    {
        healthBar.maxValue = value;
        setHelathSlidersValue(value);
    }
    public void setHelathSlidersValue(float value)
    {
        if (value <= 0)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            //Debug.Log("Changing Health: " + value);
            healthBar.value = value;
        }

    }
    public void setShieldSlidersMax(float value)
    {
        if (value <= 0)
        {
            shieldBar.gameObject.SetActive(false);
        } 
        else
        {
            shieldBar.maxValue = value;
            setShieldSlidersValue(value);
        }

    }
    public void setShieldSlidersValue(float value)
    {
        if (value <= 0)
        {
            shieldBar.gameObject.SetActive(false);
        }
        else
        {
            //Debug.Log("Changing Shield: "+value);
            shieldBar.value = value;
        }

    }
}
