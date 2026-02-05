using System;
using System.Collections;  // Ensure this is imported
using UnityEngine;

public class RotateOnDrag : MonoBehaviour
{
    //public Vector3 lastMousePosition;
    //private Quaternion targetRotation; // Desired rotation
    //[SerializeField] private float rotationSpeed = 10f; 
    //[SerializeField] private float smoothSpeed = 2f;
    //[SerializeField] private float returnSpeed = 2f;
    //public ControladorCarta _controlador;
    //public bool _returning;
    //public Vector3 mouseDelta;
    //public Vector3 currentMousePosition;

    //private float clickStartTime = 0f;
    //private bool isHolding = false;
    //private bool _changedCard = false;
    //void Start()
    //{
    //    // Initialize target rotation as the object's starting rotation
    //    targetRotation = transform.rotation;
    //    _controlador = _controlador.GetComponent<ControladorCarta>();
    //}




    //void Update()
    //{
    //    // Register the initial mouse position when clicking down
    //    if (Input.GetMouseButtonDown(0) && !_returning)
    //    {
            
    //        lastMousePosition = Input.mousePosition;
    //        _controlador._holdingCard = true;

    //        clickStartTime = Time.time; // Record the time when the click starts
    //        isHolding = true;
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        targetRotation = Quaternion.Euler(0, 180, 0);
    //        _returning = true;  // Set returning to true so we don't start multiple coroutines
    //        _controlador._holdingCard = false;
    //        StartCoroutine(ReturnNumerator());
    //        _changedCard = false;

    //        if (Time.time - clickStartTime <= 0.25f)
    //        {
    //            ShortClickAction(); // Short click action
    //        }
    //        else
    //        {
            
    //        }
    //    }

      
    //    // Update rotation when the left mouse button is held down
    //    if (Input.GetMouseButton(0) && !_returning){                 

    //        isHolding = false; // Reset holding state

    //        if (Time.time - clickStartTime >= 0.25f)
    //        {
    //            RotateObject();
    //        }
   
    //    }

    //    if (!_controlador._holdingCard)
    //    {
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 180, 0), returnSpeed * Time.deltaTime);

    //    }       
    //}




    //private void RotateObject()
    //{
    //     currentMousePosition = Input.mousePosition;

    //    // Calculate mouse movement
    //     mouseDelta = currentMousePosition - lastMousePosition;

    //    // Calculate the rotation delta
    //    float rotationAmountX = mouseDelta.x * rotationSpeed * Time.deltaTime;
    //    float rotationAmountY = mouseDelta.y * (rotationSpeed / 3) * Time.deltaTime;

    //    // Update the target rotation based on mouse movement
    //    targetRotation = Quaternion.Euler(rotationAmountY, -rotationAmountX, 0) * targetRotation;

    //    // Update the last mouse position for the next frame
    //    lastMousePosition = currentMousePosition;

    //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    //}

    //// This coroutine will run when the mouse button is pressed
    //public IEnumerator ReturnNumerator()
    //{


    //    yield return new WaitForSeconds(0.5f);  // Wait for 0.5 seconds
    //    while(transform.eulerAngles.x != 0 && transform.eulerAngles.y != 180)
    //    {
    //        yield return null;
    //    }

    //    _returning = false;  // Set returning to false after the delay
    //}


    //private void ShortClickAction()
    //{
    //    if (!_changedCard)
    //    {       
    //        StartCoroutine(_controlador.NewCardNumerator());
    //        _changedCard = true;
    //    }    
   
    //}

  

}