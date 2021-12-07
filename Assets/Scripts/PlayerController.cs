using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private PlayerInput playerInput;
    private Rigidbody2D[] partRbs;
    private bool[] groundchecks;
    private PlayerPart[] playerParts;


    [SerializeField] private Vector2 jumpForce = new Vector2(10f, 10f);

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerParts = GetComponentsInChildren<PlayerPart>();
        partRbs = new Rigidbody2D[playerParts.Length];
        groundchecks = new bool[playerParts.Length];
        for (int i = 0; i < playerParts.Length; i++)
        {
            partRbs.SetValue(playerParts[i].GetComponent<Rigidbody2D>(), i);
        }
    }

    private void Update()
    {
        Vector3 direction = Vector3.zero;
        switch (playerInput.currentControlScheme)
        {
            case "KeyMouse":
                direction = GetMouseDirection();
                break;
            case "Controller":
                direction = playerInput.actions["Direction"].ReadValue<Vector2>();
                break;
            default:
                direction = GetMouseDirection();  ///since KeyMouse is also the default control scheme
                break;
        }
        direction.z = 0f;   ///no changing depth

        if (playerInput.actions["Jump"].triggered && IsAnyGrounded())
        {
            foreach (Rigidbody2D rb in partRbs)
            {
                rb.AddForce(jumpForce * direction, ForceMode2D.Impulse);
            }
        }
    }

    private Vector3 GetMouseDirection()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(playerInput.actions["Direction"].ReadValue<Vector2>());
        direction = Vector3.Normalize(direction - transform.position);
        return direction;
    }

    private bool IsAnyGrounded()
    {
        for (int i = 0; i < playerParts.Length; i++)
        {
            groundchecks[i] = playerParts[i].IsGrounded();
        }
        return groundchecks.Any(item => item == true);
    }
}
