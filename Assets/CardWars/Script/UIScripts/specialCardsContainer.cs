using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class specialCardsContainer : MonoBehaviour
{
    [SerializeField] public List<GameObject> posList = new List<GameObject>();
    [SerializeField] GameObject demonstrationPanelOfSpecials;
    [SerializeField] public Animator _animator;
    bool isPanelActive = false;
    public bool cardShoowing = false;
    public List<GameObject> currentSpecials = new List<GameObject>();
    //public currentSpacial currentCard=new currentSpacial();
    // Start is called before the first frame update
    void Start()
    {
        currentSpecials.Clear();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void addCardToSlot(GameObject card,int index,bool isFirst)
    {
        Debug.Log(card.name + " added");
        //StartCoroutine(addCardRoutine(/*posList.ElementAt(index).gameObject.transform.GetChild(0).gameObject*/card,isFirst,index));
        card.transform.SetParent(posList.ElementAt(index).transform);
        card.transform.localScale = Vector3.one;
        //if (isFirst)
        //{
        //    _animator.Play("specialCardsPanelOpen");
        //    //yield return new WaitForSeconds(1);
        //}
        cardAnims.cardAnimations.changeCardSizeAnim(card.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), posList.ElementAt(index).transform.GetComponent<RectTransform>().sizeDelta, 1f);
        cardAnims.cardAnimations.cardRotateAnim(card.transform, 1f, new Vector3(80f, 0f, 0f));
        cardAnims.cardAnimations.cardkMoveReturnPosAnim(card.transform, 1f, card.GetComponent<CardInfos>().card.spacialdemonstrationSFX);
    }
    public void demonstrateCard(GameObject card,int index,bool callFunction,GameObject target)
    {
        card.transform.SetParent(demonstrationPanelOfSpecials.transform.GetChild(0).transform);
        cardAnims.cardAnimations.changeCardSizeAnim(card.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), demonstrationPanelOfSpecials.transform.GetChild(0).transform.GetComponent<RectTransform>().sizeDelta, 0.5f);
        cardAnims.cardAnimations.cardRotateAnim(card.transform, 0.5f, new Vector3(0f, 0f, 0f));
        cardAnims.cardAnimations.cardkMoveReturnPosAnim(card.transform, 0.5f, card.GetComponent<CardInfos>().card.trjSFX);
        cardShoowing = true;
        currentSpecials.Add(card);
        if (!callFunction)
        {
            //SoundManager.SInstance.playDialog(card.GetComponent<CardInfos>().card.useSFX);
        }
        if (callFunction)
        {

            //card.GetComponent<CardInfos>().card.Special(target);
            //Debug.Log("Spacial Calling..");
            StartCoroutine(delayedCall(card,target));
        }
           
    }
    IEnumerator delayedCall(GameObject card,GameObject target)
    {
        yield return new WaitForSeconds(1);
        //soundManager.SInstance.playDialog(card.GetComponent<CardInfos>().card.useSFX);
        card.GetComponent<CardInfos>().card.Special(target);
    }
    public void retunCardToSlot(GameObject card, int index)
    {
        card.transform.SetParent(posList.ElementAt(index).transform);
        cardAnims.cardAnimations.changeCardSizeAnim(card.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), posList.ElementAt(index).transform.GetComponent<RectTransform>().sizeDelta, 1f);
        cardAnims.cardAnimations.cardRotateAnim(card.transform, 1f, new Vector3(80f, 0f, 0f));
        cardAnims.cardAnimations.cardkMoveReturnPosAnim(card.transform, 1f, card.GetComponent<CardInfos>().card.spacialReturnSFX);
        cardShoowing = false;
        currentSpecials.Remove(card);
    }
    public void destroyCard(GameObject card,bool isLast)
    {
        cardAnims.cardAnimations.fadeAndDestroy(card.transform.GetChild(1).GetComponent<UnityEngine.UI.Image>(), 1f, card.GetComponent<CardInfos>().card.destroySFX);
        if (isLast)
        {

            if (isPanelActive)
            {
                setIsPanelActive(false);
                _animator.Play("specialCardsPanelClose");
            }

        }
           
        cardShoowing = false;
        currentSpecials.Remove(card);
    }
    public void setIsPanelActive(bool _state)
    {
        isPanelActive = _state;
    }
    public bool getIsPanelActive()
    {
        return isPanelActive;
    }
    IEnumerator addCardRoutine(GameObject card,bool isFirst,int index)
    {
        card.transform.SetParent(posList.ElementAt(index).transform);
        if (isFirst)
        {
            _animator.Play("specialCardsPanelOpen");
            yield return new WaitForSeconds(1);
        }
        cardAnims.cardAnimations.changeCardSizeAnim(card.transform.GetChild(1).gameObject.GetComponent<RectTransform>(), posList.ElementAt(index).transform.GetComponent<RectTransform>().sizeDelta, 1f);
        cardAnims.cardAnimations.cardRotateAnim(card.transform, 1f, new Vector3(80f, 0f, 0f));
        cardAnims.cardAnimations.cardkMoveReturnPosAnim(card.transform, 1f, card.GetComponent<CardInfos>().card.spacialReturnSFX);
        yield return null;
    }
    [System.Serializable]
    public struct currentSpacial
    {
        public GameObject _card;
        public int index;
        public bool isNull;
    }
}
