using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    [SerializeField]
    private GameObject startButton, exitButton;

    [SerializeField]
    private Text HighestScoreText;

    public int highestScore;
    

    // Start is called before the first frame update
    void Start()
    {

        startButton.GetComponent<Transform>().DOLocalMoveX(5, 1f).SetEase(Ease.OutBack);
        exitButton.GetComponent<Transform>().DOLocalMoveX(5, 1f).SetEase(Ease.OutBack);

        HighestScoreText.text = "HighScore: "+ PlayerPrefs.GetInt("score").ToString();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public void StartOrQuit(int determiner)
    {

        if (determiner == 1)
        {

            SceneManager.LoadScene(1);

        } else if (determiner == 0)
        {

            Application.Quit();

        }


    }


}
