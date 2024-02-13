using UnityEngine;
using Frost;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using System.Linq;

namespace Frost
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

        [Title("Controls")]
        [SerializeField] private MappingInputs mapping;
        [SerializeField] private List<MappingInputs> allMappings = new List<MappingInputs>();

        [Space, Title("Icons")]
        public FrostDictionary<ControllerType, ControllerIcons> iconsPlatforms = new FrostDictionary<ControllerType, ControllerIcons>();

        [Title("Blocked Inputs")]
        [SerializeField] private bool allBlocked = false;

        public FrostDictionary<string, ControlsPair[]> Controls => mapping?.Controls;

        public override void Awake()
        {
            base.Awake();
            type = ControllerType.KeyboardMouse;

            mapping?.GenerateDictionnary();

            ClearAllBlockedInputs();
        }

        protected virtual void Update()
        {
            InputSystem.Update();
        }

        public void ClearAllBlockedInputs()
        {
            allBlocked = false;
            foreach (var mapping in allMappings)
                mapping.ClearBlockInputs();
        }

        public void ChangeMapping(MappingInputs _mapping)
        {
            if (allMappings.Contains(_mapping))
            {
                mapping = _mapping;
                mapping.GenerateDictionnary();
            }
        }

        public MappingInputs GetMapping(string _mappingName)
        { 
            foreach (var mapping in allMappings) 
            {
                if(mapping.ID == _mappingName)
                    return mapping;
            }

            Debug.LogError($"[InputManager] No Mapping found with ID : {_mappingName}");
            return null;
        }

        public static Vector2 GetMousePosition() => Instance.GetMousePositionInScreen();
        public virtual Vector2 GetMousePositionInScreen() { return Input.mousePosition; }
        public static Vector2 GetMousePositionInWorld2D() => Instance.GetMousePositionInWorld();
        public virtual Vector2 GetMousePositionInWorld() { return Camera.main.ScreenToWorldPoint(Input.mousePosition); }

        public static bool GetKeyDown(string _key) => Instance.IsKeyDown(_key);
        protected virtual bool IsKeyDown(string _key)
        {
            if (IsBlock(_key))
                return false;

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
            if (IsBlock(_key))
                return false;

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
            if (IsBlock(_key))
                return false;

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
            if(IsBlock(_key))
                return 0f;

            return Input.GetAxisRaw(_key);
        }

        public void UnBlockInputs()
        {
            allBlocked = false;
        }

        public void BlockInputs()
        {
            allBlocked = true;
        }

        public void UnBlockInputs(MappingInputs _mapping)
        {
            _mapping?.UnBlockInputs();
        }

        public void BlockInputs(MappingInputs _mapping)
        {
            _mapping?.BlockInputs();
        }

        public void BlockInput(MappingInputs _mapping, ActionInput _action)
        {
            _mapping?.BlockInput(_action);
        }

        public void UnBlockInput(MappingInputs _mapping, ActionInput _action)
        {
            _mapping?.UnBlockInput(_action);
        }

        public void BlockInput(string _mapping,string _action)
        {
            MappingInputs mapp = GetMapping(_mapping);
            if (mapp != null)
            { 
                ActionInput action = mapp.GetAction(_action);
                if (action != null)
                    mapp.BlockInput(action);
            }
            else
                Debug.LogError($"[InputManager] No Mapping found with ID : {_mapping}");
        }

        public void UnBlockInput(string _mapping, string _action)
        {
            MappingInputs mapp = GetMapping(_mapping);
            if (mapp != null)
            {
                ActionInput action = mapp.GetAction(_action);
                if (action != null)
                    mapp.UnBlockInput(action);
            }
            else
                Debug.LogError($"[InputManager] No Mapping found with ID : {_mapping}");
        }

        public bool IsBlock(string _action)
        {
            ActionInput action = mapping?.GetAction(_action)??null;
            if (action != null)
            {
                if (mapping != null && (allBlocked || mapping.IsBlocked(action)))
                    return true;
            }

            return false;
        }

        public bool IsBlock(ActionInput _action)
        {
            if (mapping != null && (allBlocked || mapping.IsBlocked(_action)))
                return true;

            return false;
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