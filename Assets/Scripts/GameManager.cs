using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private bool readyForInput;
    public Player player;
    public GameObject nextButton;
   

    void Start()
    {
        nextButton.SetActive(false);
       
    }

    void Update()
    {
      
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            input.Normalize();

            if (input.sqrMagnitude > 0.5)
            {
                if (readyForInput)
                {
                    readyForInput = false;
                    player.Move(input);
                    nextButton.SetActive(IsLevelComplete());
                }
            }
            else
            {
                readyForInput = true;
            }

           
    }


    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    bool IsLevelComplete()
    {
        Module[] modules = FindObjectsOfType<Module>();
        foreach (var module in modules)
        {
            if (!module.onGoal) return false;
        }
        return true;
    }
   
}
