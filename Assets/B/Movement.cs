using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f; // скорость перемещения

    [SerializeField]CharacterController controller; // компонент CC для куба
    [SerializeField]Vector3 newPosition;
    

    void Start()
    {
    }

    void Update()
    {
        // определяем направление движения по оси X, Y или Z
        float directionX = Input.GetAxis("Horizontal");
        float directionY = Input.GetAxis("Vertical");
        newPosition = new Vector3(directionX, 0 , directionY) * speed;
        controller.Move(newPosition * Time.deltaTime);
        
    }
}