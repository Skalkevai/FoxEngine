using UnityEngine;
using FoxEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace FoxEngine
{
    public enum ControllerType
    {
        None,
        KeyboardMouse,
        Xbox,
        Ps4,
        Switch,
        SteamDeck,
        Generic,
    }

    public enum GamePadControls
    {
        None, A, B, X, Y, Start, Select, RB, RT, R3, LB, LT, L3, LeftJoystick_X, LeftJoystick_Y, RightJoystick_X, RightJoystick_Y, Up, Down, Left, Right
    }

    [Serializable]
    public struct ControlsPair
    {
        public GamePadControls gamePadControl;
        public KeyCode key;
    }

    public abstract class InputManager<T> : Singleton<T> where T : InputManager<T>
    {
        public static ControllerType type = default;

        [Header("Controls")]
        [SerializeField] private MappingInputs mapping;

        [Space, Header("Icons")]
        public FoxDictionary<ControllerType, ControllerIcons> iconsPlatforms = new FoxDictionary<ControllerType, ControllerIcons>();

        public FoxDictionary<string, ControlsPair[]> Controls => mapping?.Controls;

        public override void Awake()
        {
            base.Awake();
            type = ControllerType.KeyboardMouse;

            mapping?.GenerateDictionnary();
        }

        protected virtual void Update()
        {
            InputSystem.Update();
        }

        public void ChangeMapping(MappingInputs _mapping)
        {
            mapping = _mapping;
            mapping.GenerateDictionnary();
        }

        public static Vector2 GetMousePosition() => Instance.GetMousePositionInScreen();
        public virtual Vector2 GetMousePositionInScreen() { return Input.mousePosition; }
        public static Vector2 GetMousePositionInWorld2D() => Instance.GetMousePositionInWorld();
        public virtual Vector2 GetMousePositionInWorld() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

        public static bool GetKeyDown(string _key) => Instance.IsKeyDown(_key);
        protected virtual bool IsKeyDown(string _key)
        {
            if (Controls == null)
                return false;

            if (Controls.ContainsKey(_key))
            {
                foreach (var item in Controls[_key])
                {
                    if (Input.GetKeyDown(item.key))
                        return true;
                }
            }

            return false;
        }

        public static bool GetKey(string _key) => Instance.IsKeyHold(_key);
        protected virtual bool IsKeyHold(string _key)
        {
            if (Controls == null)
                return false;

            if (Controls.ContainsKey(_key))
            {
                foreach (var item in Controls[_key])
                {
                    if (Input.GetKey(item.key))
                        return true;
                }
            }

            return false;
        }

        public static bool GetKeyUp(string _key) => Instance.IsKeyUp(_key);
        protected virtual bool IsKeyUp(string _key)
        {
            if (Controls == null)
                return false;

            if (Controls.ContainsKey(_key))
            {
                foreach (var item in Controls[_key])
                {
                    if (Input.GetKeyUp(item.key))
                        return true;
                }
            }

            return false;
        }

        public static float GetAxis(string _key) => Instance.GetInputAxis(_key);
        protected virtual float GetInputAxis(string _key)
        {
            return Input.GetAxisRaw(_key);
        }

        public static Sprite GetIcon(string _key) => Instance.GetControllerIcon(_key);

        protected Sprite GetControllerIcon(string _key)
        {
            if (iconsPlatforms.TryGetValue(type, out ControllerIcons icons))
                return icons.GetIcon(_key);

            return null;
        }

        protected virtual void OnGUI()
        {
            //GUI.color = Color.white;
            //GUILayout.Space(25);
            //GUILayout.Label($"Controller Type : {type}");
        }
    }
}