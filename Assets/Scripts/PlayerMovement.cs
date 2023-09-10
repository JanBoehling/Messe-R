using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sprint;
    [SerializeField] float sneak;
    [SerializeField] Transform transformPlayer;
    private float halfHeight = 0.5f;
    private float baseHeight;
    private bool isSneaking;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        baseHeight = transformPlayer.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sneaking();
        }
    }

    private void MovePlayer()
    {
        audioSource.Play();

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime));

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed + sprint;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed - sprint;
        }
    }


    private void Sneaking()
    {
        Vector3 newScale = transformPlayer.localScale;

        if (!isSneaking)
        {
            newScale.y *= halfHeight;
            speed = speed - sneak;
        }
        else if(isSneaking && !Physics.Raycast(transform.position, Vector3.up, 1f))
        {
            newScale.y = baseHeight;
            speed = speed + sneak;
        }
        else return;

        isSneaking = !isSneaking;

        transform.localScale = newScale;
    }
}
