using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {

    // Combat animations
    public AnimationClip replaceableAttackAnim;     // Default combat animation to be overridden
    public AnimationClip[] defaultAttackAnimSet;    // Array for default combat animations to be overridden
    protected AnimationClip[] currentAttackAnimSet; // Array for current combat animations if overridden

    const float locomationAnimationSmoothTime = .1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    protected AnimatorOverrideController overrideController;

    // Use this for initialization
	protected virtual void Start () {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();

        // Allows swapping any animator clip for another clip
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        //Sets attack animation set to default
        currentAttackAnimSet = defaultAttackAnimSet;
        combat.OnAttack += OnAttack;

	}

	// Update is called once per frame	
	protected virtual void Update () {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat", combat.InCombat);
	}

    protected virtual void OnAttack()
    {
        animator.SetTrigger("attack");

		// Get random animation in attack animation slot
        int attackIndex = Random.Range(0, currentAttackAnimSet.Length);

        // Override default attack animation with that animation
        overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[attackIndex];
    }
}
