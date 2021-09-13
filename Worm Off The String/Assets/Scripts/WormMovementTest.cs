using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WormMovementTest : MonoBehaviour
{
    
    public enum Joystick
    {
        P1,
        P2,
        P3,
        P4
    }


    public Joystick player;

    public List<Rigidbody> parts;
    private Rigidbody rgd;

    [SerializeField]
    float force = 10f;

    [SerializeField]
    float flightForce = 5f;

    [SerializeField]
    float clamp = 15f;

    //Unused
    [SerializeField]
    bool canGoUp = true;

    [SerializeField]
    LayerMask groundLayer;

    [SerializeField]
    float flyingEnergy = 100f;

    [SerializeField]
    float flyingEnergyDrain = 0.5f;

    [SerializeField]
    float flyingEnergyRegen = 0.1f;

    [SerializeField]
    float flyingEnergyMax = 100f;

    //Unused
    void isTouchingGround()
    {
        foreach(Rigidbody part in parts)
        {
            if(Physics.CheckSphere(part.gameObject.transform.position, 1.5f, groundLayer))
            {
                canGoUp = true;
                return;
            }
        }
        canGoUp = false;
    }

    Vector3 Fly(Vector3 vec)
    {
        if(flyingEnergy > 0)
        {
            flyingEnergy -= flyingEnergyDrain;
            vec += (new Vector3(0, 1.0f, 0) * flightForce);
            return vec;
        }
        else
        {

            return vec;
        }
    }

    void Move()
    {
        

        float horizontal = Input.GetAxis(player + "Horizontal");
        float vertical = Input.GetAxis(player + "Vertical");


        //Camera.main.transform.
        Vector3 direction = Vector3.zero;

        //direction += (new Vector3(horizontal, 0f, vertical));
        if(player == Joystick.P1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction += new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += new Vector3(Camera.main.transform.right.x * -1f, 0f, Camera.main.transform.right.z * -1f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += new Vector3(Camera.main.transform.forward.x *-1f, 0f, Camera.main.transform.forward.z * -1f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                direction = Fly(direction);
            }
            else
            {
                if (flyingEnergy < flyingEnergyMax)
                {
                    flyingEnergy += flyingEnergyRegen;
                }
                else if (flyingEnergy > flyingEnergyMax)
                {
                    flyingEnergy = flyingEnergyMax;
                }
            }
        }

        if(player == Joystick.P2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction += new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += new Vector3(Camera.main.transform.right.x * -1f, 0f, Camera.main.transform.right.z * -1f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += new Vector3(Camera.main.transform.forward.x * -1f, 0f, Camera.main.transform.forward.z * -1f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += new Vector3(Camera.main.transform.right.x, 0f, Camera.main.transform.right.z);
            }
            if (Input.GetKey(KeyCode.RightControl))
            {
                direction = Fly(direction);
            }
            else
            {
                if(flyingEnergy < flyingEnergyMax)
                {
                    flyingEnergy += flyingEnergyRegen;
                }
                else if (flyingEnergy > flyingEnergyMax)
                {
                    flyingEnergy = flyingEnergyMax;
                }
            }
        }


        
        
        Vector3 vel = direction * force;
        Vector3.ClampMagnitude(vel, 15f);
        rgd.AddForce(vel);
    }

    void UpdateText()
    {
        GameObject.Find(player + "FuelText").GetComponent<TextMeshProUGUI>().text = "Fuel: " + (int)flyingEnergy;
    }

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void FixedUpdate()
    {
        isTouchingGround();
        Move();
    }
}
