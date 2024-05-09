using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform[] backgrounds; // Arreglo que contiene las transformaciones de las capas de fondo
    [SerializeField] private float[] parallaxScales; // Escala de parallax para cada capa de fondo
    [SerializeField] private float smoothing = 1f; // Suavizado del efecto de parallax
    private Transform camTransform; // Transform de la c�mara
    private Vector3 previousCamPos; // Posici�n anterior de la c�mara

    private void Awake()
    {
        camTransform = Camera.main.transform; // Obtiene el transform de la c�mara principal
    }

    private void Start()
    {
        previousCamPos = camTransform.position; // Guarda la posici�n inicial de la c�mara
    }

    private void Update()
    {
        // Para cada capa de fondo
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Calcula el parallax entre la posici�n actual de la c�mara y la posici�n anterior
            float parallax = (previousCamPos.x - camTransform.position.x) * parallaxScales[i];

            // Calcula la posici�n objetivo para la capa de fondo
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Crea una posici�n objetivo basada en la posici�n actual de la capa de fondo y la posici�n objetivo en el eje x
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Interpola suavemente hacia la posici�n objetivo
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // Actualiza la posici�n anterior de la c�mara al final del frame
        previousCamPos = camTransform.position;
    }
}
