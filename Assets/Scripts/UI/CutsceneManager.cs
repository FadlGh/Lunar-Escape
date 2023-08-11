using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private float imageDisplayTime = 5.0f;
    [SerializeField] private Image[] cutsceneImages;
    [SerializeField] private string sceneName;

    private void Start()
    {
        if (cutsceneImages.Length > 0)
        {
            StartCoroutine(PlayCutscene());
        }
        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Play("Cutscene");
        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Pause("Background");
    }

    private IEnumerator PlayCutscene()
    {
        foreach (Image image in cutsceneImages)
        {
            image.gameObject.SetActive(true);
            yield return new WaitForSeconds(imageDisplayTime);
            image.gameObject.SetActive(false);
        }

        GameObject.FindGameObjectWithTag("AM").GetComponent<AudioManager>().Pause("Cutscene");
        SceneManager.LoadScene(sceneName);
    }
}
