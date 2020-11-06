using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    
  [Header("GAME USER INTERFACE")]
  public GUIManager guiManager;

  [Header("GAME MANAGER")]
  public GameManager gameManager;

  [Header("JOYSTICKS E BOTÕES DE INTERAÇÃO")]
  public FixedJoystick joystickEsquerdo;
  public FixedButton buttonInteract;
  public FixedButton buttonJump;
  public FixedTouchField touchField;

  [Header("CÂMERA DO JOGADOR")]
  public Camera cam;

  protected float CameraAngleY;
  protected float CameraAngleSpeed = 0.1f;
  protected float CameraPosY;
  protected float CameraPosSpeed = 0.1f;


  //public Text energia;
  
  [Header("PLAYER CONFIGS")]
  public Rigidbody Rigidbody; 
  public int fome = 100;
  public float contador = 0.0f;
  public float velocidade = 5f;
  public float knockBackForce;
  public float knockBackTime;
  private float knockBackCounter;
  private Vector3 posicaoDoPlayer;
  private Animator animator;
  public bool estaNoChao = false;
  public float force = 50f;
  public int maxHealth = 5;
  public int currentHealth;
  public float JumpForce = 5f;
  Vector3 input;
  Vector3 vel;
  bool kickado = false;

  void Start()
  {
    // ---- Initial Objects ----
    guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
    gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    joystickEsquerdo = GameObject.FindGameObjectWithTag("JoyStick").GetComponent<FixedJoystick>();
    buttonInteract = GameObject.FindGameObjectWithTag("ButtonInteract").GetComponent<FixedButton>();
    buttonJump = GameObject.FindGameObjectWithTag("ButtonJump").GetComponent<FixedButton>();
    
    Rigidbody = GetComponent<Rigidbody>();
    currentHealth = maxHealth;
    animator = GetComponent<Animator>();
  }

  void Update()
  {

#region MOVIMENTAÇÃO

  input = new Vector3(-joystickEsquerdo.input.x, 0, -joystickEsquerdo.input.y) * 
  Time.deltaTime * 40;
  vel = Quaternion.AngleAxis(CameraAngleY - 180, Vector3.up) * input * velocidade;


  //Se o seu objeto tem collider ou Rigidbody, nao utilizar transform.position e transform.rotation (Standart Asset utiliza da maneira correta)
  //Colocar os Inputs no Update e movimentar o personagem no Fixed Update


  CameraAngleY += touchField.TouchDist.x * CameraAngleSpeed;

  cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 8);
  cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);
  
#endregion
  if (kickado)
  {
    contador += Time.deltaTime;

    if(contador >= 2.0f)
    {
      kickado = false;
      contador = 0f;
    }
  }

  if(buttonJump.Pressed && estaNoChao == true || Input.GetKeyDown(KeyCode.Space) && estaNoChao == true)
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

  }

  public void FixedUpdate()
  {
    if (estaNoChao)
    {
      Rigidbody.velocity = new Vector3(-vel.x, Rigidbody.velocity.y, -vel.z);
      transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle
      (Vector3.forward, input.normalized - Vector3.forward * 0.001f, Vector3.up), Vector3.up);
    }

  }

  public void OnTriggerEnter(Collider collider)
  {

    if (collider.gameObject.tag == "Enemy" && estaNoChao && !kickado)
    {
      if(currentHealth < 1)
      {
        //SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Debug.Log("Game Over");
      } else
      {
        currentHealth -= 1;
        guiManager.ReduceLife(currentHealth);
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

    if(collider.gameObject.tag == "NPC")
    {
      //guiManager.NPCProximityEnter();
    }
  }

  private void OnTriggerStay(Collider other) {
    if(other.tag == "Objeto")
    {
      //guiManager.ItemProximityReport("Pressione para interagir!", true);

      if(Input.GetKey(KeyCode.E) || buttonInteract.Pressed)
      {
        GameObject.FindGameObjectWithTag("Objeto").SetActive(false);
      }
    }
  }

  private void OnTriggerExit(Collider other) 
  {
    if(other.tag == "Objeto")
    {
      //guiManager.ItemProximityReport("Pressione para interagir!", false);
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