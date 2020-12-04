
using UnityEngine;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{

    public FixedJoystick joystickEsquerdo;
    public FixedButton interacao1;
    public FixedButton interacao2;
    public FixedTouchField TouchField;
    public Camera cam;
    public Text energia;

    public Rigidbody Rigidbody;

    protected float CameraAngleY;
    protected float CameraAngleSpeed = 0.1f;
    protected float CameraPosY;
    protected float CameraPosSpeed = 0.1f;

    /*public bool estaNoChao = true;
    bool kickado = false;*/


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var input = new Vector3(joystickEsquerdo.input.x, 0, joystickEsquerdo.input.y);
        var vel = Quaternion.AngleAxis(CameraAngleY + 180, Vector3.up) * input * 5f;

        /*if (estaNoChao)
        {*/
            Rigidbody.velocity = new Vector3(vel.x, Rigidbody.velocity.y, vel.z);
            transform.rotation = Quaternion.AngleAxis(CameraAngleY + Vector3.SignedAngle(Vector3.forward, input.normalized + Vector3.forward * 0.001f, Vector3.up), Vector3.up);
        //}

        CameraAngleY += TouchField.TouchDist.x * CameraAngleSpeed;

        cam.transform.position = transform.position + Quaternion.AngleAxis(CameraAngleY, Vector3.up) * new Vector3(0, 3, 8);
        cam.transform.rotation = Quaternion.LookRotation(transform.position + Vector3.up * 2f - cam.transform.position, Vector3.up);
    }

    /*private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            estaNoChao = true;
        } else
        {
            estaNoChao = false;
        }
    }*/
}
