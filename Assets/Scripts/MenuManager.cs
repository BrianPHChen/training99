using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public void onStartGame(string SceneName) {
        Debug.Log("start button clicked");
        CSVManager.AppendToFile(new string[1] {"start"});
        SceneManager.LoadScene(SceneName);
        
    }
    
    public void QuitGame() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
