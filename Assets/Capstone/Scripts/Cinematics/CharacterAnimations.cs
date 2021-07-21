using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using DG.Tweening;

namespace InterDigital.CMU
{
    [System.Serializable]
    public class OnEnableAnimation : UnityEvent { }

    [System.Serializable]
    public class OnDisableAnimation : UnityEvent { }

    [System.Serializable]
    public class OnSetTransform : UnityEvent { }


    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterAnimations : MonoBehaviour
    {
        [SerializeField]
        Animator animator;

        [SerializeField]
        NavMeshAgent agent;
        float duration;

        public void EnableAnimation(string anim)
        {
            animator.SetBool(anim, true);
        }

        public void DisableAnimation(string anim)
        {
            animator.SetBool(anim, false);
        }

        public void MoveToPosition(Transform destination)
        {
            agent.SetDestination(destination.position);
        }

        public void SetAnimationDuration(float d)
        {
            duration = d;
        }

        public void MovePositionNoNavmesh(Transform dest)
        {
            transform.DOMove(dest.position, duration);
        }

        public void RotateTowards(Transform target)
        {
            Quaternion look = Quaternion.LookRotation(Vector3.Normalize(target.position - transform.position));
            transform.DORotateQuaternion(look, 1f);
        }
    }
}