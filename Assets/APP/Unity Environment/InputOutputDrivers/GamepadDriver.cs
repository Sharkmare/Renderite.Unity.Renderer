using Renderite.Shared;
using Renderite.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadDriver : InputDriver
{
    // Gamepad-001: cache the last seen Unity Gamepad → GamepadState mapping so we don't
    // allocate a new LINQ lambda + perform a string-compare search every frame at 90 Hz.
    UnityEngine.InputSystem.Gamepad _lastUnityGamepad;
    GamepadState _cachedGamepadState;

    public override void UpdateState(InputState state)
    {
        var unityGamepad = UnityEngine.InputSystem.Gamepad.current;

        if (unityGamepad == null)
            return;

        if(state.gamepads == null)
            state.gamepads = new List<GamepadState>();

        GamepadState gamepad;

        if (unityGamepad == _lastUnityGamepad && _cachedGamepadState != null)
        {
            // Fast path: same device as last frame — no search needed.
            gamepad = _cachedGamepadState;
        }
        else
        {
            // Slow path: device changed or first frame — search by name and cache result.
            gamepad = null;
            for (int i = 0; i < state.gamepads.Count; i++)
            {
                if (state.gamepads[i].displayName == unityGamepad.displayName)
                {
                    gamepad = state.gamepads[i];
                    break;
                }
            }

            if (gamepad == null)
            {
                gamepad = new GamepadState();
                state.gamepads.Add(gamepad);
            }

            _lastUnityGamepad = unityGamepad;
            _cachedGamepadState = gamepad;
        }

        gamepad.displayName = unityGamepad.displayName;

        gamepad.a = unityGamepad.aButton.isPressed;
        gamepad.b = unityGamepad.bButton.isPressed;
        gamepad.x = unityGamepad.xButton.isPressed;
        gamepad.y = unityGamepad.yButton.isPressed;

        gamepad.leftBumper = unityGamepad.leftShoulder.isPressed;
        gamepad.rightBumper = unityGamepad.rightShoulder.isPressed;

        gamepad.leftThumbstick = unityGamepad.leftStick.ReadValue().ToRender();
        gamepad.leftThumbstickClick = unityGamepad.leftStickButton.isPressed;

        gamepad.rightThumbstick = unityGamepad.rightStick.ReadValue().ToRender();
        gamepad.rightThumbstickClick = unityGamepad.rightStickButton.isPressed;

        gamepad.leftTrigger = unityGamepad.leftTrigger.ReadValue();
        gamepad.rightTrigger = unityGamepad.rightTrigger.ReadValue();

        gamepad.dPadUp = unityGamepad.dpad.up.isPressed;
        gamepad.dPadRight = unityGamepad.dpad.right.isPressed;
        gamepad.dPadDown = unityGamepad.dpad.down.isPressed;
        gamepad.dPadLeft = unityGamepad.dpad.left.isPressed;

        gamepad.menu = unityGamepad.selectButton.isPressed;
        gamepad.start = unityGamepad.startButton.isPressed;
    }
}
