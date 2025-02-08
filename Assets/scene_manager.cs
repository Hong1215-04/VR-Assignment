using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    private string nextCondition = "";
    [SerializeField] private Animator trans;
    private float transitionDuration = 1f;
    [SerializeField] private float levelBetweenDuration = 4f;
    [SerializeField] private int nextSceneIndex; // level go when "Next"

    private void Update()
    {
        if (string.Equals(nextCondition, "Next"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    // Show transition
    IEnumerator LoadNextScene()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Function set condition to change scene
    public void StartGame()
    {
        setNextCondition("Next");
    }

    // Function to quit the game
    public void QuitGame()
    {
        // Reset the state of game before quit
        PlayerPrefs.SetInt("MainScene_DialogueCompleted", 0);
        PlayerPrefs.Save();
        UnityEditor.EditorApplication.isPlaying = false;   
        Application.Quit();
    }

    public string getNextCondition() { return nextCondition; }
    public void setNextCondition(string nCond) { nextCondition = nCond; }
    public float getLevelBetweenDuration() { return levelBetweenDuration; }
    public void setLevelBetweenDuration(float t) { levelBetweenDuration = t; }

    public void setNextSceneIndex(int i) { nextSceneIndex = i; }

}
