using UnityEngine;
using UnityEngine.UIElements;

public class Rotor : MonoBehaviour
{
    [SerializeField] int blades;
    [SerializeField] int subdivision;
    [SerializeField] float bladeLength;
    [SerializeField] float bladeWidth;
    [SerializeField] float rpm;

    [SerializeField] float airDensity = 1.225f; // kg/m^3
    [SerializeField] float liftCoefficient = 0.5f;

    [SerializeField] Transform rotorShaft;

    void Start()
    {

    }

    void Update()
    {
        float angleBetweenBlades = 360 / blades;
        float bladeAngle = 0f;
        float spacing = bladeLength / (subdivision - 1);


        // Example usage:
        Vector3 originalVector = rotorShaft.forward;
        Vector3 axisOfRotation = rotorShaft.up; // Rotate around the y-axis


        for (int j = 0; j < blades; j++)
        {
            bladeAngle += angleBetweenBlades; // Rotate by 90 degrees
            
            // Create a rotation quaternion
            Quaternion rotation = Quaternion.AngleAxis(bladeAngle, axisOfRotation);
            Vector3 directionOfBlade = rotation * originalVector;

            for (int i = 0; i < subdivision; i++)
            {
                Vector3 pointPosition = rotorShaft.position + directionOfBlade * spacing * i;
                Debug.DrawRay(pointPosition, rotorShaft.up * 0.1f, Color.blue); // Changez Vector3.up en la direction que vous souhaitez afficher le point
            }


        }
    }
}
