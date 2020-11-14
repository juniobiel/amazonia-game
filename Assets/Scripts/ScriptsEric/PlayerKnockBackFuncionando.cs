using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerKnockBackFuncionando : MonoBehaviour
{

    public FixedJoystick joystickEsquerdo;
    public FixedButton interacao2;
    public FixedTouchField TouchField;
    public Camera cam;
    public Text vida;

    public Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    private Animator animator;
    
    Vector3 input;
    Vector3 vel;

    public bool estaNoChao = false;
    public float force = 50f;
    public float velocidade = 8f;

    public float contador = 0.0f;
    bool kickado = false;

    public int maxHealth = 100;
    public int currentHealth;

    public float JumpForce = 5f;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        //estaNoChao = false;

    }

    void Update()
    {
        input = new Vector3(-joystickEsquerdo.input.x, 0, -joystickEsquerdo.input.y) * Time.deltaTime * 40;
        vel = Quaternion.AngleAxis(CameraAngleY - 180, Vector3.up) * input * velocidade;


        //Se o seu objeto tem collider ou Rigidbody, nao utilizar transform.position e transform.rotation (Standart Asset utiliza da maneira correta)
        //Colocar os Inputs no Update e movimentar o personagem no Fixed Update


        CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

        cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 8);
        cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);

        if (kickado)
        {
            contador += Time.deltaTime;

            if(contador >= 2.0f)
            {
                kickado = false;
                contador = 0f;
            }
        }

        if (currentHealth <= 0)
        {
            //Fazer o player morrer / desligar os constraints do rigdibody / 
        }

        if(interacao2.Pressed && estaNoChao == true || Input.GetKeyDown(KeyCode.Space) && estaNoChao == true)
        {
            Debug.Log("apertou!");
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpForce, Rigidbody.velocity.z);
            estaNoChao = false;
            animator.SetBool("Parado", false);
            animator.SetBool("Correndo", false);
            animator.SetBool("Pulando", true);
        }

        if (estaNoChao == true)
        {
            animator.SetBool("Pulando", false);
        }

        if(Rigidbody.velocity.magnitude > 0.5f)
        {
            animator.SetBool("Parado", false);
            animator.SetBool("Correndo", true);
        }
        else
        {
            animator.SetBool("Parado", true);
            animator.SetBool("Correndo", false);
        }

        vida.text = "Vida: " + currentHealth;

    }

    //RayCast para fazer a verificação se esta no chao
    public void FixedUpdate()
    {
        if (estaNoChao)
        {
            Rigidbody.velocity = new Vector3(-vel.x, Rigidbody.velocity.y, -vel.z);
            transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized - Vector3.forward * 0.001f, Vector3.up), Vector3.up);
        }

    }


    public void OnTriggerEnter(Collider collider)
    {
        /*if (collision.transform.tag == "Ground")
        {
            estaNoChao = true;
        }*/

        if (collider.gameObject.tag == "Enemy" && estaNoChao && !kickado)
        {
            if(currentHealth <= 25)
            {
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            } else
            {
                currentHealth -= 25;
            }

            kickado = true;
            Rigidbody.velocity = Vector3.zero;
            estaNoChao = false;
            Rigidbody.AddForce(transform.up * force * 3);
            transform.Rotate(transform.up * 180);
            Rigidbody.AddForce(transform.forward * force * 5);
        }

        if (collider.gameObject.tag == "Top")
        {
            Rigidbody.AddForce(transform.up * force * 5);
            Rigidbody.AddForce(transform.forward * force * 5);
        }
    }

    public void OnCollisionEnter(Collision other)
    {

        if (other.transform.tag == "Ground")
        {
            estaNoChao = true;
        }
    }



}