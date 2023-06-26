//File not using
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    //Speed (Can be edited in Unity)
    public float moveSpeed = 0.1f;
    Vector3 moveData = Vector3.zero;

    Vector3 movementInput = Vector3.zero;
    public float movementSpeed = 0.05f;

    Vector3 rotationInput = Vector3.zero;

    //Rotation Speed (Can be edited in Unity)
    Vector3 rotationData = Vector3.zero;
    public float rotationSpeed = 0.5f;

    void OnLook(InputValue value)

    {
        rotationInput.y = value.Get<Vector2>().x;
    }

    void OnMove(InputValue value)

    {
        movementInput = value.Get<Vector2>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            Destroy(collision.gameObject);
            GetComponent<Animator>().SetTrigger("isColliding");
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardDir = transform.forward;
        forwardDir *= movementInput.y;

        Vector3 rightDir = transform.right;
        rightDir *= movementInput.x;

        transform.position += (forwardDir + rightDir) * movementSpeed;

        transform.rotation = Quaternion.Euler(
            transform.rotation.eulerAngles
            + rotationInput * rotationSpeed);
    }

}
