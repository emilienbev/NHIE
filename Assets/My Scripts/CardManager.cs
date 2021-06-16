using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TapticPlugin;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards = new GameObject[3];
    public GameObject button_hider;
    private Animator card_0, card_1, card_2;
    private QuestionHandler question_handler;
    private Animator black_mask;

    private int numClick = 0;
    private int total_clicks = 0;
    private int backIndex, midIndex, frontIndex = 0;

    private bool last_cards = false;

    // Start is called before the first frame update
    void Start()
    {
        black_mask = GameObject.FindGameObjectWithTag("BlackMask").GetComponent<Animator>();
        button_hider = GameObject.FindGameObjectWithTag("ButtonHider");
        button_hider.SetActive(false);

        card_0 = cards[0].GetComponent<Animator>();
        card_1 = cards[1].GetComponent<Animator>();
        card_2 = cards[2].GetComponent<Animator>();

        question_handler = GameObject.FindGameObjectWithTag("QuestionHandler").GetComponent<QuestionHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCard()
    {
        Debug.Log("Next Card button pressed.");
        button_hider.SetActive(true);
        total_clicks++;

        if (question_handler.q_counter == question_handler.total_length)
        {
            last_cards = true;
        }

        StartCoroutine(NextCardEnum());
    }

    private void GetCardIndexes()
    {
        switch (numClick)
        {
            case 0:
                backIndex = cards[2].transform.GetSiblingIndex();
                midIndex = cards[1].transform.GetSiblingIndex();
                frontIndex = cards[0].transform.GetSiblingIndex();
                break;
            case 1:
                backIndex = cards[0].transform.GetSiblingIndex();
                midIndex = cards[2].transform.GetSiblingIndex();
                frontIndex = cards[1].transform.GetSiblingIndex();
                break;
            case 2:
                backIndex = cards[1].transform.GetSiblingIndex();
                midIndex = cards[0].transform.GetSiblingIndex();
                frontIndex = cards[2].transform.GetSiblingIndex();
                break;
        }
    }

    private void SetCardIndexes()
    {
        if (last_cards == true)
        {
            switch (numClick)
            {
                case 0:
                    cards[0].GetComponent<Animator>().Play("DeadCard");
                    cards[1].transform.SetSiblingIndex(frontIndex);
                    cards[2].transform.SetSiblingIndex(midIndex);
                    break;
                case 1:
                    cards[0].transform.SetSiblingIndex(midIndex);
                    cards[1].GetComponent<Animator>().Play("DeadCard_2");
                    cards[2].transform.SetSiblingIndex(frontIndex);
                    break;
                case 2:
                    cards[0].transform.SetSiblingIndex(frontIndex);
                    cards[1].transform.SetSiblingIndex(midIndex);
                    cards[2].GetComponent<Animator>().Play("DeadCard_3");
                    break;
            }
        }
        else
        {
            switch (numClick)
            {
                case 0:
                    cards[0].transform.SetSiblingIndex(backIndex);
                    cards[1].transform.SetSiblingIndex(frontIndex);
                    cards[2].transform.SetSiblingIndex(midIndex);
                    break;
                case 1:
                    cards[0].transform.SetSiblingIndex(midIndex);
                    cards[1].transform.SetSiblingIndex(backIndex);
                    cards[2].transform.SetSiblingIndex(frontIndex);
                    break;
                case 2:
                    cards[0].transform.SetSiblingIndex(frontIndex);
                    cards[1].transform.SetSiblingIndex(midIndex);
                    cards[2].transform.SetSiblingIndex(backIndex);
                    break;
            }
        }
    }

    private IEnumerator NextCardEnum()
    {
        TapticManager.Impact(ImpactFeedback.Medium);

        if (numClick == 0)
        {
            GetCardIndexes();
            //Set animations rolling
            cards[0].GetComponent<Animator>().SetInteger("CardNum", 2);
            cards[1].GetComponent<Animator>().SetInteger("CardNum", 0);
            cards[2].GetComponent<Animator>().SetInteger("CardNum", 1);

            //Change Hierarchy
            yield return new WaitForSeconds(0.15f);
            SetCardIndexes();
            numClick++;
        }
        else if (numClick == 1)
        {
            GetCardIndexes();
            cards[0].GetComponent<Animator>().SetInteger("CardNum", 1);
            cards[1].GetComponent<Animator>().SetInteger("CardNum", 2);
            cards[2].GetComponent<Animator>().SetInteger("CardNum", 0);

            yield return new WaitForSeconds(0.15f);
            SetCardIndexes();
            numClick++;
        }
        else if (numClick == 2)
        {
            GetCardIndexes();
            cards[0].GetComponent<Animator>().SetInteger("CardNum", 0);
            cards[1].GetComponent<Animator>().SetInteger("CardNum", 1);
            cards[2].GetComponent<Animator>().SetInteger("CardNum", 2);

            yield return new WaitForSeconds(0.15f);
            SetCardIndexes();
            numClick = 0;
        }

        question_handler.NextCard();

        yield return new WaitForSeconds(0.6f);
        button_hider.SetActive(false);
    }

    public void LaunchModeScreen()
    {
        question_handler.ClearQuestions();
        StartCoroutine(BackToModes());
    }

    private IEnumerator BackToModes()
    {
        black_mask.Play("BlackFadeOut");
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene("Gamemodes", LoadSceneMode.Single);
    }

}