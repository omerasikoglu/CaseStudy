using UnityEngine;
using UnityEngine.SceneManagement;

namespace Affairs3D
{
    public class SceneLoadManager : MonoBehaviour
    {
        public void ReloadThisScene()
        {
            SceneManager.LoadScene(StringData.AFFAIRS_3D);
        }
    } 
}
