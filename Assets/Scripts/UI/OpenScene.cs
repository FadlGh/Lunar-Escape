using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    [SerializeField] private string audioName;

    void Start()
    {
        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Play(audioName);
    }

    public void OpenNextScene(string name)
    {
        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Pause(audioName);
        SceneManager.LoadScene(name);
    }
}
