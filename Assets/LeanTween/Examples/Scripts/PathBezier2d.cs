using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;

public class PathBezier2d : MonoBehaviour {
	[SerializeField] List<levelItem> levelPaths = new List<levelItem>();
	public GameObject Player;
	levelItem currentLevel;
	levelItem nextLevel;
	private LTBezierPath visualizePath;
	[SerializeField]bool goToPanel = false;
	void Start () {
		// move 
		foreach(var item in levelPaths)
        {
			if(item.levelName!="Level 1")
			item.path = getPath(item.points);
        }
		// 90 degree test
		// path = new Vector3[] {new Vector3(7.5f, 0f, 0f), new Vector3(0f, 0f, 2.5f), new Vector3(2.5f, 0f, 0f), new Vector3(0f, 0f, 7.5f)};
		//visualizePath = new LTBezierPath(selectedPath);
		//LeanTween.move(dude1, selectedPath, 10f).setOrientToPath2d(true);

		//// move local
		//LeanTween.moveLocal(dude2, selectedPath, 10f).setOrientToPath2d(true);
	}
    private void Update()
    {
        if (goToPanel)
        {
			goToPanel = false;
			gotoNextLevel();
        }
    }
    public void setSelectedLevel(string levelName)
    {
		currentLevel = levelPaths.Find(x => x.levelName == levelName);
		visualizePath = new LTBezierPath(currentLevel.path);
		Player.transform.position = currentLevel.startPos.position;
	}
	public void setNextLevel(string levelName)
    {
		nextLevel = levelPaths.Find(x => x.levelName == levelName);
		visualizePath = new LTBezierPath(nextLevel.path);
	}
	public void gotoNextLevel()
	{ 
		LeanTween.moveSpline(Player, nextLevel.path, 10f).setOrientToPath2d(true).setSpeed(2f);
	}
	Vector3[] getPath(Transform[] _points)
	{
		Vector3[]pathToReturn = new Vector3[_points.Length];
		for (int i=0;i<_points.Length;i++)
        {
			pathToReturn[i] = _points[i].transform.position;
        }

			return pathToReturn;
    }
	void OnDrawGizmos(){
		// Debug.Log("drwaing");
		Gizmos.color = Color.red;
		if(visualizePath!=null)
			visualizePath.gizmoDraw(); // To Visualize the path, use this method
	}
	[System.Serializable]
	public class levelItem
    {
		public string levelName;
		public Transform startPos;
		public Transform[] points;
		public Vector3[] path;
    }
}
