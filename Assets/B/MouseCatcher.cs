using UnityEditor.UIElements;
using UnityEngine;
public class MouseCatcher : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject MouseObject;
    [SerializeField] GameObject localPlayer;
    LayerMask FloorLayer;

    void Start()
    {
        FloorLayer = LayerMask.GetMask("Default");
    }


    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции камеры в направлении мыши
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, FloorLayer)) // Запускаем Raycast на максимальную дистанцию 1000f
        {
            MouseObject.transform.position = hit.point; // Перемещаем объект в точку пересечения
            localPlayer.transform.LookAt(new Vector3(MouseObject.transform.position.x, 0.5f, MouseObject.transform.position.z));
        }
    }
}
