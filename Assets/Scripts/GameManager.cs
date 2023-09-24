using Assets.Scripts.Data;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool isGameActive;
	[HideInInspector]
	public bool isGameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public GameObject gameOverText;
    public TMPro.TMP_InputField nameInput;
    public GameObject player;
    private  string playername;
    public int score = 0;
    private PlayerList playerList = new PlayerList();
    private  const string FILENAME = "/playerData.json";

	private PlayerController playerController;
    void Start()
    {
        isGameOver = false;
        isGameActive = false;
        playerController = player.GetComponent<PlayerController>();
		player.SetActive(false);

	}

    public void SetGameOver()
    {
        SavePlayerData(new PlayerData { Name = playername, Score = score });
        isGameOver = true;
    }

	private void SavePlayerData(PlayerData playerData)
	{
		playerList.players.Add(playerData);
		string json = JsonUtility.ToJson(playerList);
		File.WriteAllText(Application.persistentDataPath + FILENAME, json);
	}

	public void LoadPlayerData()
	{
		string filePath = Application.persistentDataPath + FILENAME;
		if (File.Exists(filePath))
		{
			string json = File.ReadAllText(filePath);
			playerList = JsonUtility.FromJson<PlayerList>(json);
		}
		else
		{
			// Si le fichier n'existe pas, créez un nouveau PlayerData.
			playerList = new PlayerList();
		}
	}



	void Update()
    {
		scoreText.gameObject.SetActive(isGameActive);
		livesText.gameObject.SetActive(isGameActive);
		if (isGameOver)
        {
            gameOverText.SetActive(true);
        }

        if(isGameActive)
        {

			SetScore(score);
			SetLives(playerController.lifes);
		}


	}

    void SetScore(int score)
    {
        scoreText.text = $"Score: {score}";
    }

	void SetLives(int lives)
	{
		livesText.text = $"Lives: {lives}";
	}

    public void StartGame()
    {
		player.SetActive(true);
		titleScreen.SetActive(false);
        isGameActive = true;
		
	}

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
