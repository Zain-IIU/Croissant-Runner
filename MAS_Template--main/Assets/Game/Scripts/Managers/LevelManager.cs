using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] levels;
    
    [SerializeField] private Color[] levelBackgroundColor;
    [SerializeField] private Camera mainCamera;
    public static int CurrentLevel
    {
        get => PlayerPrefs.GetInt("CurrentLevel", 0);
        set
        {
            PlayerPrefs.SetInt("CurrentLevel", value);
            PlayerPrefs.Save();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        Application.targetFrameRate = 60;
        DisableAllLevels();
        LoadGame();   
        ChangeBackgroundColor();
    }
    
    private void ChangeBackgroundColor()
    {
        mainCamera.backgroundColor = levelBackgroundColor[GetLevelIndex()];
    }

    private void DisableAllLevels()
    {
        foreach (var level in levels)
        {
            level.SetActive(false);
        }
    }
    private void LoadGame()
    {
        CurrentLevel = GetLevelIndex();
        print(CurrentLevel);
        levels[CurrentLevel].SetActive(true);

        MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelStarted, CurrentLevel);
    }

    int GetLevelIndex()
    {
        return CurrentLevel % levels.Length;
    }
    [ContextMenu("Load Next Level")]
    public void IncrementLevelIndex()
    {
        MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelCompleted, CurrentLevel);
        CurrentLevel++;
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        PlayerPrefs.Save();
        Debug.Log("Loading Next level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    [ContextMenu("Restart Level")]
    public void ReplayLevel()
    {
        MASGameEvents.instance.LevelEvent(MASGameEvents.LevelEvents.LevelFailed, CurrentLevel);
        Debug.Log("Loading same level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}