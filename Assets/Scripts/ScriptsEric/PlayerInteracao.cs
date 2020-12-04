using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteracao : MonoBehaviour
{
    public FixedButton interacao1;
    private GameObject objetoNpc;
    private GameObject objetoObjeto;

    private bool triggering = false;
    private bool estaEmMissao = false;
    private bool plantaColetada = false;

    public GameObject rangeText;
    public GameObject textoMissaoIDA;
    public GameObject textoMissaoVOLTA;
    public GameObject textoMissaoCOMPLETA;

    private void Update()
    {
        //Verifica se esta colidindo
        if (triggering)
        {
            rangeText.SetActive(true); 

        } else
        {
            rangeText.SetActive(false);
            rangeText.GetComponent<Text>().color = Color.black;
           
        }


        //Verifica se esta colidindo e interagindo com algum objeto
        if (triggering == true && (Input.GetKey(KeyCode.E) || interacao1.Pressed))
        {
            //Verifica contato com o NPC
            if(objetoNpc.tag == "NPC")
            {

                //Inicia uma missão
                if (estaEmMissao == false || plantaColetada == false)
                rangeText.GetComponent<Text>().color = Color.clear;
                textoMissaoIDA.SetActive(true);
                estaEmMissao = true;

                //Conclui uma missão
                if (plantaColetada)
                {
                    textoMissaoIDA.SetActive(false);
                    textoMissaoVOLTA.SetActive(false);
                    textoMissaoCOMPLETA.SetActive(true);

                    //Tentando tirar o MISSAO CUMPRIDA da tela

                    /*StartCoroutine(EsperarSegundos());

                    textoMissaoCOMPLETA.SetActive(false);
                    estaEmMissao = false;
                    plantaColetada = false;*/
                }
            }

            //Coleta um objeto
            if (objetoObjeto.tag == "Objeto")
            {
                rangeText.GetComponent<Text>().color = Color.clear;
                objetoObjeto.SetActive(false);
                textoMissaoIDA.SetActive(false);
                textoMissaoVOLTA.SetActive(true);
                plantaColetada = true;
            }

        }
    }

    private void OnTriggerEnter(Collider objeto)
    {
        if(objeto.tag == "NPC" || objeto.tag == "Objeto")
        {
            triggering = true;
            objetoNpc = objeto.gameObject;
            objetoObjeto = objeto.gameObject;
        }
    }

    private void OnTriggerExit(Collider objeto)
    {
        if (objeto.tag == "NPC" || objeto.tag == "Objeto")
        {
            triggering = false;
            objetoNpc = null;
            objetoObjeto = null;
        }
    }

    IEnumerator EsperarSegundos()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Ja esperei os 3 seg...");
    }

}
