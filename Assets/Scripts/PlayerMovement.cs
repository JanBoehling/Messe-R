using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float sprint;
    [SerializeField] float sneak;
    [SerializeField] Transform transformPlayer;
    private float halfHeight = 0.5f;
    private float baseHeight;
    private bool isSneaking;

    private float initialStamina = 7f;
    public float currentStamina;
    private float staminaIncreaseSpeed = 2f;

    public GameObject StaminaContainer = null;
    public Image StaminaBar = null;

    private float moveSpeed;

    private AudioSource audioSource;

    private void Awake()
    {
        moveSpeed = speed;
        currentStamina = initialStamina;
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
        UpdateUIVisibility();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Sneaking();
        }
    }

    private void MovePlayer()
    {
        float staminaDecreaseRate = 0;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (currentStamina > 0)
            {
                staminaDecreaseRate = Time.deltaTime * 3;
                moveSpeed = sprint;
            }
            else
            {
                moveSpeed = speed;
            }
        }
        else
        {
            if (currentStamina < initialStamina)
            {
                currentStamina += Time.deltaTime * staminaIncreaseSpeed;
            }
        }

        currentStamina = Mathf.Clamp(currentStamina - staminaDecreaseRate, 0, initialStamina);

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = speed;
        }

        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime));
    }

    private void UpdateUIVisibility()
    {
        if (StaminaContainer != null)
        {
            StaminaContainer.SetActive(currentStamina < initialStamina);
        }

        if (StaminaBar != null)
        {
            float value = Mathf.Clamp01(currentStamina / initialStamina);
            StaminaBar.fillAmount = value;
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
