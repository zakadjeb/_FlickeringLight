using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChangeColor : MonoBehaviour
{
    public List<Color> ColorList;
    public List<int> FreqsList;
    public int perTrial;
    public int Duration;
    private Color c1, c2, c3, c4, c5, c6;
    private Renderer r;
    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<Renderer>();
        List<int> tempList = new List<int>();

        c1 = new Color(255/255f, 173/255f, 173/255f, 1f);   // Carefully selected colors from www.coolors.com
        c2 = new Color(255/255f, 184/255f, 171/255f, 1f);   // Carefully selected colors from www.coolors.com
        c3 = new Color(255/255f, 204/255f, 167/255f, 1f);   // Carefully selected colors from www.coolors.com
        c4 = new Color(255/255f, 225/255f, 170/255f, 1f);   // Carefully selected colors from www.coolors.com
        c5 = new Color(254/255f, 235/255f, 174/255f, 1f);   // Carefully selected colors from www.coolors.com
        c6 = new Color(253/255f, 255/255f, 182/255f, 1f);   // Carefully selected colors from www.coolors.com

        for (int i = 0; 0 < perTrial; i++) {
            ColorList.Add(c1); // First color, c1
            ColorList.Add(c2); // Second color, c2
            ColorList.Add(c3); // Third color, c3
            ColorList = ColorList.OrderBy(x => UnityEngine.Random.value).ToList();
        }

        for (int i = 0; i <= ColorList.Count; i ++) {
            if (ColorList[i] == c1) { FreqsList.Add(10); } // Start at 10 Hz
            if (ColorList[i] == c2) { FreqsList.Add(25); } // +15 Hz
            if (ColorList[i] == c3) { FreqsList.Add(40); } // +15 Hz
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) { 
            StopAllCoroutines();           
            StartCoroutine(ColorFlickr());
        }

        if (Input.GetKeyDown(KeyCode.A)) {            
            StopAllCoroutines();
            StartCoroutine(BlackFlickr());
        }
        // To do:
        // Make the colors go backwards as well (reverse list) for a few times. Then finally switch over to a flickering
        // between gray and white. Hopefully, the Hertz will determine the experience of colors.
    }

    IEnumerator ColorFlickr () {
        for (int j = 0; j < ColorList.Count; j++) {
            for (int i = 0; i < Duration/(1f/FreqsList[j]); i++) {
                r.material.SetColor("_Color", ColorList[j]); // From color list
                yield return new WaitForSeconds((1f/FreqsList[j])/2);
                r.material.SetColor("_Color", new Color(.5f,.5f,.5f,1f)); // Gray color
                yield return new WaitForSeconds((1f/FreqsList[j])/2);
            }
        }
    }

    IEnumerator BlackFlickr () {
        for (int j = 0; j < ColorList.Count; j++) {
            for (int i = 0; i < Duration/(1f/FreqsList[j]); i++) {
                r.material.SetColor("_Color", new Color(0f,0f,0f,1f)); // Black color
                yield return new WaitForSeconds((1f/FreqsList[j])/2);
                r.material.SetColor("_Color", new Color(.5f,.5f,.5f,1f)); // Gray color
                yield return new WaitForSeconds((1f/FreqsList[j])/2);
            }
        }
    }

}
