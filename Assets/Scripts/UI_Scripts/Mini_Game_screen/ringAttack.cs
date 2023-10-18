using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DigitalRuby.LightningBolt;

public class ringAttack : MonoBehaviour
{
    [SerializeField] float Attack;
    [SerializeField] Transform TopPos;
    [SerializeField] Transform BottomPos;
    [SerializeField] float chargeAttack;
    [SerializeField] float maxRingCharge;
    [SerializeField] float currentRingCharge;
    [SerializeField] Slider ringSlider;
    [SerializeField] GameObject attackFx;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip chargedAttackSFX;
    [SerializeField] GameObject chargedAttackFx;
    [SerializeField] GameObject highLight;
    bool ringCharged;
    MiniGame_Manager _gameManager;
    bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("Managements").gameObject.GetComponent<MiniGame_Manager>();
        setSlidersMaxValue(maxRingCharge);
        ringCharged = false;
        currentRingCharge = maxRingCharge;
    }
    // Update is called once per frame
    void Update()
    {
        if (_gameManager.currentEnemy != null && IsPointerOverUIObject() && !_gameManager.currentPlayer.GetComponent<Player>().spacialUsing)
        {
            if (canAttack && !_gameManager.isGameOver && !_gameManager.currentEnemy.GetComponent<EnemyContainer>().isDead && _gameManager.currentEnemy.GetComponent<EnemyContainer>().enterdArena)
            {
                if (Input.GetMouseButton(0))
                {
                    if (ringCharged)
                    {
                        canAttack = false;
                        spacialAttack();
                    }
                    else
                    {
                        canAttack = false;
                        regularAttack();
                    }

                }

            }
        }

    }
    public bool IsPointerOverUIObject()
    {
        bool result = false;
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult r in results)
        {
            //if (r.gameObject.layer.Equals(cardLayer))
            //{
            if (r.gameObject.GetComponent<RectTransform>() != null)
            {
                //Debug.Log(r.gameObject.transform.parent.gameObject.name);
                if (r.gameObject.name.Contains("(ignore)"))
                {
                    result = false;
                    break;
                }
                else
                {
                    result=true;
                }

            }
            //}

        }


        return result;
    }
    public void setSlidersMaxValue(float value)
    {
        ringSlider.maxValue = value;
        setSlidersValue(value);
    }
    public void setSlidersValue(float value)
    {
        currentRingCharge = value;
        ringSlider.value = currentRingCharge;
        //Debug.Log(currentRingCharge);
        if(currentRingCharge<=0)
        {
            //ringSlider.value = currentRingCharge;
            highLight.SetActive(true);
            //LeanTween.scale(ringSlider.transform.parent.gameObject, new Vector3(1.1f, 1.1f, 1.1f), 1f).setEase(LeanTweenType.easeInSine);
            ringCharged = true;
        }
            
    }
    public async void regularAttack()
    {
            var hit = _gameManager.currentEnemy.gameObject.GetComponent<IDamagable>();
            if (hit != null)
            {
                currentRingCharge -= 3;
                //if (currentRingCharge < 0)
                //    currentRingCharge = 0;
                setSlidersValue(currentRingCharge);
                hit.IDamage(Attack);
                Vector3 pos = new Vector3(_gameManager.currentEnemy.gameObject.transform.position.x + (Random.Range(-0.2f, 0.2f)), _gameManager.currentEnemy.gameObject.transform.position.y + (Random.Range(-0.2f, 0.2f)), _gameManager.currentEnemy.gameObject.transform.position.z);
                Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spawnPos.z = 100f;
            //GameObject fx = Instantiate(attackFx, spawnPos, Quaternion.identity);
            //attackFx.GetComponent<LightningBoltScript>().Init();
            //attackFx.GetComponent<LightningBoltScript>().StartPosition = spawnPos;
            //attackFx.GetComponent<LightningBoltScript>().EndPosition = pos;
            //attackFx.transform.SetParent(_gameManager.currentEnemy.gameObject.transform.parent.transform);
            //attackFx.transform.localScale = Vector3.one;
            //attackFx.SetActive(true);
            //attackFx.GetComponent<LightningBoltScript>().Trigger();
                SoundManager.SInstance.playSfx(attackSFX);
                //Destroy(fx, 1f);
                StartCoroutine(delay(1));
            }
    }
    public async void spacialAttack()
    {
            //Debug.Log("Spacial ring");
            var hit = _gameManager.currentEnemy.gameObject.GetComponent<IDamagable>();
            if (hit != null)
            {
                //Debug.Log("Max Charge:" + maxRingCharge);
                currentRingCharge = maxRingCharge;
                //LeanTween.scale(ringSlider.transform.parent.gameObject, Vector3.one, 1f).setEase(LeanTweenType.easeOutSine);
                highLight.SetActive(false);
                ringCharged = false;
                setSlidersValue(currentRingCharge);
                hit.IDamage(chargeAttack);
            //Vector3 pos = new Vector3(_gameManager.currentEnemy.gameObject.transform.position.x + (Random.Range(-0.5f, 0.5f)), _gameManager.currentEnemy.gameObject.transform.position.y + (Random.Range(-0.5f, 0.5f)), _gameManager.currentEnemy.gameObject.transform.position.z);
            //GameObject fx = Instantiate(attackFx, pos, Quaternion.identity);
            //fx.transform.SetParent(_gameManager.currentEnemy.gameObject.transform.parent.transform);
            //fx.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //Destroy(fx, 1f);
            //Vector3 pos = new Vector3(_gameManager.currentEnemy.gameObject.transform.position.x + (Random.Range(-0.2f, 0.2f)), _gameManager.currentEnemy.gameObject.transform.position.y + (Random.Range(-0.2f, 0.2f)), _gameManager.currentEnemy.gameObject.transform.position.z);
            Vector3 spawnPos = TopPos.position;
            spawnPos.z = 100f;
            spawnPos.x = _gameManager.enemyArenaPos.transform.position.x + (Random.Range(-0.2f, 0.2f));
            Vector3 _topPos = TopPos.position;
            _topPos.x=spawnPos.x;
            Vector3 _bottomPos = BottomPos.position;
            _bottomPos.x= spawnPos.x;
            //GameObject fx = Instantiate(chargedAttackFx, spawnPos, Quaternion.identity);
            chargedAttackFx.GetComponent<LightningBoltScript>().Init();
            chargedAttackFx.GetComponent<LightningBoltScript>().StartPosition = _topPos;
            chargedAttackFx.GetComponent<LightningBoltScript>().EndPosition =_bottomPos;
            chargedAttackFx.transform.SetParent(_gameManager.currentEnemy.gameObject.transform.parent.transform);
            chargedAttackFx.transform.localScale = Vector3.one;
            chargedAttackFx.SetActive(true);
            chargedAttackFx.GetComponent<LightningBoltScript>().Trigger();
            SoundManager.SInstance.playSfx(chargedAttackSFX);
            //Destroy(fx, 4f);
            StartCoroutine(delay(2));
            }    
    }
    IEnumerator delay(int delay)
    {
        canAttack = false;
        yield return new WaitForSeconds(delay);
        if (chargedAttackFx.activeInHierarchy)
            chargedAttackFx.SetActive(false);
        if (attackFx.activeInHierarchy)
            attackFx.SetActive(false);
        canAttack = true;
    }
}
