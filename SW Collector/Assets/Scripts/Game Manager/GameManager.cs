using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text scoreTracker;
    public GameObject collectedUI;
    public GameObject resultUI;
    public Text resultText;

    private bool gameEnded = false;
    private float restartDelay = 2f;
    private float completionDelay = 1f;
    private int numOfPowerConverters = 0;
    private int numOfCollectedPCs = 0;

	public void PlayerDied()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Invoke("Restart", restartDelay);
            collectedUI.SetActive(false);
            resultText.text = "You Died";
            resultUI.SetActive(true);
        }
    }

    public void Score()
    {
        numOfCollectedPCs++;

        scoreTracker.text = numOfCollectedPCs + " / " + numOfPowerConverters;

        if(numOfCollectedPCs >= numOfPowerConverters)
        {
            Invoke("StageComplete", completionDelay);
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StageComplete()
    {
        Debug.Log("Stage completed!");
        resultText.text = "Stage Completed";
        resultUI.SetActive(true);
        Invoke("QuitThis", completionDelay);
    }

    private void Start()
    {
        numOfPowerConverters = FindObjectsOfType<PowerConverter>().Length;
        scoreTracker.text = numOfCollectedPCs + " / " + numOfPowerConverters;
    }

    void QuitThis()
    {
        Application.Quit();
    }
}
