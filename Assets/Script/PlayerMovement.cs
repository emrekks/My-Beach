using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 touchStartPos;
    private bool isDragging = false;

    public Animator anim;
    public Camera cam;
    public float moveSpeed = 5f; // Karakterin hareket hızı
    public float rotationSpeed = 180f; // Yüzünü döndürme hızı
    private Vector3 offset;

    private void Start()
    {
        CalculateOffset();
    }

    void Update()
    {
        UpdateTouchInput();
        UpdateMovement();
    }

    void CalculateOffset()
    {
        offset = cam.transform.position - transform.position;
    }

    void UpdateTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector2 touchPos = touch.position;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touchPos);
                    break;

                case TouchPhase.Moved:
                    OnTouchMoved(touchPos);
                    break;

                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }
        }
    }

    void OnTouchBegan(Vector2 touchPos)
    {
        touchStartPos = touchPos;
        isDragging = true;
    }

    void OnTouchMoved(Vector2 touchPos)
    {
        if (isDragging)
        {
            Vector2 delta = touchPos - touchStartPos;
            Vector2 moveInput = delta.normalized;

            // Karakterin yüzünü döndür
            float angle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    void OnTouchEnded()
    {
        isDragging = false;
    }

    void UpdateMovement()
    {
        if (isDragging)
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            UpdateCameraPosition();
        }

        anim.SetBool("moving", isDragging);
    }

    void UpdateCameraPosition()
    {
        cam.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset;
    }
}
