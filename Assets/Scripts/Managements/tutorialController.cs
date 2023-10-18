using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class tutorialController : MonoBehaviour
{
    [SerializeField] GameObject tutarialPrefab;
    [SerializeField] GameObject miniGameTutarialPrefab;
    [SerializeField] GameObject tutarialArrow;
    GameObject Arrow;
    GameObject tPanel;
    Animator tutorialTextAnimator;
    string textOpenAnim = "tutotialTextOpen";
    string textCloseAnim = "tutotialTextClose";
    private static tutorialController instance;
    public static tutorialController TInstance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Tutorial Manager's instance is null");
            }
            return instance;
        }
    }
    //public delegate void callBackDelegate();
    //callBackDelegate newDelegate;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #region Registration Tutorial
    public void registerPanelTutorial(GameObject _callback)
    {
        //Debug.Log(_callback.name);
        GameObject panel = Instantiate(tutarialPrefab);
        panel.transform.GetChild(0).gameObject.SetActive(false);
        panel.transform.SetParent(_callback.GetComponent<Selection_MenuController>().canvas.transform);
        panel.transform.position = _callback.GetComponent<Selection_MenuController>().canvas.transform.position;
        panel.transform.localScale=Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        StartCoroutine(registrationTutorial(panel, _callback));
    }
    IEnumerator registrationTutorial(GameObject tutorialPanel, GameObject _callback)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "register_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);
        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "register_Text_Line_2");
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);

        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "register_Text_Line_3");
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(2);
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tutorialPanel,1);
        yield return new WaitForSeconds(1);
        if (_callback != null)
            _callback.GetComponent<Selection_MenuController>().openRegisterPanel();

    }
    #endregion
    #region Main Maenu Armory Tutorial
    public void mainMenuArmoryTutorial(GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform);
        tPanel.transform.position = _callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        GameObject arrow = Instantiate(tutarialArrow);
        Arrow = arrow;
        Arrow.SetActive(false);
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        StartCoroutine(mainMenuArmoryTutorialRoutine(tPanel, _callback,Arrow));
       

    }
    IEnumerator mainMenuArmoryTutorialRoutine(GameObject tutorialPanel, GameObject _callback, GameObject _Arrow)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_Armory_Text_LÝne_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);
        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_Armory_Text_LÝne_2");
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().weaponaryArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().weaponaryArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        Arrow.SetActive(true);
        _callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Weaponary").GetComponent<Toggle>().interactable = true;
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tPanel.gameObject, 1);
    }
    public void endArmoryTutorial()
    {
        tPanel.GetComponentInChildren<Animator>().Play("tutorialClose");
        Destroy(tPanel,1f);
        Arrow.gameObject.SetActive(false);
    }
    public void activateShieldArrow(GameObject _callback)
    {
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().shieldArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().shieldArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        Arrow.SetActive(true);
        _callback.GetComponent<mainMenu_Controller>().weaponaryToggles.Find(x => x.name == "Shields").GetComponent<Toggle>().interactable = true;
    }
    public void deActivaeShieldArrow()
    {
        Arrow.SetActive(false);
    }
    public void activateArmorArrow(GameObject _callback)
    {
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().armorArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().armorArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        _callback.GetComponent<mainMenu_Controller>().weaponaryToggles.Find(x => x.name == "Armors").GetComponent<Toggle>().interactable = true;
        Arrow.SetActive(true);
    }
    public void deActivateArmorArrow()
    {
        Arrow.SetActive(false);
    }
    public void ActivateHomeArrow(GameObject _callback)
    {
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().HomeArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().HomeArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        _callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Home").GetComponent<Toggle>().interactable = true;
        Arrow.SetActive(true);
    }
    public void deActivateHomeArrow()
    {
        Destroy(Arrow.gameObject);
    }
    #endregion
    #region First Clash Tutorial
    public void mainMenuFirstClashTutorial(GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform);
        tPanel.transform.position = _callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        GameObject arrow = Instantiate(tutarialArrow);
        Arrow = arrow;
        Arrow.SetActive(false);
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        StartCoroutine(mainMenuFirstClashTutorialRoutine(tPanel, _callback, Arrow));


    }
    IEnumerator mainMenuFirstClashTutorialRoutine(GameObject tutorialPanel, GameObject _callback, GameObject _Arrow)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_FirstClash_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().mapArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().mapArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        Arrow.SetActive(true);
        _callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Map").GetComponent<Toggle>().interactable = true;
    }
    public void endMainMenuClashTutorial()
    {
        Destroy(tPanel.gameObject);
        Destroy(Arrow.gameObject);
    }
    #endregion
    #region Housing Tutorial
    public void mainMenuHousingTutorial(GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform);
        tPanel.transform.position = _callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        GameObject arrow = Instantiate(tutarialArrow);
        Arrow = arrow;
        Arrow.SetActive(false);
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        StartCoroutine(mainMenuHousingTutorialRoutine(tPanel, _callback, Arrow));


    }
    IEnumerator mainMenuHousingTutorialRoutine(GameObject tutorialPanel, GameObject _callback, GameObject _Arrow)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_Housing_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);
        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_Housing_Text_Line_2");
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().housingArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().housingArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        Arrow.SetActive(true);
        _callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Housing").GetComponent<Toggle>().interactable = true;
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tPanel.gameObject, 1);
    }
    public void activateHousingHomeArrow(GameObject _callback)
    {
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().housingHomeArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().housingHomeArrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        _callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Home").GetComponent<Toggle>().interactable = true;
        Arrow.SetActive(true);
    }
    public void endmainMenuHousingTutorial(GameObject _callback)
    {
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = true;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = true;
        }
        Destroy(Arrow.gameObject);
    }
    #endregion
    #region cardGame Button Tutorial
    public void mainMenuActivateCardTutorial(GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform);
        tPanel.transform.position = _callback.GetComponent<mainMenu_Controller>().Canvas.transform.GetChild(0).transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        GameObject arrow = Instantiate(tutarialArrow);
        Arrow = arrow;
        Arrow.SetActive(false);
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = false;
        }
        StartCoroutine(mainMenuActivateCardTutorialRoutine(tPanel, _callback, Arrow));
    }
    IEnumerator mainMenuActivateCardTutorialRoutine(GameObject tutorialPanel, GameObject _callback, GameObject _Arrow)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "mainMenu_cardGameActivation_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        Arrow.transform.SetParent(_callback.GetComponent<mainMenu_Controller>().tavernArrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<mainMenu_Controller>().tavernArrowPos.transform.position;
        Arrow.transform.rotation = _callback.GetComponent<mainMenu_Controller>().tavernArrowPos.transform.rotation;
        Arrow.transform.localScale = Vector3.one;
        _callback.GetComponent<mainMenu_Controller>().openTavern();
        yield return new WaitForSeconds(0.5f);
        Arrow.SetActive(true);
       
        //_callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Map").GetComponent<Toggle>().interactable = true;
    }
    public void endMainMenuActivateCardTutorialTutorial(GameObject _callback)
    {
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().menuToggles)
        {
            item.GetComponent<Toggle>().interactable = true;
        }
        foreach (var item in _callback.GetComponent<mainMenu_Controller>().weaponaryToggles)
        {
            item.GetComponent<Toggle>().interactable = true;
        }
        Destroy(tPanel.gameObject);
        Destroy(Arrow.gameObject);
    }
    #endregion
    #region CarGame Tutorial

    public void cardGameStartTutorial(GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<GameManager>().Canvas.transform);
        tPanel.transform.position = _callback.GetComponent<GameManager>().Canvas.transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        StartCoroutine(cardGameStartTutorialRoutine(tPanel, _callback));
    }
    IEnumerator cardGameStartTutorialRoutine(GameObject tutorialPanel, GameObject _callback)
    {
        yield return new WaitForSeconds(0.5f);

        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_Start_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);
        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_Start_Line_2");
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialTextAnimator.Play(textCloseAnim);
        yield return new WaitForSeconds(1);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_Start_Line_3");
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tutorialPanel.gameObject, 2);
        yield return new WaitForSeconds(2);
        _callback.GetComponent<DeckToPick>().dealCards();

        //_callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Map").GetComponent<Toggle>().interactable = true;
    }
    public void cardGameTutorial(int _textLine,bool result,GameObject _callback)
    {
        GameObject panel = Instantiate(tutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<GameManager>().Canvas.transform);
        tPanel.transform.position = _callback.GetComponent<GameManager>().Canvas.transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        StartCoroutine(cardGameTutorialRoutine(tPanel, _callback, _textLine,result));
    }
    IEnumerator cardGameTutorialRoutine(GameObject tutorialPanel, GameObject _callback, int _textLine,bool result)
    {
        yield return new WaitForSeconds(0.5f);
        if (_textLine == 1)
        {
            tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_Line_1");
        }
        else if (_textLine == 2)
        {
            if(result)
                tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_Success__Line_2");
            else
                tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "cardGame_Tutorial_Text_unSuccess__Line_2");
        }
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(2);
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tutorialPanel.gameObject, 1);
        yield return new WaitForSeconds(1);
        _callback.GetComponent<GameManager>().keepGoing();

        //_callback.GetComponent<mainMenu_Controller>().menuToggles.Find(x => x.name == "Map").GetComponent<Toggle>().interactable = true;
    }
    #endregion
    #region Mini Game Tutorial
    public void miniGameTutorial(GameObject _callback, bool isringPowerTutorial)
    {
        GameObject panel = Instantiate(miniGameTutarialPrefab);
        tPanel = panel;
        tPanel.transform.GetChild(0).gameObject.SetActive(false);
        tPanel.transform.SetParent(_callback.GetComponent<MiniGame_Manager>().Canvas.transform.GetChild(0).transform);
        tPanel.transform.position = _callback.GetComponent<MiniGame_Manager>().Canvas.transform.GetChild(0).transform.position;
        tPanel.transform.localScale = Vector3.one;
        tutorialTextAnimator = panel.transform.Find("Container/textBG/Text").GetComponent<Animator>();
        if (isringPowerTutorial)
        {
            StartCoroutine(ringPowerTutorialRoutine(panel, _callback));
        }
        else
        {
            GameObject arrow = Instantiate(tutarialArrow);
            Arrow = arrow;
            Arrow.SetActive(false);
            StartCoroutine(spacialPoweRoutine(tPanel, _callback));
        }


       
    }
    IEnumerator ringPowerTutorialRoutine(GameObject tutorialPanel, GameObject _callback)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "miniGame_Tutorial_Ring_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(3);
        tutorialPanel.GetComponentInChildren<Animator>().SetTrigger("tutorialClose");
        Destroy(tPanel.gameObject,1);
    }
    IEnumerator spacialPoweRoutine(GameObject tutorialPanel, GameObject _callback)
    {
        yield return new WaitForSeconds(0.5f);
        tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "miniGame_Tutorial_Spacial_Text_Line_1");
        tutorialPanel.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        tutorialTextAnimator.Play(textOpenAnim);
        Arrow.transform.SetParent(_callback.GetComponent<MiniGame_Manager>().arrowPos.transform);
        Arrow.transform.position = _callback.GetComponent<MiniGame_Manager>().arrowPos.transform.position;
        Arrow.transform.localScale = Vector3.one;
        Arrow.SetActive(true);
        //yield return new WaitForSeconds(3);
        //tutorialTextAnimator.Play(textCloseAnim);
        //yield return new WaitForSeconds(1);
        //tutorialPanel.GetComponent<setMarketItemsInfoTextsForString>().setText("tutorial_Texts", "miniGame_Tutorial_Spacial_Text_Line_2");
        //tutorialTextAnimator.Play(textOpenAnim);
        yield return new WaitForSeconds(1);
        if(_callback!=null)
            _callback.GetComponent<MiniGame_Manager>().playerSpacial_1.GetComponent<playerSpacial_Controller>().keepGoing();
        //tutorialPanel.GetComponentInChildren<Animator>().Play("tutorialClose");
        
    }
    public void deactiveMiniGameArrow()
    {
        StopCoroutine(spacialPoweRoutine(null,null));
        Destroy(tPanel.gameObject, 1);
        Destroy(Arrow.gameObject);
    }
    #endregion

}
