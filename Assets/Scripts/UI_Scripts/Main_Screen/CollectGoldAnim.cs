using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CollectGoldAnim : MonoBehaviour
{
    [SerializeField] GameObject goldPrefab;
    [SerializeField] GameObject goldContainer;
    [SerializeField] mainMenu_Controller _gameManager;
    [SerializeField] WeaponaryMarketController weaponaryMarket;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void collect()
    {
        goldContainer.GetComponent<Animator>().Play("goldGainAnim");
        SoundManager.SInstance.playSfx(SoundManager.SInstance.achivementSFX);
        SaveLoad_Manager.gameData.playerInfos.playerGoldCount += SaveLoad_Manager.gameData.playerInfos.earnedGold;
        _gameManager.updateGold();
        weaponaryMarket.updateGoldTexts();
        _gameManager.player_OBJ.GetComponent<PlayerAnimationController>().playWinAnim(_gameManager.player_OBJ.GetComponent<PlayerSkinController>().playerParts, "Defaults/Default_Default_Win", 1f);
        SaveLoad_Manager.gameData.playerInfos.earnedGold = 0;
        SaveLoad_Manager.SInstance.saveJson();
    }
    public void collectGolds()
    {
        Invoke("collect", 1);
        //if (SaveLoad_Manager.gameData.playerInfos.earnedGold > 0)
        //{
        //    int randomGoldObj = Random.Range(6, 11);
        //    for (int i = 0; i <= randomGoldObj; i++)
        //    {
        //        GameObject goldObj = Instantiate(goldPrefab, _gameManager.playerSpawnPos.transform.position, Quaternion.identity);
        //        goldObj.transform.SetParent(_gameManager.Canvas.transform);
        //        goldObj.transform.localScale = Vector3.one;
        //        goldAnimation(goldObj);
        //    }
        //    SaveLoad_Manager.gameData.playerInfos.playerGoldCount += SaveLoad_Manager.gameData.playerInfos.earnedGold;
        //    _gameManager.updateGold();
        //    _gameManager.player_OBJ.GetComponent<PlayerAnimationController>().playBodyAnim(_gameManager.player_OBJ.GetComponent<PlayerSkinController>().playerParts, "Defaults/Default_Default_Win", false, 1f);
        //    SaveLoad_Manager.gameData.playerInfos.earnedGold = 0;
        //}
        //await Task.Delay(5000);

    }
    public void goldAnimation(GameObject _gold)
    {
        LeanTween.move(_gold, goldContainer.transform.position, 0.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(() => { Destroy(_gold.gameObject); scaleUpGoldContainer(); });
    }
    public void scaleUpGoldContainer()
    {
        LeanTween.scale(goldContainer, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEase(LeanTweenType.easeInBounce).setOnComplete(() => LeanTween.scale(goldContainer, new Vector3(1f, 1f, 1f), 0.5f).setEase(LeanTweenType.easeOutBounce));
    }
}
