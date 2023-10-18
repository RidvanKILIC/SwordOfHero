using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public class setMarketItemsInfoTextsForString : MonoBehaviour
{
    //[SerializeField] TMP_Text _Labeltext;
    [SerializeField] TMP_Text _text;
    [SerializeField] LocalizedString _localizeLabelString;
    [SerializeField] LocalizeStringEvent _localizetextString;
    string itemName="test";
    // Start is called before the first frame update
    void Start()
    {
        _localizetextString = _text.GetComponent<LocalizeStringEvent>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //public void setString(string _name)
    //{
    //    itemName = _name;
    //}
    //public void OnEnable()
    //{
    //    _localizeLabelString.Arguments = new object[] { itemName };

    //}
    //public void OnDisable()
    //{
    //    _localizeLabelString.StringChanged += UpdateText;
    //}
    //public void UpdateText(string value)
    //{
    //    _Labeltext.text = value;
    //}
    public void setText(string tableReference,string _key)
    {
        //setString(value);
        //Debug.Log(tableReference+","+_key);
        //_localizeLabelString.Arguments[0] = itemName;
        //_localizeLabelString.RefreshString();
        _localizetextString.StringReference.SetReference(tableReference, _key);
        _localizetextString.RefreshString();
    }
}
