using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyContainer : MonoBehaviour,IDamagable
{
    [SerializeField] public Enemy _enemyScriptable;
    [SerializeField] inGameHealthAndShieldBar _healthBar;
    [SerializeField]float health;
    [SerializeField]float shield;
    [SerializeField]public bool isDead;
    [SerializeField] public bool enterdArena = false;
    MiniGame_Manager _gameManager;
    [SerializeField] GameObject attackFX;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] enemyAnimationController _enemyAnim;
    public float _Health { get; set; }
    public float _Shield { get; set; }
    // Start is called before the first frame update
    void Start()
    {

    }
    public void init()
    {
        isDead = _enemyScriptable.isDead;
        _gameManager = GameObject.FindGameObjectWithTag("Managements").GetComponent<MiniGame_Manager>();
        health = _enemyScriptable.health;
        shield = _enemyScriptable.shield;
        _Health = health;
        _Shield = shield;
        enterdArena = false;
        //Debug.Log("Enemy Health:" + health);
        //Debug.Log("Enemy Shield:" + shield);
        _healthBar.setHelathSlidersMax(health);
        _healthBar.setShieldSlidersMax(shield);
    }
    // Update is called once per frame
    void Update()
    {
        if (enterdArena)
            this.gameObject.transform.position = _gameManager.enemyArenaPos.transform.position;
    }
    public void IAttack()
    {
        if (!_gameManager.isGameOver&& !isDead && !_gameManager.currentPlayer.GetComponent<Player>().isDead)
        {
            _enemyAnim.PlayAttacAnim();
            SoundManager.SInstance.playSfx(_enemyScriptable.attackSFX);
            _enemyScriptable.Attack(this._enemyScriptable.attack);
            //Vector3 pos = new Vector3(_gameManager.currentPlayer.gameObject.transform.position.x + (Random.Range(-0.5f, 0.5f)), _gameManager.currentPlayer.gameObject.transform.position.y + (Random.Range(-0.5f, 0.5f)), _gameManager.currentEnemy.gameObject.transform.position.z);
            //GameObject fx = Instantiate(attackFX, pos, Quaternion.identity);
            //fx.transform.SetParent(_gameManager.currentPlayer.gameObject.transform.parent.transform);
            //fx.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //Destroy(fx, 1f);
            //Invoke("changeTurnDelayed", 1f);
        }

    }
    //public void changeTurnDelayed()
    //{
    //    _gameManager.changeTurn();
    //}
    public void IDamage(float _damageAmount)
    {
        if (!isDead)
        {

            SoundManager.SInstance.playSfx(_enemyScriptable.hitSFX);
            _enemyAnim.PlayBodyAnim("Defaults/Default_Default_Hit",false,2f);
            if (_Shield > 0)
            {
                _Shield -= _damageAmount;
                if (_Shield <= 0)
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
    }
    public void EnterArena(Transform pos)
    {
        _enemyAnim.PlayBodyAnim("Defaults/Default_Default_Walkforward",true);
        _enemyScriptable.enterArena(this.gameObject, pos);
        Invoke("setEnterdArenaTrue", _enemyScriptable.arenaEnterDureation);
    }
    public void setEnterdArenaTrue()
    {
        _enemyAnim.PlayBodyAnim("Defaults/Default_Default_Idle",true);
        enterdArena = true;
    }
    public void IDead()
    {
        isDead = true;
        _healthBar.gameObject.SetActive(false);
        //Debug.Log("Enemy Dead");
        setDeath();
        //Invoke("setDeath", 1f);
        
    }
    public void setDeath()
    {
        _enemyAnim.PlayBodyAnim("Defaults/Default_Default_Death", false);
        _gameManager.setEnemyDeath(this.gameObject.name);
    }
    public void enemyWin()
    {
        SoundManager.SInstance.playSfx(_enemyScriptable.deathSFX);
        _enemyAnim.PlayBodyAnim("Defaults/Default_Default_Win", false);
    }

    public void IHeal(float _healAmount)
    {
        throw new System.NotImplementedException();
    }
}
