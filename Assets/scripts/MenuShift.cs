using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuShift : MonoBehaviour
{

    //private int nextScreen;


    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);


    }


    public void ToGame()
    {
        SceneManager.LoadScene(1);


    }
    public void ToH2P()
    {
        SceneManager.LoadScene(2);


    }
    public void YouLost()
    {
        //if (pollution =>300 || backing=<0){
        //
        //
        //
        //
        //}

        

    }


}
