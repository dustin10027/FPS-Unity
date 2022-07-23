using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Manages the logic of the shooter game.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("References")]
    public Text timerText;          //The timer text to update
    public int totalTargets;        //The amount of targets that are int the scene
    [HideInInspector]
    public int remainingTargets;    //The remaming targets of the scene
    private float gameTimer = 0f;   //Stores the how time has passed since the first target is destroyed
    
    /// <summary>
    /// Unity Start Callback
    /// </summary>
    void Start()
    {
        totalTargets = GameObject.FindGameObjectsWithTag("Target").Length;
        remainingTargets = totalTargets;
    }
    /// <summary>
    /// Unity Update Callback
    /// </summary>
    void Update()
    {
        ///Updating the text
        timerText.text = gameTimer.ToString("F2");

        if (remainingTargets < totalTargets && remainingTargets != 0) {
            gameTimer += Time.deltaTime;
        }

        ///Reseting the Scene
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(0);
        }
    }
}