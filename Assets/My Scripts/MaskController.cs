using UnityEngine;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    private Image mask;
    private Color color;
    private Transform[] grids = new Transform[2];
    private Transform tinyGrid;
    private float tAlpha, fAlpha;

    // Start is called before the first frame update
    void Start()
    {
        mask = this.GetComponent<Image>();
        color.a = 0;

        grids[0] = GameObject.FindGameObjectWithTag("BigGrid").GetComponent<Transform>();
        grids[1] = GameObject.FindGameObjectWithTag("SmallGrid").GetComponent<Transform>();
        tinyGrid = GameObject.FindGameObjectWithTag("TinyGrid").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAlphaChildren();
    }

    private void ChangeAlphaChildren()
    {
        foreach (Transform grid in grids)
        {
            foreach (Transform element in grid)
            {
                color = element.GetComponent<Image>().color;

                fAlpha = color.a;
                tAlpha = (-Mathf.Round(tinyGrid.position.y) - 1) / 4;
                color.a = Mathf.Lerp(fAlpha, tAlpha, 0.3f);

                element.GetComponent<Image>().color = color;
                Debug.Log(Mathf.Round(tinyGrid.position.y) + "\t\t" + color.a);

                if (color.a > 0.2f)
                {
                    mask.raycastTarget = false;
                }
                else
                {
                    mask.raycastTarget = true;
                }
            }
        }
    }
}