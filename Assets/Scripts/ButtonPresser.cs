using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPresser : MonoBehaviour
{

    Camera cam;
    bool shootRay;
    
    private delegate void InputUpdateFunc();

    private InputUpdateFunc InputUpdate;
    
    // Start is called before the first frame update
    void Start()
    {

        if (GameManager.instance.inVR)
        {
            InputUpdate = VRInputs;
            //cam = FindObjectOfType<XRRig>().GetComponentInChildren<Camera>();
        }
        else
        {
            InputUpdate = MouseInputs;
            cam = FindObjectOfType<PlayerMouseCameraControl>().GetComponentInChildren<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MouseInputs();
        if(shootRay && CardManager.instance.canPressButtons)
        {
            ButtonRay();
        }
    }

    void MouseInputs()
    {
        /*if(UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame)
        {
            shootRay = true;
        }*/
    }
    
    void VRInputs()
    {
        /*if(UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame) // FIGURE THIS OUT
        {
            shootRay = true;
        }*/
    }

    void ButtonRay()
    {
        Ray buttonRay = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(buttonRay, out hit, 10f))
        {
            Debug.DrawRay(buttonRay.origin, buttonRay.direction, Color.magenta);
            Debug.Log(hit.collider.gameObject);
            if(hit.collider.tag == "Button")
            {
                hit.collider.gameObject.GetComponent<AlienButton>().Pressed();
            }
        }

        shootRay = false;
    }
}
