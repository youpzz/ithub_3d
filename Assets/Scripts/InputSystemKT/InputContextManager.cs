using UnityEngine;

public class InputContextManager : MonoBehaviour
{
    private PlayerControls _input;

    private void Awake()
    {
        _input = new PlayerControls();
        _input.Player.Enable();
    }

    public void ToggleVehicleMode(bool isInVehicle)
    {
        if (isInVehicle)
        {
            _input.Player.Disable();
            _input.Vehicle.Enable();
        }
        else
        {
            _input.Vehicle.Disable();
            _input.Player.Enable();
        }
    }
}