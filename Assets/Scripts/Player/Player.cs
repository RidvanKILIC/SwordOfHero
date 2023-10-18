using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RK.GameDatas;
public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] float health;
    [SerializeField] float damage;
    [SerializeField] float spacial1Damage;
    [SerializeField] float shield;
    [SerializeField] public bool spacialUsing;
    [SerializeField] GameObject attackFX;
    MiniGame_Manager _gameManager;
    public bool isDead=false;
    GameDatas.playerSaveInfos _playerInfos = new GameDatas.playerSaveInfos();
    [SerializeField] inGameHealthAndShieldBar _healthBar;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] AudioClip hitSFX;
    [SerializeField] AudioClip deathFX;
    [SerializeField] AudioClip winSFX;
    [SerializeField] PlayerAnimationController _playerAnim;
    [SerializeField] PlayerSkinController _playerSkins;
    public float _Health { get; set; }
    public float _Shield { get; set; }

    // Start is called before the first frame update
    void Start()
    {
       

    }
    public void Init()
    {
        _gameManager = GameObject.FindGameObjectWithTag("Managements").gameObject.GetComponent<MiniGame_Manager>();
         health = SaveLoad_Manager.gameData.playerInfos.playerBaseHealth;
         shield = SaveLoad_Manager.gameData.playerInfos.playerBaseShield;
         damage = SaveLoad_Manager.gameData.playerInfos.playerBaseAttack;
        _Health = health;
        _Shield = shield;
        //Debug.Log("Player Health:" + health);
        //Debug.Log("Player Shield:" + shield);
        _healthBar.setHelathSlidersMax(health);
        _healthBar.setShieldSlidersMax(shield);
        _playerAnim.playBodyAnim(_playerSkins.playerParts, "Defaults/Default_Default_Idle",true);
        //health = _playerInfos.playerCurrentHealth;
        //shield = _playerInfos.playerCurrentShield;
        //damage = _playerInfos.playerCurrentAttack;
        
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void IDamage(float _damageAmount)
    {
        if (!isDead)
        {
            SoundManager.SInstance.playSfx(hitSFX);
            if(!spacialUsing)
                _playerAnim.playBodyAnim(_playerSkins.playerParts, "Defaults/Default_Default_Hit",false,2f);

            if (_Shield > 0)
            {
                _Shield -= _damageAmount;
                if (_Shield < 0)
                    _Shield = 0;

                _healthBar.setShieldSlidersValue(_Shield);
            }
            else
            {
                _Health -= _damageAmount;
                if (_Health <= 0)
                {
                    _Health = 0;
                    _healthBar.setHelathSlidersValue(_Health);
                    IDead();
                }
                else
                {
                    _healthBar.setHelathSlidersValue(_Health);
                }
            }
        }
        //throw new System.NotImplementedException();
    }
    public void IAttack()
    {
        if (!_gameManager.isGameOver &&!_gameManager.currentEnemy.GetComponent<EnemyContainer>().isDead && _gameManager.currentEnemy.GetComponent<EnemyContainer>().enterdArena && !isDead)
        {
            if (!string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
            {
                Weapon chooseWeapon = ObjectsLists.swords.Find(x => x._name == SaveLoad_Manager.gameData.playerInfos.equippedSword);
                if (chooseWeapon != null)
                {
                    if (!string.IsNullOrEmpty(chooseWeapon._name))
                    {
                        float speed = 1f;
                        if (spacialUsing)
                            speed = 2f;
                        //Debug.Log("Player Attack");
                        _playerAnim.playAttackAnim(_playerSkins.playerParts, _playerSkins.getWeapon(), /*"Weapon/" + */chooseWeapon._weaponType.ToString() + "/" + "Right_" + chooseWeapon._name + "_Attack",speed);

                    }
                }
            }
            var hit = _gameManager.currentEnemy.gameObject.GetComponent<IDamagable>();
            if (hit != null)
            {
                hit.IDamage(damage);
                SoundManager.SInstance.playSfx(attackSFX);
                //Vector3 pos = new Vector3(_gameManager.currentEnemy.gameObject.transform.position.x + (Random.Range(-0.5f, 0.5f)), _gameManager.currentEnemy.gameObject.transform.position.y + (Random.Range(-0.5f, 0.5f)), _gameManager.currentEnemy.gameObject.transform.position.z);
                //GameObject fx = Instantiate(attackFX, pos, Quaternion.identity);
                //fx.transform.SetParent(_gameManager.currentEnemy.gameObject.transform.parent.transform);
                //fx.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                //Destroy(fx, 1f);
            }
               
            //Invoke("changeTurnDelayed", 1f);
        }

    }
    public void spacial1Attack()
    {
        if (!_gameManager.isGameOver && !_gameManager.currentEnemy.GetComponent<EnemyContainer>().isDead && _gameManager.currentEnemy.GetComponent<EnemyContainer>().enterdArena)
        {
            var hit = _gameManager.currentEnemy.gameObject.GetComponent<IDamagable>();
            if (hit != null)
            {
                if (!SaveLoad_Manager.gameData.isClashedOnce)
                {
                    tutorialController.TInstance.deactiveMiniGameArrow();
                }
                spacialUsing = true;
                _gameManager.spacialUsed();
                _gameManager.spacial1(1f,true);
                Invoke("spacialFinished", 4f);
            }
        } 
    }
    public void spacialFinished()
    {
        //Debug.Log("SpacialUsed");
        _gameManager.spacial1(1f, false);
        spacialUsing = false;
    }
    //public void changeTurnDelayed()
    //{
    //    _gameManager.changeTurn();
    //}
    public void IDead()
    {
        SoundManager.SInstance.playSfx(deathFX);
        _playerAnim.playBodyAnim(_playerSkins.playerParts, "Defaults/Default_Default_Death",false);
        isDead = true;
        //Debug.Log("Player Dead");
        _gameManager.isGameOver = true;
        _gameManager.gameOver();
    }
    public void playerWin()
    {
        _playerAnim.playBodyAnim(_playerSkins.playerParts, "Defaults/Default_Default_Win", false, 1f);
    }

    public void IHeal(float _healAmount)
    {
        throw new System.NotImplementedException();
    }
}
