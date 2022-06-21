using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] int score;
    [SerializeField] int highscore;
    public Color[] template = { new Color32(255, 81, 81, 255), new Color32(255, 129, 82, 255), new Color32(255, 233, 82, 255), new Color32(163, 255, 82, 255), new Color32(82, 207, 255, 255), new Color32(170, 82, 255, 255) };

    private UIController uiController;

    private float time;
    [SerializeField] float timeOfGame;

    [SerializeField] NumberContentController numberContentController;
    [SerializeField] ContentController contentController;

    [SerializeField] List<int> currentArr;
    [SerializeField] List<int> currentArrSorted;
    [SerializeField] int currentUserValue;
    [SerializeField] int leng;

    private int indexChooseNumber;

    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        UpdateSlider();

        if(time < 0)
        {
            GameOver();
        }
    }

    public void UpdateSlider()
    {
        uiController.UpdateSlider(time);
    }

    public void SetSlider()
    {
        uiController.SetSlider(timeOfGame);
    }

    public void OnPressHandle(int value)
    {
        if(value == currentArrSorted[indexChooseNumber])
        {
            numberContentController.UpdateInfo(indexChooseNumber, value);
            UpdateScore();
            StartCoroutine(StartNextTurn());
        }
        else
        {
            GameOver();
        }
    }

    public void InsertNumberToQueue(int number)
    {
        numberContentController.Spaw(number);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        uiController.GameOver();
    }

    public void UpdateScore()
    {
        score++;
        if(highscore <= score)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", highscore);
            uiController.UpdateHighScore(highscore);
        }
        uiController.UpdateScore(score);
    }

    IEnumerator StartNextTurn()
    {
        yield return new WaitForSeconds(0.5f);
        NextTurn();
    }

    public void NextTurn()
    {
        currentUserValue = 0;

        leng = Random.Range(3, 6);
        currentArr = new List<int>();
        currentArrSorted = new List<int>();

        numberContentController.Spaw(leng);

        var list = new List<int>();
        for (int i = 0; i < leng; i++)
        {
            int value = Random.Range(1, 100);
            list.Add(0);
            currentArr.Add(value);
            currentArrSorted.Add(value);
        }

        

        currentArrSorted.Sort();
        time = timeOfGame;

        numberContentController.UpdateAllInfo(currentArrSorted);
        indexChooseNumber = Random.Range(0,currentArrSorted.Count);
        numberContentController.HideInfo(indexChooseNumber);

        for(int i = 0; i< list.Count; i++)
        {
            list[i] = GetNumber();
        }

        list[Random.Range(0, list.Count)] = currentArrSorted[indexChooseNumber];

        contentController.SpawButton(list);

    }

    private int GetNumber()
    {
        if(indexChooseNumber == 0)
        {
            return Random.Range(currentArrSorted[indexChooseNumber+1], 100);
        }
        else if(indexChooseNumber == currentArrSorted.Count - 1)
        {
            return Random.Range(1, currentArrSorted[indexChooseNumber - 1]);
        }
        else
        {
            int isLeftSide = Random.Range(0, 2);
            if (isLeftSide == 0)
            {
                return Random.Range(0, currentArrSorted[indexChooseNumber-1]);
            }
            else
            {
                return Random.Range(currentArrSorted[indexChooseNumber+1], 100);
            }
        }
    }

    public void Reset()
    {
        Time.timeScale = 1;

        time = timeOfGame;
        SetSlider();
        score = 0;
        highscore = PlayerPrefs.GetInt("score");
        uiController.UpdateScore(score);
        uiController.UpdateHighScore(highscore);

        NextTurn();
    }

}
