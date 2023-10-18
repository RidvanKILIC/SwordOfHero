using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class gameOverScript : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text gameOverText;
    [SerializeField] GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setText(string text)
    {
        gameOverText.text = text + " won!";
    }
    public void mainMenu()
    {
    }
    public void restart()
    {
        sceneManager.reloadGame();
    }
    public void openCloseGameOverPanel(bool _state)
    {
        gameOverPanel.SetActive(_state);
    }
}
