# TestCase1
A simple 2D movement with a basic attacking mechanism
All scripts are [here](https://github.com/Mute06/TestCase1/tree/main/Assets/Scripts)

## PlayerMovement Script
Every variable that needs to be changed in editor are marked as `[SerializeField] private` 

Rotates the player when moving left this is better than flipx because it also moves the child attackCentre empty gameobject

Idle and Walk anims are switched in animator using a float variable called 'speed'

Ground check is done using `Physics2D.OverlapCircle()`
If player is grounded and jumps `AddForceY()` function is used to jump

Checks if there is an emeny using `Physics2D.OverlapCircle()` and gets it `IDamagable` component

## Enemy

Abstracting the `TakeDamage()` into a interface called `IDamagable` allows us to use the same detection code with all kinds of diffent enemy objects

We store the initial color of the sprite as we need to return it to its original state after taking damage
After taking damage a coroutine is called that will return it back to its orignal state
This coroutine is stored in a variable and stopped when an attack occurs while it's in damaged state
This is to make sure the enemy doesnt return to its orignal color before it should
