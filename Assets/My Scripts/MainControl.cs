using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TapticPlugin;
using UnityEngine.SceneManagement;

public class MainControl : MonoBehaviour
{
    private Transform tinyGrid;
    private readonly string[] icon_tags = {"Image_1", "Image_2", "Image_3"};
    private readonly string[] tick_tags = {"TickBox_1", "TickBox_2", "TickBox_3", "TickBox_4", "TickBox_5", "TickBox_6", "TickBox_7", "TickBox_8", "TickBox_9"};
    private readonly string[] text_tags = {"Text_1", "Text_2", "Text_3"};
    private Image[] tick_boxes = new Image[9];
    private int[] numClicks = new int[9];
    private Color color;
    private float tAlpha, fAlpha;
    private GameObject interGo;
    private TMP_Text playTxt;
    private Button playButton;
    private int previousPosition_tiny = -5;
    private TransferManager transferManager;
    private Animator black_mask;

    private List<int> selected_modes = new List<int>();

    public Sprite selected, unselected; //Set in Editor
    public List<Transform> trans_el = new List<Transform>(); //Images to become transparent
    private List<TMP_Text> trans_text = new List<TMP_Text>(); //Text to become transparent
    public GameObject explosion; //Set in editor


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        black_mask = GameObject.FindGameObjectWithTag("BlackMask").GetComponent<Animator>();

        tinyGrid = GameObject.FindGameObjectWithTag("TinyGrid").GetComponent<Transform>();

        playTxt = GameObject.FindGameObjectWithTag("PlayText").GetComponent<TMP_Text>();
        playButton = GameObject.FindGameObjectWithTag("PlayButton").GetComponent<Button>();

        transferManager = GameObject.FindGameObjectWithTag("TransferManager").GetComponent<TransferManager>();

        color.a = 0;

        FixGridPositions();
        InitializeDisappearingElements();
        InitializeDisappearingText();
        InitializeTickBoxes();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeAlphaChildren();
        TapticTrigger();

        if (selected_modes.Count > 0)
        {
            playTxt.alpha = 100;
            playButton.interactable = true;
        }
        else
        {
            playTxt.alpha = 0;
            playButton.interactable = false;
        }

    }

    private void TapticTrigger()
    {
        switch (Mathf.Round(tinyGrid.position.y))
        {
            case -4:
                if (previousPosition_tiny != -4)
                {
                    TapticManager.Impact(ImpactFeedback.Light);
                    Debug.Log("Taptic feedback -4 triggered.");
                    previousPosition_tiny = -4;
                }
                break;
            case -3:
                if (previousPosition_tiny != -3)
                {
                    TapticManager.Impact(ImpactFeedback.Light);
                    Debug.Log("Taptic feedback -3 triggered.");
                    previousPosition_tiny = -3;
                }
                break;
            case -2:
                if (previousPosition_tiny != -2)
                {
                    TapticManager.Impact(ImpactFeedback.Light);
                    Debug.Log("Taptic feedback -2 triggered.");
                    previousPosition_tiny = -2;
                }
                break;
            case -1:
                if (previousPosition_tiny != -1)
                {
                    TapticManager.Impact(ImpactFeedback.Light);
                    Debug.Log("Taptic feedback -1 triggered.");
                    previousPosition_tiny = -1;
                }
                break;
        }
    }

    public void SelectMode(int mode)
    {
        TapticManager.Impact(ImpactFeedback.Medium);

        if (numClicks[mode] % 2 == 0)
        {
            Vector2 pos = new Vector2(tick_boxes[mode].transform.position.x, tick_boxes[mode].transform.position.y);
            interGo = Instantiate(explosion, pos, Quaternion.identity);
            Destroy(interGo, 2f);
            tick_boxes[mode].sprite = selected;
            selected_modes.Add(mode);
            transferManager.selected_packs.Add(mode);
        }
        else
        {
            tick_boxes[mode].sprite = unselected;
            selected_modes.Remove(mode);
            transferManager.selected_packs.Remove(mode);
        }

        numClicks[mode]++;

        Debug.Log("Selected modes are :");
        foreach (int o in selected_modes)
        {
            Debug.Log(o);
        }
    }

    private void InitializeTickBoxes()
    {
        int i = 0;
        foreach (string t in tick_tags)
        {
            tick_boxes[i] = GameObject.FindGameObjectWithTag(t).GetComponent<Image>();
            i++;
            Debug.Log(t + " Added to tick box array.");
        }
    }


    private void InitializeDisappearingElements()
    {
        foreach (string t in icon_tags)
        {
            trans_el.Add(GameObject.FindGameObjectWithTag(t).GetComponent<Transform>());
            Debug.Log(t + " Added to disappearing elements list.");
        }
    }

    private void InitializeDisappearingText()
    {
        foreach (string t in text_tags)
        {
            trans_text.Add(GameObject.FindGameObjectWithTag(t).GetComponent<TMP_Text>());
            Debug.Log(t + " Added to disappearing text array.");
        }
    }


    private void FixGridPositions()
    {
        tinyGrid.position = new Vector2(tinyGrid.position.x, -453f);

        Debug.Log("Grids successfully fixed.");
    }

    
    private void ChangeAlphaChildren()
    {
        color = trans_el[0].GetComponent<Image>().color;

        fAlpha = color.a;
        //tAlpha = (-Mathf.Round(tinyGrid.position.y) - 1) / 4;

        if (Mathf.Round(tinyGrid.position.y) == -4)
        {
            tAlpha = 1f;
        } else if (Mathf.Round(tinyGrid.position.y) > -1)
        {
            tAlpha = 0f;
        } else
        {
            tAlpha = -Mathf.Round(tinyGrid.position.y) / 4 - 0.3f;
        }

        color.a = Mathf.Lerp(fAlpha, tAlpha, 0.19f);

        foreach (Transform tt in trans_el)
        {
            tt.GetComponent<Image>().color = color;
            tt.GetComponent<Image>().color = color;
        }
        foreach (TMP_Text tt in trans_text)
        {
            tt.alpha = color.a;
        }
        Debug.Log(Mathf.Round(tinyGrid.position.y) + "\t\t" + color.a);
    }
   

    public void LaunchCardScreen()
    {
        TapticManager.Impact(ImpactFeedback.Medium);
        StartCoroutine(GoToCardGame());
    }

    private IEnumerator GoToCardGame()
    {
        black_mask.Play("BlackFadeOut");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        TapticManager.Impact(ImpactFeedback.Medium);
        StartCoroutine(BackToTheMenu());
    }

    private IEnumerator BackToTheMenu()
    {
        black_mask.Play("BlackFadeOut");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}