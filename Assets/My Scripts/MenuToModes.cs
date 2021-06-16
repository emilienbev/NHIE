using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TapticPlugin;

public class MenuToModes : MonoBehaviour
{
    private Animator black_mask;

    void Start()
    {
        Application.targetFrameRate = 60;
        black_mask = GameObject.FindGameObjectWithTag("BlackMask").GetComponent<Animator>();
    }
    public void GoToModes()
    {
        TapticManager.Impact(ImpactFeedback.Medium);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        black_mask.Play("BlackFadeOut");
        yield return new WaitForSeconds(.50f);
        SceneManager.LoadScene("Gamemodes", LoadSceneMode.Single);
    }
}