using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class setMarketItemsInfoTextsForInt : MonoBehaviour
{
    [SerializeField] TMP_Text _Labeltext;
    [SerializeField] TMP_Text _text;
    [SerializeField] LocalizedString _localizeLabelString;

    int itemAtackOrDefense=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setValue(int value)
    {
        itemAtackOrDefense = value;
    }
    public void OnEnable()
    {
        _localizeLabelString.Arguments = new object[] { itemAtackOrDefense };

    }
    public void OnDisable()
    {
        _localizeLabelString.StringChanged += UpdateText;
    }
    public void UpdateText(string value)
    {
        _Labeltext.text = value;
    }
    public void setText(int _value)
    {
        setValue(_value);
        //Debug.Log(itemAtackOrDefense);
        _localizeLabelString.Arguments[0] = itemAtackOrDefense;
        _localizeLabelString.RefreshString();
        _text.text = _value.ToString();
    }
}
