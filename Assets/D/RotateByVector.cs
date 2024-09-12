using UnityEngine;

public class RotateByVector : MonoBehaviour
{
    private Vector3 previousPosition;

    void Start()
    {
        // Инициализируем предыдущую позицию текущей
        previousPosition = transform.position;
    }

    void Update()
    {
        // Получаем направление движения
        Vector3 direction = transform.position - previousPosition;

        // Если объект движется
        if (direction != Vector3.zero)
        {
            // Создаем поворот, смотрящий в направлении движения
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Применяем поворот к объекту
            transform.rotation = targetRotation;
        }

        // Обновляем предыдущую позицию
        previousPosition = transform.position;
    }
}
