using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] float moveDelay = 0.2f; // Hareket gecikmesi
    [SerializeField] float lerpSpeed = 5f; // Lerp hýzýný kontrol eder
    private float moveTimer;

    private Vector2Int direction = Vector2Int.right;
    private Vector3 rotation = Vector3.zero;

    private void Awake()
    {
        moveTimer = moveDelay;
    }

    private void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        if (moveTimer <= 0)
        {
            Move();
            moveTimer = moveDelay;
        }
    }

    private void Move()
    {
        float x = transform.position.x + direction.x;
        float y = transform.position.y + direction.y;

        // Yalnýzca buçuklu deðerlere yuvarlayýn
        x = Mathf.Round(x * 2) / 2;
        y = Mathf.Round(y * 2) / 2;

        transform.DOMove(new Vector2 (x, y),0.2f).SetEase(Ease.Linear);
        transform.rotation = Quaternion.Euler(rotation);
    }
    public bool Occupies(int x, int y)
    {
        //foreach (Transform segment in segments)
        //{
        //    if (Mathf.RoundToInt(segment.position.x) == x &&
        //        Mathf.RoundToInt(segment.position.y) == y)
        //    {
        //        return true;
        //    }
        //}
        return false;
    }

    public void ChangeDirection(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 inputDirection = context.ReadValue<Vector2>();

            switch (inputDirection)
            {
                case Vector2 v when v == Vector2.up && direction != Vector2Int.down:
                    rotation = new Vector3(0, 0, 90);
                    direction = Vector2Int.up;
                    break;

                case Vector2 v when v == Vector2.down && direction != Vector2Int.up:
                    rotation = new Vector3(0, 0, -90);
                    direction = Vector2Int.down;
                    break;

                case Vector2 v when v == Vector2.left && direction != Vector2Int.right:
                    rotation = new Vector3(0, 0, 180);
                    direction = Vector2Int.left;
                    break;

                case Vector2 v when v == Vector2.right && direction != Vector2Int.left:
                    rotation = new Vector3(0, 0, 0);
                    direction = Vector2Int.right;
                    break;

                default:
                    break;
            }
        }
    }
}
