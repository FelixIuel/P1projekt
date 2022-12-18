using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagerNS {
    public class SceneManagement : MonoBehaviour {
        public static void ChangeScene(string scene){
            SceneManager.LoadScene(scene);
        }
        public void CloseGame(){
           Application.Quit();
        }
    }
}
