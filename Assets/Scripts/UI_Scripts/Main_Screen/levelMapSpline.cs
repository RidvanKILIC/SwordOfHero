using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;
using RK.GameDatas.levelItems;
using DanielLochner.Assets.SimpleScrollSnap;
public class levelMapSpline : MonoBehaviour
{
	[SerializeField] List<levelSplineItem> levelPaths = new List<levelSplineItem>();
	[SerializeField] GameObject warningPopUp;
	public GameObject Player;
	levelSplineItem currentLevel;
	levelSplineItem nextLevel;
	private LTBezierPath visualizePath;
	[SerializeField] bool goToPanel = false;
	[SerializeField] LevelMapScrooling _MapScrool;
	int currentLevelIndex=0;
	levelController _levelController;
	static bool isMoving = false;
	[SerializeField]SimpleScrollSnap _scroolSnap;
	void Start()
	{
		_levelController = GameObject.FindGameObjectWithTag("Managements").GetComponent<levelController>();
		
		// move 

		// 90 degree test
		// path = new Vector3[] {new Vector3(7.5f, 0f, 0f), new Vector3(0f, 0f, 2.5f), new Vector3(2.5f, 0f, 0f), new Vector3(0f, 0f, 7.5f)};
		//visualizePath = new LTBezierPath(selectedPath);
		//LeanTween.move(dude1, selectedPath, 10f).setOrientToPath2d(true);

		//// move local
		//LeanTween.moveLocal(dude2, selectedPath, 10f).setOrientToPath2d(true);
	}
	public void setPlayer()
    {
		currentLevel = levelPaths.Find(x => x.levelName == SaveLoad_Manager.gameData.currentLevelName);
		//Debug.Log("Current Level" + SaveLoad_Manager.gameData.currentLevelName);
		
		_scroolSnap.Setup();
		//Player Image
		//VehicleImage
		Player.GetComponent<PlayerHUD_Controller>().Init();
		Player.transform.SetParent(currentLevel.startPos.transform.parent.transform.parent.transform);
		Player.transform.position = currentLevel.startPos.position;
	}
	public void initPaths()
    {
		_MapScrool.init();
		foreach (var item in levelPaths)
		{
			if (item.levelName != "Level 1")
				item.path = getPath(item.points);
		}
	}
	private void Update()
	{
		if (goToPanel)
		{
			//goToPanel = false;
			//gotoNextLevel();
		}
	}
	public void setSelectedLevel(string levelName)
	{
		//Debug.Log(levelName);
		currentLevel = levelPaths.Find(x => x.levelName == levelName);
		
		//currentLevel = levelPaths[currentLevelIndex];

		Player.transform.SetParent(currentLevel.startPos.transform.parent.transform.parent.transform);
		Player.transform.position = currentLevel.startPos.position;
		initScroolPos();
	}
	public void initScroolPos()
    {
		//Debug.Log(currentLevel.levelName);
		//_MapScrool.target = currentLevel.startPos.GetComponent<RectTransform>();
		_MapScrool.CenterOnItem(currentLevel.startPos.GetComponent<RectTransform>());
	}
	public void setCurrentLevel(string levelName)
	{
		currentLevel = levelPaths.Find(x => x.levelName == levelName);
		//visualizePath = new LTBezierPath(nextLevel.path);
	}
	public void setNextLevel(string levelName)
	{
		nextLevel = levelPaths.Find(x => x.levelName == levelName);
		//visualizePath = new LTBezierPath(nextLevel.path);
	}
	public void gotoNextLevel(int levelIndex)
	{
		initPaths();
		currentLevelIndex++;
		nextLevel = levelPaths[levelIndex];
		Debug.Log(nextLevel.levelName);
		//_MapScrool.target = nextLevel.startPos.GetComponent<RectTransform>();
		//_MapScrool.CenterOnItem();
		LeanTween.moveSpline(Player, nextLevel.path, 1f).setSpeed(1.5f).setOnComplete(()=> { _MapScrool.CenterOnItem(nextLevel.startPos.GetComponent<RectTransform>());Debug.Log("Moved");});/*setOnComplete(()=> Player.GetComponent<Animator>().SetBool("PlayAnim", false));*/
	}
	public void goToLevel(string levelName)
	{
		SoundManager.SInstance.playSfx(SoundManager.SInstance.buttonSFX);
		//Debug.Log("called level:" + levelName);
		var lvlItem = _levelController.levelObjects.Find(x => x.GetComponent<levelItem>().levelName == levelName);
        if (lvlItem != null)
        {
			if (!lvlItem.GetComponent<levelItem>().getIsLock() && !string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
			{
				
				initPaths();
				currentLevelIndex++;
				//nextLevel = levelPaths[currentLevelIndex];
				nextLevel = levelPaths.Find(x => x.levelName == levelName);
				int currentIndex = LevelList.LInstance.levelList.FindIndex(x => x.levelName == currentLevel.levelName);
				var nextIndex = LevelList.LInstance.levelList.Find(x => x.levelName == nextLevel.levelName);
                //Debug.Log(nextLevel.levelName);
                //_MapScrool.target = nextLevel.startPos.GetComponent<RectTransform>();
                //_MapScrool.CenterOnItem();
                if (nextIndex!=null)
                {
                    if (!nextIndex.isBossDead)
                    {
						var Level = _levelController.levelObjects.Find(x => x.GetComponent<levelItem>().levelName == levelName).GetComponent<levelItem>();
						if (Level != null)
						{
							//Debug.Log("Level Set!");
							//LevelList.LInstance.currentLevelItem = Level;
							LevelList.LInstance.currentLevelItem = Level.name;
						}

						sceneManager.sceneToLoad = "miniGame_Scene";
						sceneManager.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
						LeanTween.cancel(Player);
						if (currentLevel != nextLevel && Level != null)
						{
							LeanTween.moveSpline(Player, nextLevel.path, 1f).setSpeed(1.5f).setOnComplete(() => { _MapScrool.CenterOnItem(nextLevel.startPos.GetComponent<RectTransform>()); sceneManager.loadScene("loadingScene"); });/*setOnComplete(()=> Player.GetComponent<Animator>().SetBool("PlayAnim", false));*/
						}
						else
						{
							sceneManager.loadScene("loadingScene");
						}
					}
                    else
                    {
						//Debug.Log("Already Cleared");

						warningPopUp.GetComponent<warningPanelText>().setText("Warning_Texts", "area_cleared_Text");
						activePopUp();
					}
					
				}

					
				//currentLevel = nextLevel;

			}
            else
            {

                if (string.IsNullOrEmpty(SaveLoad_Manager.gameData.playerInfos.equippedSword))
                {
					warningPopUp.GetComponent<warningPanelText>().setText("Warning_Texts", "no_weapon_Text");
					activePopUp();
				}
				else if (lvlItem.GetComponent<levelItem>().getIsLock())
                {
					warningPopUp.GetComponent<warningPanelText>().setText("Warning_Texts", "area_Locked_Text");
					activePopUp();
				}

			}
		}
	}
	Vector3[] getPath(Transform[] _points)
	{
		Vector3[] pathToReturn = new Vector3[_points.Length];
		for (int i = 0; i < _points.Length; i++)
		{
			pathToReturn[i] = _points[i].transform.position;
		}

		return pathToReturn;
	}
	//void OnDrawGizmos()
	//{
	//	// Debug.Log("drwaing");
	//	Gizmos.color = Color.red;
	//	if (visualizePath != null)
	//		visualizePath.gizmoDraw(); // To Visualize the path, use this method
	//}
	[System.Serializable]
	public class levelSplineItem
	{
		public string levelName;
		public Transform startPos;
		public Transform[] points;
		public Vector3[] path;
	}
	public void activePopUp()
    {
		SoundManager.SInstance.playSfx(SoundManager.SInstance.deniedSFX);
		warningPopUp.SetActive(true);
    }
	public void deActivePopUp()
	{
		warningPopUp.SetActive(false);
	}
}
