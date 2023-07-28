using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTransform : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;
    public Vector3 scaleChange = new Vector3(2f, 2f, 2f);

    void Update()
    {
        // Move the GameObject
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Scale the GameObject
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localScale += scaleChange;
        }
    }
}

