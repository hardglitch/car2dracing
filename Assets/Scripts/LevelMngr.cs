using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMngr : MonoBehaviour
{
    public void OpenScene(int scn)
    {
        Time.timeScale = 1;
        Global.Level = scn;
        SceneManager.LoadScene(1);
    }


    public void NextLevel()
    {
        var lvl = Global.Level;
        if (Global.Level < Global.MaxLevel)
            Global.Level = ++lvl;
        Time.timeScale = 1;
        OpenScene(lvl);
    }


    public void ReloadLevel()
    {
        Time.timeScale = 1;
        OpenScene(Global.Level);
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