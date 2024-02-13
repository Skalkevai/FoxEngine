using System.Collections.Generic;
using UnityEngine;

namespace Frost
{
    public class RegistrableStateMachineBehaviour : StateMachineBehaviour
    {
        public delegate void RegistrableStateMachineEvent(RegistrableStateMachineBehaviour _behaviour,string _key);

        private static Dictionary<Animator, RegistrableStateMachineEvent> onEnter = new Dictionary<Animator, RegistrableStateMachineEvent>();
        private static Dictionary<Animator, RegistrableStateMachineEvent> onUpdate = new Dictionary<Animator, RegistrableStateMachineEvent>();
        private static Dictionary<Animator, RegistrableStateMachineEvent> onExit = new Dictionary<Animator, RegistrableStateMachineEvent>();

        public string key;
        private AnimatorStateInfo lastAnimationInfo;

        private static void Clear()
        {
            onEnter.Clear();
            onUpdate.Clear();
            onExit.Clear();
        }

        public static void RegisterOnEnter(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onEnter.ContainsKey(animator))
                onEnter[animator] += handler;
            else
                onEnter.Add(animator, handler);
        }

        public static void UnregisterOnEnter(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onEnter.ContainsKey(animator))
            {
                onEnter[animator] -= handler;

                if (onEnter[animator] == null)
                    onEnter.Remove(animator);
            }
        }

        public static void RegisterOnUpdate(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onUpdate.ContainsKey(animator))
                onUpdate[animator] += handler;
            else
                onUpdate.Add(animator, handler);
        }

        public static void UnregisterOnUpdate(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onUpdate.ContainsKey(animator))
            {
                onUpdate[animator] -= handler;

                if (onUpdate[animator] == null)
                    onUpdate.Remove(animator);
            }
        }

        public static void RegisterOnExit(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onExit.ContainsKey(animator))
                onExit[animator] += handler;
            else
                onExit.Add(animator, handler);
        }

        public static void UnregisterOnExit(Animator animator, RegistrableStateMachineEvent handler)
        {
            if (onExit.ContainsKey(animator))
            {
                onExit[animator] -= handler;

                if (onExit[animator] == null)
                    onExit.Remove(animator);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            RegistrableStateMachineEvent handler = null;
            lastAnimationInfo = stateInfo;
            if (onEnter.TryGetValue(animator, out handler) && handler != null)
                handler(this,key);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            RegistrableStateMachineEvent handler = null;
            lastAnimationInfo = stateInfo;
            if (onUpdate.TryGetValue(animator, out handler) && handler != null)
                handler(this, key);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            RegistrableStateMachineEvent handler = null;
            lastAnimationInfo = stateInfo;
            if (onExit.TryGetValue(animator, out handler) && handler != null)
                handler(this, key);
        }

        public AnimatorStateInfo QueryAnimatorStateInfo()
        {
            return lastAnimationInfo;
        }
    }
}
