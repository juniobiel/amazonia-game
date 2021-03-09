using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScript : MonoBehaviour
{

    Dictionary<int, string> creditDictionary;
    Dictionary<int, string> devDictionary;

    int stringIndex;
    string actualLIne;
    int lineIndex;
    float timer;
    bool finishAnimation1;
    bool waitingNow;

    private void OnEnable()
    {
        timer = 0;
        finishAnimation1 = false;
        stringIndex = 0;
        actualLIne = "";
        lineIndex = 0;
        waitingNow = false;

        creditDictionary = new Dictionary<int, string>();
        devDictionary = new Dictionary<int, string>();

        creditDictionary.Add(0, "Apesar do nosso jogo ser divertido...");
        creditDictionary.Add(1, "... destruir o meio-ambiente não é !");
        creditDictionary.Add(2, "#salveaamazonia");

        devDictionary.Add(0, "Desenvolvedores:");
        devDictionary.Add(1, "Éric Soares Fernandes");
        devDictionary.Add(2, "Gabriel Júnio Fernandes Pereira");
        devDictionary.Add(3, "Leandro Ribeiro de Pádua");

        gameObject.GetComponent<Text>().text = "";

    }

    void Update()
    {
        timer += Time.deltaTime;

        if (gameObject.activeSelf == true && finishAnimation1 == false && waitingNow == false)
        {
            if (timer >= 0.07f)
            {
                if (!gameObject.GetComponent<Text>().text.Equals(creditDictionary[lineIndex])) 
                {    
                    gameObject.GetComponent<Text>().text += creditDictionary[lineIndex][stringIndex];
                    stringIndex++;
                } else if (gameObject.GetComponent<Text>().text.Equals(creditDictionary[lineIndex]))
                {
                    lineIndex++;

                    if(lineIndex >= 3)
                    {
                        finishAnimation1 = true;
                        lineIndex = 0;
                    }

                    stringIndex = 0;
                    waitingNow = true;
                    StartCoroutine("WaitNow");
                }

                timer -= timer;

            }
        }


        if (gameObject.activeSelf == true && finishAnimation1 == true && waitingNow == false)
        {
            if (timer >= 0.07f && lineIndex <= 3)
            {

                if (!actualLIne.Equals(devDictionary[lineIndex]))
                {

                    actualLIne += SetLineText();
                    gameObject.GetComponent<Text>().text += SetLineText();
                    stringIndex++;
                }

                else if (actualLIne.Equals(devDictionary[lineIndex]))
                {
                    if (lineIndex <= 2)
                    {
                        gameObject.GetComponent<Text>().text += "\n";
                    }
                    
                    actualLIne = "";
                    lineIndex++;
                    stringIndex = 0;
                }
                timer -= timer;
            }
        }
    }

    char SetLineText()
    {
        return devDictionary[lineIndex][stringIndex];
    }
    IEnumerator WaitNow()
    {
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<Text>().text = "";
        waitingNow = false;
    }
}
