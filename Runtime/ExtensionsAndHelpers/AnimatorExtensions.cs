using System;
using System.Collections.Generic;
using UnityEngine;

namespace StudioName.Runtime.ExtensionAndHelpers
{
    /// <summary>
    /// Extends the <see cref="UnityEngine.Animator"/> class with additional functionality
    /// </summary>
    public static class AnimatorExtensions
    {
        /// <summary>
        /// Saves the state of our animator by state and play position of each layer in our animator
        /// </summary>
        /// <param name="animator">The animator to snapshot</param>
        /// <returns>A snapshot of our animator containing all information necessary in order to restore animator play state later</returns>
        public static AnimatorSnapshot SaveSnapshot(this Animator animator)
        {
            //create a new snapshot
            var animatorSnapshot = new AnimatorSnapshot
            {
                layerSnapshots = new List<AnimatorLayerSnapshot>(),
                floatParams = new Dictionary<AnimatorControllerParameter, float>(),
                intParams = new Dictionary<AnimatorControllerParameter, int>(),
                boolAndTriggerParams = new Dictionary<AnimatorControllerParameter, bool>()
            };
            
            //save all of our animator layer snapshots so we can restore full state of animator later
            for (var i = 0; i < animator.layerCount; i++)
            {
                var currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(i);
                animatorSnapshot.layerSnapshots.Add(new AnimatorLayerSnapshot
                {
                    stateNameHash = currentAnimatorStateInfo.fullPathHash,
                    layerIndex = i,
                    normalizedTime = currentAnimatorStateInfo.normalizedTime
                });
            }
            
            //save all of our animator parameters so we can restore them later
            foreach (var parameter in animator.parameters)
            {
                switch (parameter.type)
                {
                    case AnimatorControllerParameterType.Float:
                        animatorSnapshot.floatParams.Add(parameter, animator.GetFloat(parameter.name));
                        break;
                    case AnimatorControllerParameterType.Int:
                        animatorSnapshot.intParams.Add(parameter, animator.GetInteger(parameter.name));                        
                        break;
                    case AnimatorControllerParameterType.Bool:
                        animatorSnapshot.boolAndTriggerParams.Add(parameter, animator.GetBool(parameter.name));
                        break;
                    case AnimatorControllerParameterType.Trigger:
                        goto case AnimatorControllerParameterType.Bool;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            //return the snapshot
            return animatorSnapshot;
        }
        
        /// <summary>
        /// Restores our animator to the state it was in when the <see cref="AnimatorSnapshot"/> was taken
        /// </summary>
        /// <param name="animator">the animator to restore snapshot to</param>
        /// <param name="animatorSnapshot">The snapshot we want to restore our animator to</param>
        public static void RestoreSnapshot(this Animator animator, AnimatorSnapshot animatorSnapshot)
        {
            //restore all of our animator parameters
            foreach (var floatParam in animatorSnapshot.floatParams)
            {
                animator.SetFloat(floatParam.Key.name,floatParam.Value);
            }
            foreach (var intParam in animatorSnapshot.intParams)
            {
                animator.SetInteger(intParam.Key.name,intParam.Value);
            }
            foreach (var boolAndTriggerParam in animatorSnapshot.boolAndTriggerParams)
            {
                animator.SetBool(boolAndTriggerParam.Key.name,boolAndTriggerParam.Value);
            }
            //play each of animator layers from where they left off
            foreach (var layerSnapshot in animatorSnapshot.layerSnapshots)
            {
                animator.Play(layerSnapshot.stateNameHash, layerSnapshot.layerIndex, layerSnapshot.normalizedTime);
            }
        }

        /// <summary>
        /// A snapshot of our entire animator. That is All layers and their current states and play positions
        /// </summary>
        public struct AnimatorSnapshot
        {
            /// <summary>
            /// A snapshot of all the layers of an animator. Restoring all layers will restore the full state of the animator.
            /// </summary>
            public List<AnimatorLayerSnapshot> layerSnapshots;
            /// <summary>
            /// The bool and trigger parameters mapped to their current values for our animator (triggers are just sexy bool params lol)
            /// </summary>
            public Dictionary<AnimatorControllerParameter,bool> boolAndTriggerParams;
            /// <summary>
            /// The int parameters mapped to their current values for our animator
            /// </summary>
            public Dictionary<AnimatorControllerParameter,int> intParams;
            /// <summary>
            /// The float parameters mapped to their current values for our animator
            /// </summary>
            public Dictionary<AnimatorControllerParameter,float> floatParams;
        }

        /// <summary>
        /// A snapshot of a single layer of an animator
        /// </summary>
        public struct AnimatorLayerSnapshot
        {
            /// <summary>
            /// The name hash of the state that is currently playing.
            /// </summary>
            public int stateNameHash;
            /// <summary>
            /// The index of this layer.
            /// </summary>
            public int layerIndex;
            /// <summary>
            /// The time offset between zero and one that this state is currently playing at.
            /// </summary>
            public float normalizedTime;
        }
    }
}