using UnityEngine.SceneManagement;

namespace Runtime.InGame
{
    public class Navigator
    {
        public static void LoadScene(string scene)
        {
            SceneManager.LoadSceneAsync(scene);
        }
    }
}