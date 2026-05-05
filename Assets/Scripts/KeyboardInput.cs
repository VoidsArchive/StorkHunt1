using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    public Mongoose Mongoose;
    void Update()
    {
       Keyboard keyboard = Keyboard.current;
       
       if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
       {
           Mongoose.Move(Vector2.left);
       }
       if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
       {
           Mongoose.Move(Vector2.right);
       }
       
    }
}
