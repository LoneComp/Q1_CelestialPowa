using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HumanAI : MonoBehaviour
{
    // Movement values
    private float speed = 5f;
    Transform target;
    Transform foodTarget;
    
    // Hunger values
    private float startingHunger;
    private float maxHunger = 100;
    private float lowestHunger = 0;
    private bool satiated;

    private float _currentHunger;
    public float currentHunger { get { return _currentHunger; } set { _currentHunger = Mathf.Clamp(value, lowestHunger, maxHunger); } }
    private void Awake()
    {
        // All the references will be initialized here
    }
    private void Start()
    {
        // Setup the initial values
        startingHunger = Random.Range(60, 100);
        _currentHunger = startingHunger;
        
        // Behavior tree will be constructed here
        ConstructBehaviorTree();
    }
    
    #region Behavior Tree
    private void ConstructBehaviorTree()
    {
        // This is a placeholder for the behavior tree
        // All the node will be added here
        N_Hunger nHunger = new N_Hunger(this, lowestHunger);
        N_WonderAround nWonderAround = new N_WonderAround(this, target);
        N_LookForFood nLookForFood = new N_LookForFood(this, foodTarget);
    }
    #endregion

    private void Update()
    {
        satiated = currentHunger >= 75f; // Set satiated
        
        DeleteHunger();
        FindClosestFood();
        Die(currentHunger, lowestHunger);
        
        // Debug //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EatFood(10);
            Debug.Log("currentHunger: " + currentHunger);
        }
    }
    
    #region public Methods
    public void EatFood(float foodValue)
    {
        currentHunger += foodValue;
    }
    
    // public void GoTo(Transform _target)
    // {
    //     // Move to the target
    //     transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.fixedDeltaTime);
    // }
    #endregion
    
    #region Private Methods
    private float DeleteHunger()
    {
        currentHunger -= 0.05f * Time.fixedDeltaTime;
        
        return currentHunger;
    }
    
    private void Die(float _hungerThreshold, float _lowestHunger)
    {
        if (_hungerThreshold <= _lowestHunger)
        {
            this.gameObject.SetActive(false); // Optimise it
        }
    }
    
    private Transform FindClosestFood()
    {
        // Find the closest food
        
        //Make the logic
        
        return target;
    }
    #endregion
}
