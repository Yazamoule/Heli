using UnityEngine;
using UnityEngine.InputSystem;

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

    public float pitch = 0f;
    public float roll = 0f;

    void Start()
    {
        angleBetweenBlades = 360 / blades;
        bladeAngle = 0f;
        spacing = bladeLength / (subdivision - 1);
    }

    void FixedUpdate()
    {
        DrawCardinal();

        bladeAngle = 0f;

        Vector3 originalVector = rotorShaft.forward;
        Vector3 axisOfRotation = rotorShaft.up; // Rotate around the y-axis

        for (int j = 0; j < blades; j++)
        {
            bladeAngle += angleBetweenBlades; // Rotate by 90 degrees

            // Create a rotation quaternion

            Quaternion rotation = Quaternion.AngleAxis(bladeAngle, axisOfRotation);
            Vector3 directionOfBlade = rotation * originalVector;
            Debug.DrawRay(rotorShaft.position, directionOfBlade * bladeLength, Color.white);

            for (int i = 0; i < subdivision; i++)
            {
                Vector3 pointPosition = rotorShaft.position + directionOfBlade * spacing * i;
                float distanceFromCenter = Vector3.Distance(pointPosition, rotorShaft.position);
                Debug.DrawRay(pointPosition, rotorShaft.up * GetLift(distanceFromCenter) * Time.deltaTime, Color.magenta);
            }
        }
    }

    private float GetLift(float distanceFromCenter)
    {
        float area = (bladeLength / subdivision) * bladeWidth;
        float volume = bladeLength * bladeWidth * bladeThickness;
        float mass = GetWeight(volume, bladeDensity);

        float angularVelocity = (2 * Mathf.PI * (rpm / 60)) * distanceFromCenter;
        float lift = liftCoefficient * airDensity * area * (angularVelocity * angularVelocity);

        return lift;
    }


    private float GetWeight(float volume, float density)
    {
        float gravity = 9.81f;
        float weight = volume * density * gravity;
        return weight;
    }
    private void DrawCardinal()
    {
        Quaternion rotationPitchPlus = Quaternion.AngleAxis(0f, rotorShaft.up);
        Vector3 directionPitchPlus = rotationPitchPlus * rotorShaft.forward;
        Debug.DrawRay(rotorShaft.position, directionPitchPlus * pitch, Color.green) ;

        Quaternion rotationPitchMinus = Quaternion.AngleAxis(180f, rotorShaft.up);
        Vector3 directionPitchMinus = rotationPitchMinus * rotorShaft.forward;
        Debug.DrawRay(rotorShaft.position, directionPitchMinus * pitch, Color.yellow);

        Quaternion rotationRollPlus = Quaternion.AngleAxis(90f, rotorShaft.up);
        Vector3 directionRollPlus = rotationRollPlus * rotorShaft.forward;
        Debug.DrawRay(rotorShaft.position, directionRollPlus * roll, Color.red);

        Quaternion rotationRollMinus = Quaternion.AngleAxis(270f, rotorShaft.up);
        Vector3 directionRollMinus = rotationRollMinus * rotorShaft.forward;
        Debug.DrawRay(rotorShaft.position, directionRollMinus * roll, Color.blue);

    }
    #region input

    public void OnPitch(InputAction.CallbackContext context)
    {
        pitch = context.ReadValue<float>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        roll = context.ReadValue<float>();
    }
    #endregion
}
