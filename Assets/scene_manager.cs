using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_manager : MonoBehaviour
{
    private string nextCondition = "";
    [SerializeField] private Animator trans;
    private float transitionDuration = 1f;
    [SerializeField] private int nextSceneIndex; // level go when "Next"
    [SerializeField] private int looseSceneIndex; // level go when "Loose"
    private MainSceneStateManager msManager;

    private void Update()
    {
        if (string.Equals(nextCondition, "Next"))
        {
            StartCoroutine(LoadNextScene());
        }
        else if (string.Equals(nextCondition, "Loose"))
        {
            StartCoroutine(LoadLooseScene());
        }
    }

    // Show goal cut scene transition
    IEnumerator LoadNextScene()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(nextSceneIndex);
    }

    // Show booo cut scene transition
    IEnumerator LoadLooseScene()
    {
        trans.SetTrigger("Start");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(looseSceneIndex);

    }

    public string getNextCondition() { return nextCondition; }
    public void setNextCondition(string nCond) { nextCondition = nCond; }

}
