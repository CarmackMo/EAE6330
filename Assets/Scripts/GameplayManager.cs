using UnityEngine;
using UnityEngine.SceneManagement;



public class GameplayManager : Singleton<GameplayManager>
{


    public void ReloadScene()
    {
        Scene currScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currScene.name, LoadSceneMode.Single);
    }



}
