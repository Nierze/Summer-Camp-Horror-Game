using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    private float velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = NewInputManager.Instance.GetPlayerMovement();


        /////////////////////////////////////////
        /// Player Rotation
        // float characterFacing = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        float characterFacing = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, characterFacing, ref velocity, 0.1f);
        transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);


        /////////////////////////////////////////
        /// Player Movement

        transform.position += new Vector3(movement.x, 0f, movement.y) * movementSpeed * Time.deltaTime;

        
    }
}
