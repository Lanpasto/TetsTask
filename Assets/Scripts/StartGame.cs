using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
       public void LoadScene()
    {
        SceneManager.LoadScene(0);//load game scene
    }
}
