using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class warningPanelText : MonoBehaviour
{
    [SerializeField] LocalizeStringEvent _localizeBodyString;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setText(string tableReference, string _key)
    {
        //setString(value);
        //Debug.Log(tableReference+","+_key);
        //_localizeLabelString.Arguments[0] = itemName;
        //_localizeLabelString.RefreshString();
        _localizeBodyString.StringReference.SetReference(tableReference, _key);
        _localizeBodyString.RefreshString();
    }
}
