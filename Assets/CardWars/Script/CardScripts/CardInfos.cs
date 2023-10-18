using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using cardAnims;
using DigitalRuby.LightningBolt;

public class CardInfos : MonoBehaviour
{
    #region Variables
    public Card card;
    [Header("Card Objects")]
    [SerializeField] GameObject lightning;
    [SerializeField] GameObject cardFx;
    [SerializeField] TMPro.TMP_Text powerText;
    [SerializeField] UnityEngine.UI.Image cardTypeImage;
    [SerializeField] UnityEngine.UI.Image cardCharImage;
    //[SerializeField] TMPro.TMP_Text descText;
    //[SerializeField] TMPro.TMP_Text costText;
    //[SerializeField] TMPro.TMP_Text attackText;
    //[SerializeField] TMPro.TMP_Text healthText;
    [SerializeField] UnityEngine.UI.Image cardImage;
    [SerializeField] UnityEngine.UI.Image cardPattern;
    [SerializeField] bool revealed;
    [SerializeField] bool updateLightningStartPos=false;
    [SerializeField] GameObject highLight;
    //[SerializeField] UnityEngine.UI.Image cardImage;
    //[SerializeField] UnityEngine.UI.Image cardImage;
    //public bool inHand = false;
    public GameObject enemy;
    public GameObject owner;
    GameManager _gameManager;
    useCardScript _useCardScript;


    #endregion
    // Start is called before the first frame update
    void Awake()
    {
        
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _useCardScript = GameObject.FindObjectOfType<useCardScript>();
        Init(); 
    }
    /// <summary>
    /// Initializes Objects & References
    /// </summary>
    public void Init()
    {
        //cardImage = gameObject.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "cardFront").gameObject.GetComponent<UnityEngine.UI.Image>();
        ///*transform.Find("cardFront").GetComponent<UnityEngine.UI.Image>();*/
        //cardPattern = gameObject.transform.Find("cardBack").GetComponentsInChildren<Transform>(true).FirstOrDefault(t => t.name == "BG").gameObject.GetComponent<UnityEngine.UI.Image>();
        /*transform.Find("cardBack").GetComponent<UnityEngine.UI.Image>();*/
        //setInHand(false);
        //nameText.text = this.card.name;
        //descText.text = this.card.description;
        //costText.text = this.card.cost.ToString();
        //attackText.text = this.card.attack.ToString();
        //healthText.text = this.card.health.ToString();
        //cardImage.sprite = this.card.cardSprite;
        //cardPattern.sprite = this.card.pattern;
        //cardTypeImage.overrideSprite = card.typeIcon;
        //cardCharImage.overrideSprite = card.charSprite;
        //if (card.type.Equals(Card.Type.Attack))
        //{
        //    powerText.text = card.attack.ToString();
        //}
        //if (card.type.Equals(Card.Type.Deffense))
        //{
        //    powerText.text = card.health.ToString();
        //}
        if(card!=null)
            cardImage.overrideSprite = card.cardSprite;

        activateDeactivateHighLight(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (updateLightningStartPos)
            getLightningStartPosition();


    }
    /// <summary>
    /// Assing given paramter into inHand variable 
    /// </summary>
    /// <param name="_state"></param>
    //public void setInHand(bool _state)
    //{
    //    //Debug.Log(this.gameObject.name + " is in hand " + _state);
    //    inHand = _state;
    //}
    /// <summary>
    /// Returns inhGHand variable
    /// </summary>
    /// <returns></returns>
    //public bool getInHand()
    //{
    //    return inHand;
    //}
    /// <summary>
    /// Calls cards function according to its type
    /// </summary>
    public void UseCard()
    {
        //cardAnims.cardAnimations.setTRjfx(card.trj);
        //cardAnims.cardAnimations.setfx(card.fx);
        if (card.type.Equals(Card.Type.Attack))
        {
            StartCoroutine(attackRoutine());
        }
        if (card.type.Equals(Card.Type.Deffense))
        {
            StartCoroutine(healRoutine());
        }
        if (card.type.Equals(Card.Type.Special))
        {
            if (card.specialType.Equals(Card.SpacialType.Buff))
            {
                StartCoroutine(spacialRoutine(owner));
            }
            else if (card.specialType.Equals(Card.SpacialType.Debuff))
            {
                StartCoroutine(spacialRoutine(enemy));
            }

        }
    }
    /// <summary>
    /// Activates Deactivates cards fx
    /// </summary>
    /// <param name="_state"></param>
    public void activateDeactivateCardFx(bool _state)
    {
        cardFx.SetActive(_state);
    }
    /// <summary>
    /// Activates Deactivates cards HighLight
    /// </summary>
    /// <param name="_state"></param>
    public void activateDeactivateHighLight(bool _state)
    {
        if(_state)
            highLight.SetActive(_state);
        else if (!_state && highLight.activeInHierarchy)
            highLight.SetActive(_state);
    }
    public bool getRevealed()
    {
        return revealed;
    }
    public void setRevealed(bool _state)
    {
        revealed = _state;
    }
    public Card.Type getCardType()
    {
        return this.card.type;
    }

    public Card.SpacialType getSpecialType()
    {
        return this.card.specialType;
    }
    IEnumerator attackRoutine()
    {
        cardAnims.cardAnimations.attackTrjAnim(owner.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform.position, enemy.GetComponent<Player_Card>()._avatar.transform.localPosition, 1f,this.card.trjSFX, this.card.fxSFX);
        yield return new WaitForSeconds(1f);
        cardAnims.cardAnimations.shakeAttaackAnim(enemy.GetComponent<Player_Card>()._avatar.transform.transform,(card.attack/15), 1f);
        //yield return new WaitForSeconds(1f);
   

        List<GameObject> specList = enemy.GetComponent<Player_Card>().getSpacialList();
        bool callFunction = true;
        for (int i = 0; i < specList.Count; i++)
        {
            if (specList[i] != null)
            {
                if (specList[i].GetComponent<CardInfos>().card.Location.Equals(Card.callLocation.beforeTakeDamage))
                {
                    if (specList[i].GetComponent<CardInfos>().card.FunctionType.Equals(Card.FunctionCallType.Replace))
                        callFunction = false;

                    _gameManager.showAndUseSpecialCard(specList[i], enemy, true, _gameManager.getCurrentTurn());
                }
            }
        }

        if(callFunction)
            card.Attack(enemy);

        yield return new WaitForSeconds(1f);
        _useCardScript.delayedUseCard();
    }
    IEnumerator healRoutine()
    {
        cardAnims.cardAnimations.SpecialTrjAnim(owner.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform.position, owner.GetComponent<Player_Card>()._avatar.transform.localPosition, 0.5f, this.card.trjSFX, this.card.fxSFX);
        yield return new WaitForSeconds(0.5f);
        cardAnims.cardAnimations.scaleUpAnim(owner.GetComponent<Player_Card>()._avatar.transform, 1f);
        card.Heal(owner);
        yield return new WaitForSeconds(1f);
        _useCardScript.delayedUseCard();
    }
    IEnumerator spacialRoutine(GameObject target)
    {
        cardAnims.cardAnimations.SpecialTrjAnim(owner.GetComponent<Player_Card>().currentCardPanel.transform.GetChild(0).transform.position, target.GetComponent<Player_Card>()._avatar.transform.localPosition, 0.5f, this.card.trjSFX, this.card.fxSFX);
        yield return new WaitForSeconds(0.5f);
        cardAnims.cardAnimations.scaleUpAnim(target.GetComponent<Player_Card>()._avatar.transform, 1f);
        yield return new WaitForSeconds(0.5f);
        if (this.card.type.Equals(Card.Type.Special))
        {
            if (this.card.Location.Equals(Card.callLocation.CardSelected))
            {
                this.card.Initialize();
                //Debug.Log("Card Used");
                //_gameManager.showAndUseSpecialCard(this.gameObject, target, ()=>this.card.Special(target));
                //SoundManager.SInstance.playDialog(this.card.useSFX);
                this.card.Special(target);
            }
            else
            {
                //Debug.Log("Card Stored");
                target.GetComponent<Player_Card>().AddSpacial(this.gameObject);
            }
        }

        yield return new WaitForSeconds(0.5f);
        _useCardScript.delayedUseCard();
    }
    public void activateLightnig(Vector3 startPos, GameObject endPos)
    {
        //GameObject fx = Instantiate(lightning, lightning.transform.position, Quaternion.identity);
        //fx.transform.SetParent(lightning.transform.parent.transform);
        //fx.transform.localScale = Vector3.one;
        lightning.gameObject.SetActive(true);
        lightning.GetComponent<LightningBoltScript>().Init();
        //lightning.GetComponent<LightningBoltScript>().StartPosition = lightning.transform.position;
        lightning.GetComponent<LightningBoltScript>().EndObject = endPos;
        
        //updateLightningStartPos = true;
        lightning.GetComponent<LightningBoltScript>().Trigger();
        //InvokeRepeating("getLightningStartPosition",0,0.001f);
    }
    public void getLightningStartPosition()
    {
        lightning.GetComponent<LightningBoltScript>().StartPosition = lightning.transform.position;
    }
}

