using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FoxEngine
{
    public class NewInputSystem : InputManager<NewInputSystem>
    {
        private const string XBOX_CONTROLLER = "XInput";
        private const string PS_CONTROLLER = "DualSense";
        private const string SWITCH_CONTROLLER = "Pro Controller";

        public void OnEnable()
        {
            InputSystem.onAfterUpdate += CheckDevice;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputSystem.onAfterUpdate -= CheckDevice;
        }

        private void CheckDevice()
        {
            if (Gamepad.current?.wasUpdatedThisFrame ?? false)
            {
                string gamePadName = Gamepad.current.name;

                if (gamePadName.Contains(XBOX_CONTROLLER))
                    type = ControllerType.Xbox;
                else if (gamePadName.Contains(PS_CONTROLLER))
                    type = ControllerType.Ps4;
                //else if (gamePadName.Contains(SWITCH_CONTROLLER))
                //type = ControllerType.Switch;
                else
                    type = ControllerType.Generic;
            }
            else if (Keyboard.current.wasUpdatedThisFrame)
                type = ControllerType.KeyboardMouse;
        }

        protected override float GetInputAxis(string _key)
        {
            if (type != ControllerType.KeyboardMouse)
            {
                if (Gamepad.current == null)
                {
                    FoxEngine.Debug.LogError("No controller detected but you are in Gamepad Mode");
                    return 0;
                }

                List<GamePadControls> control = new List<GamePadControls>();

                if (Controls != null && Controls.ContainsKey(_key))
                {
                    float value = 0;
                    foreach (var item in Controls[_key])
                    {
                        switch (item.gamePadControl)
                        {
                            case GamePadControls.LeftJoystick_X:
                                value = Gamepad.current.leftStick.ReadValue().x;
                                break;
                            case GamePadControls.LeftJoystick_Y:
                                value = Gamepad.current.leftStick.ReadValue().y;
                                break;
                            case GamePadControls.RightJoystick_X:
                                value = Gamepad.current.rightStick.ReadValue().x;
                                break;
                            case GamePadControls.RightJoystick_Y:
                                value = Gamepad.current.rightStick.ReadValue().y;
                                break;
                            default:
                                value = 0f;
                                break;
                        }

                        if (value != 0)
                            break;
                    }

                    return value;
                }
                else
                    return Input.GetAxisRaw(_key);
            }
            else
                return Input.GetAxisRaw(_key);
        }

        protected override bool IsKeyDown(string _key)
        {
            if (type != ControllerType.KeyboardMouse)
            {

                if (Gamepad.current == null)
                {
                    FoxEngine.Debug.LogError("No controller detected but you are in Gamepad Mode");
                    return false;
                }

                if (Controls != null && Controls.ContainsKey(_key))
                {
                    bool keyDown = false;
                    foreach (var item in Controls[_key])
                    {
                        switch (item.gamePadControl)
                        {
                            case GamePadControls.A:
                                keyDown = Gamepad.current.aButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.B:
                                keyDown = Gamepad.current.bButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.X:
                                keyDown = Gamepad.current.xButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.Y:
                                keyDown = Gamepad.current.yButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.Start:
                                keyDown = Gamepad.current.startButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.Select:
                                keyDown = Gamepad.current.selectButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.RB:
                                keyDown = Gamepad.current.rightShoulder.wasPressedThisFrame;
                                break;
                            case GamePadControls.RT:
                                keyDown = Gamepad.current.rightTrigger.wasPressedThisFrame;
                                break;
                            case GamePadControls.R3:
                                keyDown = Gamepad.current.rightStickButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.LB:
                                keyDown = Gamepad.current.leftShoulder.wasPressedThisFrame;
                                break;
                            case GamePadControls.LT:
                                keyDown = Gamepad.current.leftTrigger.wasPressedThisFrame;
                                break;
                            case GamePadControls.L3:
                                keyDown = Gamepad.current.leftStickButton.wasPressedThisFrame;
                                break;
                            case GamePadControls.Up:
                                keyDown = Gamepad.current.dpad.up.wasPressedThisFrame;
                                break;
                            case GamePadControls.Down:
                                keyDown = Gamepad.current.dpad.down.wasPressedThisFrame;
                                break;
                            case GamePadControls.Left:
                                keyDown = Gamepad.current.dpad.left.wasPressedThisFrame;
                                break;
                            case GamePadControls.Right:
                                keyDown = Gamepad.current.dpad.right.wasPressedThisFrame;
                                break;
                            default:
                                keyDown = false;
                                break;
                        }
                        if (keyDown != false)
                            break;
                    }
                    return keyDown;
                }
                else
                    return false;
            }
            else
            {
                if (Controls != null && Controls.ContainsKey(_key))
                {
                    foreach (var item in Controls[_key])
                    {
                        if (Input.GetKeyDown(item.key))
                            return true;
                    }
                    return false;
                }
                return false;
            }
        }

        protected override bool IsKeyHold(string _key)
        {
            if (type != ControllerType.KeyboardMouse)
            {
                if (Gamepad.current == null)
                {
                    FoxEngine.Debug.LogError("No controller detected but you are in Gamepad Mode");
                    return false;
                }

                if (Controls != null && Controls.ContainsKey(_key))
                {
                    bool keyHold = false;
                    foreach (var item in Controls[_key])
                    {
                        switch (item.gamePadControl)
                        {
                            case GamePadControls.A:
                                keyHold = Gamepad.current.aButton.isPressed;
                                break;
                            case GamePadControls.B:
                                keyHold = Gamepad.current.bButton.isPressed;
                                break;
                            case GamePadControls.X:
                                keyHold = Gamepad.current.xButton.isPressed;
                                break;
                            case GamePadControls.Y:
                                keyHold = Gamepad.current.yButton.isPressed;
                                break;
                            case GamePadControls.Start:
                                keyHold = Gamepad.current.startButton.isPressed;
                                break;
                            case GamePadControls.Select:
                                keyHold = Gamepad.current.selectButton.isPressed;
                                break;
                            case GamePadControls.RB:
                                keyHold = Gamepad.current.rightShoulder.isPressed;
                                break;
                            case GamePadControls.RT:
                                keyHold = Gamepad.current.rightTrigger.isPressed;
                                break;
                            case GamePadControls.R3:
                                keyHold = Gamepad.current.rightStickButton.isPressed;
                                break;
                            case GamePadControls.LB:
                                keyHold = Gamepad.current.leftShoulder.isPressed;
                                break;
                            case GamePadControls.LT:
                                keyHold = Gamepad.current.leftTrigger.isPressed;
                                break;
                            case GamePadControls.L3:
                                keyHold = Gamepad.current.leftStickButton.isPressed;
                                break;
                            case GamePadControls.Up:
                                keyHold = Gamepad.current.dpad.up.isPressed;
                                break;
                            case GamePadControls.Down:
                                keyHold = Gamepad.current.dpad.down.isPressed;
                                break;
                            case GamePadControls.Left:
                                keyHold = Gamepad.current.dpad.left.isPressed;
                                break;
                            case GamePadControls.Right:
                                keyHold = Gamepad.current.dpad.right.isPressed;
                                break;
                            default:
                                keyHold = false;
                                break;
                        }

                        if (keyHold != false)
                            break;
                    }
                    return keyHold;
                }
                else
                    return false;
            }
            else
            {
                if (Controls != null && Controls.ContainsKey(_key))
                {
                    foreach (var item in Controls[_key])
                    {
                        if (Input.GetKey(item.key))
                            return true;
                    }
                    return false;
                }
                return false;
            }
        }

        protected override bool IsKeyUp(string _key)
        {
            if (type != ControllerType.KeyboardMouse)
            {
                if (Gamepad.current == null)
                {
                    FoxEngine.Debug.LogError("No controller detected but you are in Gamepad Mode");
                    return false;
                }

                if (Controls != null && Controls.ContainsKey(_key))
                {
                    bool keyReleased = false;
                    foreach (var item in Controls[_key])
                    {
                        switch (item.gamePadControl)
                        {
                            case GamePadControls.A:
                                keyReleased = Gamepad.current.aButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.B:
                                keyReleased = Gamepad.current.bButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.X:
                                keyReleased = Gamepad.current.xButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Y:
                                keyReleased = Gamepad.current.yButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Start:
                                keyReleased = Gamepad.current.startButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Select:
                                keyReleased = Gamepad.current.selectButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.RB:
                                keyReleased = Gamepad.current.rightShoulder.wasReleasedThisFrame;
                                break;
                            case GamePadControls.RT:
                                keyReleased = Gamepad.current.rightTrigger.wasReleasedThisFrame;
                                break;
                            case GamePadControls.R3:
                                keyReleased = Gamepad.current.rightStickButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.LB:
                                keyReleased = Gamepad.current.leftShoulder.wasReleasedThisFrame;
                                break;
                            case GamePadControls.LT:
                                keyReleased = Gamepad.current.leftTrigger.wasReleasedThisFrame;
                                break;
                            case GamePadControls.L3:
                                keyReleased = Gamepad.current.leftStickButton.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Up:
                                keyReleased = Gamepad.current.dpad.up.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Down:
                                keyReleased = Gamepad.current.dpad.down.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Left:
                                keyReleased = Gamepad.current.dpad.left.wasReleasedThisFrame;
                                break;
                            case GamePadControls.Right:
                                keyReleased = Gamepad.current.dpad.right.wasReleasedThisFrame;
                                break;
                            default:
                                keyReleased = false;
                                break;
                        }

                        if (keyReleased != false)
                            break;
                    }
                    return keyReleased;
                }
                else
                    return false;
            }
            else
            {
                if (Controls != null && Controls.ContainsKey(_key))
                {
                    foreach (var item in Controls[_key])
                    {
                        if (Input.GetKeyUp(item.key))
                            return true;
                    }
                    return false;
                }

                return false;
            }
        }
    }
}