using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Rotor : MonoBehaviour
{
    [SerializeField] int blades;
    [SerializeField] int subdivision;
    [SerializeField] float bladeLength;
    [SerializeField] float bladeWidth;
    [SerializeField] float bladeThickness; 
    [SerializeField] float bladeDensity = 2000f; // kg/m^3
    [SerializeField] float rpm;

    [SerializeField] float airDensity = 1.225f; // kg/m^3
    [SerializeField] float liftCoefficient = 0.5f;

    [SerializeField] Transform rotorShaft;

    float angleBetweenBlades;
    float bladeAngle;
    float spacing;

    void Start()
    {
        angleBetweenBlades = 360 / blades;
        bladeAngle = 0f;
        spacing = bladeLength / (subdivision - 1);
    }

    void FixedUpdate()
    {
        bladeAngle = 0f;

        Vector3 originalVector = rotorShaft.forward;
        Vector3 axisOfRotation = rotorShaft.up; // Rotate around the y-axis

        for (int j = 0; j < blades; j++)
        {
            bladeAngle += angleBetweenBlades; // Rotate by 90 degrees
            
            // Create a rotation quaternion
            Quaternion rotation = Quaternion.AngleAxis(bladeAngle, axisOfRotation);
            Vector3 directionOfBlade = rotation * originalVector;
            Debug.DrawRay(rotorShaft.position, directionOfBlade * bladeLength, Color.green);

            for (int i = 0; i < subdivision; i++)
            {
                Vector3 pointPosition = rotorShaft.position + directionOfBlade * spacing * i;
                float distanceFromCenter = Vector3.Distance(pointPosition, rotorShaft.position);
                Debug.DrawRay(pointPosition, rotorShaft.up * GetLift(distanceFromCenter) * Time.deltaTime, Color.blue) ;
            }
        }
    }

    private float GetLift(float distanceFromCenter)
    {
        float area = (bladeLength / subdivision) * bladeWidth;
        float volume = bladeLength * bladeWidth * bladeThickness;
        float mass = GetWeight(volume,bladeDensity);

        float angularVelocity = (2 * Mathf.PI * (rpm / 60)) * distanceFromCenter;
        float lift = liftCoefficient * airDensity * area * (angularVelocity * angularVelocity);

        return lift;
    }

    void OnYaw()
    {

    }

    public float GetWeight(float volume, float density)
    {
        float gravity = 9.81f;
        float weight = volume * density * gravity;
        return weight;
    }
}
