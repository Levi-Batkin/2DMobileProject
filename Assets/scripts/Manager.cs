using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Manager : MonoBehaviour
{

    public float itemCollected = 0f;
    public TMP_Text text, starveAmount, highScore;
    public GameObject starvedtext;
    public float starvetimer = 10f, starveMultiplier = 1f, starveAddedMult = 0.2f;
    int starveMult;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("highscore"))
        {
            PlayerPrefs.SetFloat("highscore", 0f);
            highScore.text = "High Score: 0";
        }
        else
        {
            highScore.text = "High Score: "+ PlayerPrefs.GetFloat("highscore").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Cheese Collected: " + itemCollected.ToString();
        starveAmount.text = "Time Till Starvation: " + starvetimer.ToString("0.0") + "s";
        if(PlayerPrefs.GetFloat("highscore") < itemCollected) {
            highScore.text = "High Score: "+ itemCollected;
        }
        if(starvetimer <= 0)
        {
            if(PlayerPrefs.GetFloat("highscore") < itemCollected)
            {
                PlayerPrefs.SetFloat("highscore", itemCollected);
                highScore.text = "High Score: "+ PlayerPrefs.GetFloat("highscore").ToString();
            }
            itemCollected = 0f;
            GameObject death = Instantiate(starvedtext, new Vector2(0f,0f), Quaternion.identity);
            Destroy(death, 1.5f);
            starveMultiplier = 1f;
            starvetimer = 10f;
        }
        else
        {
            starvetimer -= Time.deltaTime * starveMultiplier;
    
        }
        if(int.TryParse((itemCollected / 5).ToString(), out starveMult) && itemCollected != 0)
        {           
            starveMultiplier = 1f + (starveAddedMult * starveMult); 
        }
    }

}
