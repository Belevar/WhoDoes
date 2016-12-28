using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GamesIconsAnimator : MonoBehaviour {

    [Tooltip("Animator for games icons")]
    public Animator animator;
    public Transform gamesIconsPlaceholder;
    List<GameObject> gamesAnimationObjects;
    int currentAnimation;

	// Use this for initialization
	void Start () {
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        gamesAnimationObjects = new List<GameObject>();
        currentAnimation = 0;
        foreach (Transform rowOfGames in gamesIconsPlaceholder)
        {
            foreach (Transform game in rowOfGames)
            {
                gamesAnimationObjects.Add(game.gameObject);    
            }
        }
        animator.SetTrigger("PopUp");
        playAllIconsAnimations();
    }
	
	// Update is called once per frame
	void Update () {
        playAllIconsAnimations();
	}

    float timeBetweenAnimation = 1f;
    float timePassed = 0f;

    void playAllIconsAnimations()
    {
        if(timePassed < timeBetweenAnimation)
        {
            timePassed += Time.deltaTime;
        } else
        {
            string trigger = gamesAnimationObjects[currentAnimation].name;
            if(trigger == "GameObject") //very bad workaround needen while developing in train
            {
                trigger = "spinning bottle";
            }
            animator.SetTrigger(trigger);
            timePassed = 0f;
            ++currentAnimation;
            Debug.Log("Current Animatoin " + currentAnimation);
            Debug.Log("Current Count " + gamesAnimationObjects.Count);
            currentAnimation = currentAnimation % gamesAnimationObjects.Count;
            Debug.Log("Current Animatoin " + currentAnimation);
        }
    }
}
