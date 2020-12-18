using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTextAnimation : MonoBehaviour
{

    public GameObject CreditUI;

    public GameObject EndUI;

    public GameObject TextUI;
    
    public GameObject NPCNameUI;

    public GameObject CreditText;

    [Header("FADE")]
    public float FadeRate;
    public Image image;
    public float targetAlpha;

    Dictionary<int, string> finalizationText;
    int lineIndex;
    int stringIndex;
    float timer;
    string actualLIne;
    bool waitingNow;
    Text txtCredits;
    float alphaAux;

    private void OnEnable()
    {
        targetAlpha = image.color.a;

        finalizationText = new Dictionary<int, string>();
        
        finalizationText.Add(0, "Parabéns! Você conseguiu sobreviver a todos os desafios do nosso jogo!");
        finalizationText.Add(1, "Porém, fora do jogo, a nossa amiga Amazônia não está conseguindo vencer estes problemas...");

        finalizationText.Add(2, "Temos que cuidar muito bem dela pois ela é muito importantes para todos nós!");
        finalizationText.Add(3, "A Amazônia é a maior floresta úmida do nosso planeta!");
        finalizationText.Add(4, "Ela possui a maior diversidade de plantas, mamíferos e peixes de água doce!");

        finalizationText.Add(5, "Então... Queremos deixar uma mensagem para você...");

        lineIndex = 0;
        stringIndex = 0;
        timer = 0;
        actualLIne = "";
        waitingNow = false;

        gameObject.GetComponent<Text>().text = "";
        gameObject.GetComponent<Text>().fontSize = 65;

        txtCredits = TextUI.GetComponent<Text>();
    }

    private void Update() 
    {

        Color curColor = image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            image.color = curColor;
        }

        timer += Time.deltaTime;

        if(gameObject.activeSelf == true)
        {
            if(timer >= 0.07f && lineIndex <= 5 && waitingNow == false)
            {

                if(!actualLIne.Equals(finalizationText[lineIndex]))
                    {

                        actualLIne += SetLineText();
                        gameObject.GetComponent<Text>().text += SetLineText();
                        stringIndex++;                    
                    }

                    else if (actualLIne.Equals(finalizationText[lineIndex]))
                    {
                        gameObject.GetComponent<Text>().text += "\n";
                        actualLIne = "";
                        lineIndex++;
                        stringIndex = 0;

                        if(lineIndex == 2 || lineIndex == 5)
                        {
                            waitingNow = true;
                            StartCoroutine("WaitNow");
                        }    
                    }
                timer -= timer;
            }
            else if(lineIndex >= 6 && timer >= 0.07f)
            {
                CreditUI.SetActive(true);
                FadeIn();

                if (curColor.a >= 0.998f)
                {
                    EndUI.SetActive(false);
                    CreditText.SetActive(true);
                }
            }
        }
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }


    char SetLineText()
    {
       return finalizationText[lineIndex][stringIndex];
    }

    IEnumerator WaitNow()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<Text>().text = "";
        waitingNow = false;        
    }

    
}
