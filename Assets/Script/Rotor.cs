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

        for (int j = 0; j < blades; j++)
        {
            float angleRadians = bladeAngle * Mathf.Deg2Rad;
            Vector3 rayDirection = new Vector3(Mathf.Cos(angleRadians), 0f, Mathf.Sin(angleRadians));

            for (int i = 0; i < subdivision; i++)
            {
                Vector3 pointPosition = rotorShaft.position + rayDirection * spacing * i;
                Debug.DrawRay(pointPosition, rotorShaft.up * 0.1f, Color.blue); // Changez Vector3.up en la direction que vous souhaitez afficher le point
            }
            bladeAngle += angleBetweenBlades;
        }
    }
}
