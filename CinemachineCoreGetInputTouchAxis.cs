using Cinemachine;
using UnityEngine;


public class CinemachineCoreGetInputTouchAxis : MonoBehaviour
{

    public float TouchSensitivity_x = 10f;
    public float TouchSensitivity_y = 10f;

    // Use this for initialization
    void Start()
    {
        CinemachineCore.GetInputAxis = HandleAxisInputDelegate;
    }

    float HandleAxisInputDelegate(string axisName)
    {
        switch (axisName)
        {

            case "Mouse X":

                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        if (Input.touches[i].position.x / Screen.width >= 0.5)
                        {
                            return Input.touches[i].deltaPosition.x / TouchSensitivity_x;
                            
                        }
                        
                    }
                    break;
                    
                    
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            case "Mouse Y":
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        if (Input.touches[i].position.x / Screen.width >= 0.5)
                        {
                            return Input.touches[i].deltaPosition.y / TouchSensitivity_x;

                        }

                    }
                    break;
                }
                else
                {
                    return Input.GetAxis(axisName);
                }

            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }
}