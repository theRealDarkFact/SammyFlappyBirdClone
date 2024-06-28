using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Main Game Set Up")]
    [SerializeField] private int Score = 0;
    [SerializeField] private int Lives = 3;
    [SerializeField] private AudioClip TitleScreenBGM, GameOverBGM, StageBGM, CreditsBGM;
    [SerializeField] private float GameSpeed = 1.0f, GameSpeedIncreaseCounter = 0;
    private AudioSource BGMPlayer;


    [Header("UI Set Up")]
    [SerializeField] private HighScoreTable highscoreTable;
    [SerializeField] private TextMeshProUGUI ScoreValueText, LiveValueText, GO_HighScoreText, GO_ScoreText, PlatTextValue, LastScoreText;
    [SerializeField] private GameObject CreditsUI, PauseUI, GameOverUI, OptionsMenuUI, GameOverConfirmationUI, TitleScreenUI, HighScoreTable, PlayerNameEntry, InstructionsUI, FPSCOunter, GameControlIconA, GameControlIconB, GameControlIconC;
    [SerializeField] private Button StartButton, QuitButton, SettingsButton, MainMenuSetttingsButton, PauseSettingsButton, HighScoreButton, ReturnFromHighScore, OptionsReturnButton, QuitCancelButton, RestartButton, PauseResumeButton, GameOverDefaultButton, InstructionReturnButton, CreditsReturnButton;
    [SerializeField] private Button FlapButton, CrapButton, PauseButton, PlayerNameEnterDefaultButton;
    [Header("Icons for Buttons")]
    [SerializeField] private Sprite PauseButtonXB, PauseButtonPS, PauseButtonPC, PauseButtonMobile;
    [SerializeField] private Sprite FlapButtonXB, FlapButtonPS, FlapButtonPC, FlapButtonMobile;
    [SerializeField] private Sprite PoopButtonXB, PoopButtonPS, PoopButtonPC, PoopButtonMobile;


    public enum sPlat
    {
        Mobile,
        XboxUWP,
        PCUWP,
        PlayStation,
        PC
    }
    [Header("Platform Specific Items")][SerializeField] public sPlat PlatformType;

    private bool PauseGame = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Player.Instance.DisableControls();
            BGMPlayer = Camera.main.GetComponent<AudioSource>();
            #region
            //This preprocessor makes sure the engine is turning off things not needed to the end users and selecting the proper platforms.
#if UNITY_IOS || UNITY_ANDROID
            PlatformType = sPlat.Mobile;         
            Application.targetFrameRate = 30;                                                                   //Targeted 30 FPS
#elif UNITY_STANDALONE_WIN
            PlatformType = sPlat.PC;
            Application.targetFrameRate = 60;                                                                   //Targeted 60 FPS
#elif UNITY_WSA_10_0
            string devicePlatform = SystemInfo.deviceType.ToString();
            if (devicePlatform == "Desktop")
            {
                PlatformType = sPlat.PCUWP;
                Application.targetFrameRate = 60;                                                                   //Targeted 60 FPS
                Debug.Log("PC UWP Mode..");
            }
            else 
            {
                PlatformType = sPlat.XboxUWP;
                Application.targetFrameRate = 60;                                                                   //Targeted 60 FPS
                Debug.Log("Xbox UWP Mode..");
            }

#elif !UNITY_IOS || !UNITY_ANDROID || !UNITY_STANDALONE
            PlatformType = sPlat.PS;
            Application.targetFrameRate = 60;                                                                   //Targeted 60 FPS
#endif
#if UNITY_EDITOR
            PlatTextValue.transform.parent.gameObject.SetActive(true);
            PlatTextValue.text = PlatformType.ToString();
            FPSCOunter.SetActive(true);
#else
            PlatTextValue.transform.parent.gameObject.SetActive(false);
            FPSCOunter.SetActive(false);
#endif
            #endregion
            SetControlIcons();
        }
    }

    private void ShowControlHelpersXB() 
    {
        GameControlIconA.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = FlapButtonXB;
        GameControlIconB.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PoopButtonXB;
        GameControlIconC.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonXB;
        GameControlIconA.SetActive(true);
        GameControlIconB.SetActive(true);
        GameControlIconC.SetActive(true);
    }

    private void ShowControlHelpersPC()
    {
        GameControlIconA.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PoopButtonPC;
        GameControlIconB.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = FlapButtonPC;
        GameControlIconC.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonPC;
        GameControlIconA.SetActive(true);
        GameControlIconB.SetActive(true);
        GameControlIconC.SetActive(true);
    }

    public int GetLives() 
    {
        return Lives;
    }

    public float GetGameSpeed() 
    {
        return GameSpeed;
    }

    private void Start()
    {
        StartButton.Select();
    }

    public void StartGame() 
    {
        Score = 0;
        Lives = 3;
        Player.Instance.EnableControls();
        if(BGMPlayer.clip != StageBGM) 
        {
            BGMPlayer.Stop();
            BGMPlayer.clip = StageBGM;
            BGMPlayer.loop = true;
            BGMPlayer.Play();

        }
        TitleScreenUI.SetActive(false);
        if (PlatformType == sPlat.Mobile)
        {
            FlapButton.gameObject.SetActive(true);
            CrapButton.gameObject.SetActive(true);
            PauseButton.gameObject.SetActive(true);
        }
        if (PlatformType == sPlat.PCUWP) ShowControlHelpersPC();
        if (PlatformType == sPlat.XboxUWP) ShowControlHelpersXB();
        EnemySpawner.Instance.StartSpawner = true;
    }

    public void ShowHighScore() 
    {
        ReturnFromHighScore.Select();
        HighScoreTable.SetActive(true);
    }

    public void HideHighScores() 
    {
        if(TitleScreenUI.activeSelf) 
        {
            HighScoreButton.Select();
        }
        else 
        {
            RestartButton.Select();
        }
        HighScoreTable.SetActive(false);
    }

    private void SetControlIcons() 
    {
        switch (PlatformType) 
        {
            case sPlat.Mobile:
                PauseButton.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonMobile;
                break;
            case sPlat.XboxUWP:
                PauseButton.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonXB;
                break;
            case sPlat.PlayStation:
                PauseButton.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonPS;
                break;
            case sPlat.PC:
                PauseButton.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = PauseButtonPC;
                break;

        }

    }

    public void AddScore(int value) 
    {
        Score += value;
    }

    private void LateUpdate()
    {
        UpdateUI();
        ProgressiveSpeedAddition();
    }

    public void ProgressiveSpeedAddition() 
    {
        GameSpeedIncreaseCounter += Time.deltaTime;
        if(GameSpeedIncreaseCounter > 15f) 
        {
            GameSpeedIncreaseCounter = 0;
            GameSpeed += 0.1f;
            if (GameSpeed >= 3) GameSpeed = 3;
        }
    }

    public void togglePause() 
    {
        PauseGame = !PauseGame;
        PauseUI.SetActive(PauseGame);
        if (PauseGame) 
        {
            Time.timeScale = 0;
            PauseResumeButton.Select();
        }
        else 
        {
            Time.timeScale = 1f;
        }
    }

    public void TakeLife() 
    {
        Lives--;
        if (Lives < 1)
        {
            Player.Instance.DisableControls();
            GameOver();
        }
    }

    private void GameOver() 
    {
        Player.Instance.DisableControls();
        if (PlatformType == sPlat.Mobile)
        {
            FlapButton.gameObject.SetActive(false);
            CrapButton.gameObject.SetActive(false);
            PauseButton.gameObject.SetActive(false);
        }
        if(PlatformType == sPlat.PCUWP || PlatformType == sPlat.XboxUWP) 
        {
            GameControlIconA.SetActive(false);
            GameControlIconB.SetActive(false);
            GameControlIconC.SetActive(false);
        }
        GameOverUI.SetActive(true);
        if(highscoreTable.CompareForLowestScore() < Score) 
        {
            PlayerNameEntry.SetActive(true);
            PlayerNameEnterDefaultButton.Select();
            LastScoreText.text = Score.ToString();
        }

        RestartButton.Select();

        if (BGMPlayer.clip != GameOverBGM)
        {
            BGMPlayer.Stop();
            BGMPlayer.clip = GameOverBGM;
            BGMPlayer.loop = false;
            BGMPlayer.Play();
        }

        Time.timeScale = 0;
        GO_ScoreText.text = Score.ToString();
        GO_HighScoreText.text = highscoreTable.HighestScore().ToString();

    }

    private void UpdateUI() 
    {
        ScoreValueText.text = Score.ToString("000000");
        LiveValueText.text = Lives.ToString("00");
    }

    public void GameOverRestart() 
    {
        Player.Instance.EnableControls();
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void GameOverQuitButton() 
    {
        GameOverConfirmationUI.SetActive(true);
        QuitCancelButton.Select();
    }

    public void OptionsMenuShow() 
    {
        OptionsMenuUI.SetActive(true);
        OptionsReturnButton.Select();
    }

    public void OptionsMenuHide()
    {
        OptionsMenuUI.SetActive(false);
        if (PauseUI.activeSelf) PauseResumeButton.Select(); else if (GameOverUI.activeSelf) RestartButton.Select(); else MainMenuSetttingsButton.Select();
    }

    public void OptionsInstructShow() 
    {
        InstructionsUI.SetActive(true);
        InstructionReturnButton.Select();
    }


    public void OptionsInstructHide()
    {
        InstructionsUI.SetActive(false);
        OptionsReturnButton.Select();
    }

    public void QuitYes() 
    {
        Debug.Log("Quitting Application");
        Application.Quit();
    }

    public void QuitNo() 
    {
        if (PauseUI.activeSelf)
        {
            PauseSettingsButton.Select();
        }
        else if(GameOverUI.activeSelf)
        {
            GameOverDefaultButton.Select();
        }
        else if (TitleScreenUI.activeSelf) 
        {
            QuitButton.Select();
        }
        else 
        {
            SettingsButton.Select();
        }
        GameOverConfirmationUI.SetActive(false);
    }

    public void ShowCredits() 
    {
        CreditsReturnButton.Select();
        if (BGMPlayer.clip != CreditsBGM)
        {
            BGMPlayer.Stop();
            BGMPlayer.clip = CreditsBGM;
            BGMPlayer.loop = false;
            BGMPlayer.Play();

        }
        CreditsUI.SetActive(true);
        CreditsUI.gameObject.GetComponentInChildren<Animator>().Play("Default");
    }

    public void HideCredits() 
    {
        TitleScreenUI.SetActive(true);
        StartButton.Select();
        if (BGMPlayer.clip != StageBGM)
        {
            BGMPlayer.Stop();
            BGMPlayer.clip = StageBGM;
            BGMPlayer.loop = true;
            BGMPlayer.Play();

        }
        CreditsUI.SetActive(false);
    }
}
