using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroolBarStartPos : MonoBehaviour
{
    [SerializeField]List<ScrollRect> _sRect = new List<ScrollRect>();

    void Start()
    {
        setScroolReactVerticalPos();
    }
    public void setScroolReactVerticalPos()
    {
        foreach(var item in _sRect)
        item.verticalNormalizedPosition = 1;
    }
}
