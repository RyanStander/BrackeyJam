using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int beginGameSceneIndex = 1;
    public int GhostStationSceneIndex = 2;
    public int MonsterStationSceneIndex = 3;
    public int SubmarineStationSceneIndex = 4;

    public void StartGame()
    {
        SceneManager.LoadScene(beginGameSceneIndex);
    }

    public void LoadGhostStation()
    {
        SceneManager.LoadScene(GhostStationSceneIndex);
    }

    public void LoadMonsterStation()
    {
        SceneManager.LoadScene(MonsterStationSceneIndex);
    }
    public void LoadSubmarineStation()
    {
        SceneManager.LoadScene(SubmarineStationSceneIndex);
    }
}
