using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private const string GAME_SCENE_NAME = "Game";
    #region Button variables
    [SerializeField] private Button playNowBtn;
    [SerializeField] private Button quitGameBtn;
    #endregion

    #region Add and Remove Listeners
    private void OnEnable()
    {
        playNowBtn.onClick.AddListener(StartGame);
        quitGameBtn.onClick.AddListener(QuitGame);
    }
    private void OnDisable()
    {
        playNowBtn.onClick.RemoveListener(StartGame);
        quitGameBtn.onClick.RemoveListener(QuitGame);
    }
    #endregion

    #region private methods
    private void StartGame()
    {
        print("Start game");
        SceneManager.LoadScene(GAME_SCENE_NAME);
    }
    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    #endregion
}
