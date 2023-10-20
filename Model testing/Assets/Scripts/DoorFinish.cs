using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorFinish : MonoBehaviour
{
    [SerializeField] private AudioSource FinishSound;

    private bool levelCompleted = false;
    

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player" && !levelCompleted)
        {
            Debug.Log ("YES!");
            FinishSound.Play();
            levelCompleted = true;
            Completelevel();
            
        }
    }

    private void Completelevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
