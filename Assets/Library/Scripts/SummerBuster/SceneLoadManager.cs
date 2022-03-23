using UnityEngine;
using UnityEngine.SceneManagement;

namespace SummerBuster
{
    public class SceneLoadManager : MonoBehaviour
    {

        public void ReloadThisScene()
        {
            SceneManager.LoadScene(StringData.SUMMERBUSTER);
        }





    } 
}
