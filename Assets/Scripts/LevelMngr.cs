using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMngr : MonoBehaviour
{
    public void OpenScene(int scn)
    {
        Time.timeScale = 1;
        Global.SetLevel(scn);
        SceneManager.LoadScene(1);
    }


    public void NextLevel()
    {
        int lvl = Global.GetLevel();
        if (Global.GetLevel() < Global.GetMaxLevel())
            Global.SetLevel(++lvl);
        Time.timeScale = 1;
        OpenScene(lvl);
    }


    public void ReloadLevel()
    {
        Time.timeScale = 1;
        OpenScene(Global.GetLevel());
    }


    public void AppQuit()
    {
        Application.Quit();
    }


    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
