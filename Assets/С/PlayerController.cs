using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float tiltAngle = 5f; // Угол наклона камеры при боковом движении
    public float tiltSpeed = 5f; // Скорость изменения наклона камеры
    public Transform cameraTransform;

    public float jumpForce = 8f; // Сила прыжка
    public float airControl = 2f; // Управление в воздухе
    public float airAcceleration = 1.5f; // Ускорение при стрейфах в воздухе
    public float gravity = 20f;

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;
    
    private float pitch = 0f;
    private float yaw = 0f;
    private bool isGrounded;

    private float currentTilt = 0f; // Текущий угол наклона камеры

    private void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Скрываем и фиксируем курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // Обработка вращения камеры
        RotateCamera();

        // Обработка перемещения персонажа
        MovePlayer();
        
        RayCast();
    }

    private void RotateCamera()
    {
        // Получаем движение мыши
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Обновляем углы поворота
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -90f, 90f); // Ограничиваем угол поворота вверх/вниз

        // Применяем вращение к игроку и камере
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, GetCameraTilt());
    }

    private float GetCameraTilt()
    {
        // Рассчитываем целевой угол наклона камеры в зависимости от направления движения
        float targetTilt = 0;
        if (Input.GetKey(KeyCode.A))
        {
            targetTilt = tiltAngle;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            targetTilt = -tiltAngle;
        }

        // Плавно интерполируем текущий угол наклона к целевому углу
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSpeed);

        return currentTilt;
    }

    private void MovePlayer()
    {
        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            // Получаем входные данные по осям
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            // Определяем направление движения
            Vector3 forwardMovement = transform.forward * moveZ;
            Vector3 rightMovement = transform.right * moveX;

            // Комбинируем движение по осям
            moveDirection = (forwardMovement + rightMovement).normalized * moveSpeed;

            // Обработка прыжков
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpForce;
            }
            else
            {
                verticalVelocity = -gravity * Time.deltaTime;
            }
        }
        else
        {
            // В воздухе мы используем физику для управления
            float moveX = Input.GetAxis("Horizontal") * airControl;
            float moveZ = Input.GetAxis("Vertical") * airControl;

            Vector3 airMovement = (transform.forward * moveZ + transform.right * moveX) * airAcceleration;

            moveDirection += airMovement * Time.deltaTime;

            verticalVelocity -= gravity * Time.deltaTime;
        }

        moveDirection.y = verticalVelocity;

        // Двигаем персонажа
        characterController.Move(moveDirection * Time.deltaTime);
    }
    
    void RayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2f, LayerMask.GetMask("Default")))
        {
            if (Input.GetMouseButtonDown(0))
                hit.collider.GetComponent<Cannon>().Fire();
            if(Input.GetMouseButtonDown(1))
                hit.collider.attachedRigidbody.AddForceAtPosition(cameraTransform.forward * 100, hit.point, ForceMode.Impulse);
        }
        Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 2f, Color.red);
    }
}
