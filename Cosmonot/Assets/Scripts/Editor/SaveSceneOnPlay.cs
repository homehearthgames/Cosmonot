using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoadAttribute]
public static class SaveSceneOnPlay {
    static SaveSceneOnPlay() {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    private static void LogPlayModeState(PlayModeStateChange state) {
        if(state == PlayModeStateChange.ExitingEditMode){
            string path = EditorSceneManager.GetActiveScene().path;
            bool saveOK = EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), string.Join("/", path));
            Debug.Log("Saved Scene " + (saveOK ? "OK" : "Error!"));
        }
    }
}