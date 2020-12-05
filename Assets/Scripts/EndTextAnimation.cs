using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTextAnimation : MonoBehaviour
{
    Dictionary<int, string> finalizationText;

    int lineIndex;
    int stringIndex;
    float timer;
    string actualLIne;
    bool waitingNow;
    private void OnEnable() 
    {
        finalizationText = new Dictionary<int, string>();
        
        finalizationText.Add(0, "Parabéns! Você conseguiu sobreviver a todos os desafios");
        finalizationText.Add(1, "Porém, a nossa amiga Amazônia não está conseguindo vencer estes desafios...");

        finalizationText.Add(2, "Apesar de divertido, precisamos cuidar do meio ambiente, muito sério!");
        finalizationText.Add(3, "A Amazônia é a maior floresta úmida da TERRA, o nosso planeta");
        finalizationText.Add(4, "Ela possui a maior diversidade de plantas, peixes e mamíferos");

        lineIndex = 0;
        stringIndex = 0;
        timer = 0;
        actualLIne = "";
        waitingNow = false;

        gameObject.GetComponent<Text>().text = "";
        gameObject.GetComponent<Text>().fontSize = 65;
    }

    private void Update() 
    {

        timer += Time.deltaTime;

        if(gameObject.activeSelf == true)
        {
            if(timer >= 0.05f && lineIndex <= 4 && waitingNow == false)
            {

                if(!actualLIne.Equals(finalizationText[lineIndex]))
                    {

                        actualLIne += SetLineText();
                        gameObject.GetComponent<Text>().text += SetLineText();
                        stringIndex++;                    
                    }

                    else if (actualLIne.Equals(finalizationText[lineIndex]))
                    {
                        gameObject.GetComponent<Text>().text += "\n\n";
                        actualLIne = "";
                        lineIndex++;
                        stringIndex = 0;

                        if(lineIndex == 2)
                        {
                            waitingNow = true;
                            StartCoroutine("WaitNow");
                        }    
                    }
                timer -= timer;
            }
        }
        
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
