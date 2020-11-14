using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComKnockBack : MonoBehaviour
{

    public FixedJoystick joystickEsquerdo;
    public FixedButton interacao1;
    public FixedButton interacao2;
    public FixedTouchField TouchField;
    public Camera cam;
    public Text energia;
    public int fome = 100;
    public float contador = 0.0f;
    public float velocidade = 5f;

    public Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    private Vector3 posicaoDoPlayer;

    //public GameObject player;

    /*public bool estaNoChao = true;
    bool kickado = false;*/


    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        var input = new Vector3(joystickEsquerdo.input.x, 0, joystickEsquerdo.input.y);
        var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * velocidade;

        posicaoDoPlayer = Rigidbody.transform.position;

        if (knockBackCounter <= 0)
        {
        Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
        transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

        cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 8);
        cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);


        if(fome <= 0)
        {
            energia.text = "Você está com muita fome!!!";
            velocidade = 2f;
        }
        else
        {
            contador += Time.deltaTime;
            if(contador >= 2.0f)
            {
                fome--;
                contador = 0.0f;
            }

            energia.text = "ENERGIA: " + fome + "%";
        }
    }

    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;

        posicaoDoPlayer = direction * knockBackForce;
        posicaoDoPlayer.y = knockBackForce;
    }

}
