/*
 ╚════════════════════════════════════════════════════╦═══════════════════════════════════╗
 ╦══════════════╦═════════════════════════╦╗─╔═══════─╚════╗─╔═══════════════════════════─║
 ║─══╦════════╗─╠═══════════════════════╗─║║─║─╔╦═════╦════╝─╠════─════╦═════════╦════════╣
 ╠═╦─╚═════╦╗─║─║─╔▄▀▀▀▀▄╔══▀██▀▀▀▀█▄╔╗─║─╚╝─║─╚╝─║─▄▀▀▀▀▄╗─╔▀██▀▀▀▀█▄─╚═─╔═══╗─╔╝─╔════╗─║
 ║─╚══════─╚╣─║─║─█─╔══─╠█─¤─██─────▀█╣─╚════╬════╩══╦═══─█─║─██─────▀█═══╝─╔═╝─╚╗─║─╔╗─║─║
 ║─╔════════╝─║─╠═══╝─▄▄▀╝───██──────█╚════╗─║─╔════─║─▀▀▀▄─║─██──────█─╔═══╩══╗─║─║¤╔╝─║─║
 ║─║─╔═══╦════╣─║─╔▄▄▀╝────╔═██─────▄█─╔═─═╝─║─╚══╦█─╚═══─█─║─██─────▄█─║─╔═══─╝─║─╚═╝─╦╝─║
 ║─║─║─║─╠════╝─║─█▄▄▄▄▄▄█─║▄██▄▄▄▄█▀╝─╚═════╩═══─╚═▀▄▄▄▄▀╝─╚▄██▄▄▄▄█▀══─═╩══════╩═════╩─═╬═╗
 ║─║─║─║─║─╔════╩╦═══╦═CHARACTER╩══════════IN═A════════════WORLD╩════════════════════════-╠═╣
 ║─║─║─║─║─╚══─╗─║─║─║─║─╚═══════════─╔══════════════════════╦═════════════════════╦══════╣
 ║─╚═══╩═══════╩═══╩═══╩══════════════╩By-DANIELEVEL║──────────────────────☠─────────────✧
 ╚═══════════════════════════════════════════════════════════╩═════════════════════╩══════╝

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
 using UnityEditor;
 #endif


public class Alpha_2D_Character_In_3D_World : MonoBehaviour
{

 // ╔════════════════════════════════════════════════════════════『※Setting The things we need※』═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      [Header("╔══════════════════════╗")][Space(-13)]
      [Header("║              ２-D CHARACTERS              ║")][Space(-13)]
      [Header("║                IN A ３-D WORLD                ║")][Space(-13)]
      [Header("║                                                                  ║")][Space(-13)]
      [Header("║                By∼@DANIEL3VEL             ║")][Space(-13)]
      [Header("╚══════════════════════╝")][Space(10)]

      [Tooltip("Drag the Main Camera object here")]
      public Transform TheCamera;//---------------------------------------------------[A transform called TheCamera is created and put serializeField to assign the camera to this code]              
      [Tooltip("Drag the object that has the Animator and Sprite Renderer components here")]
      public GameObject billboard;//--------------------------------------------------[the object that has the Animator and Sprite Renderer components]
      
      [Header("Instructions:")][Space(-13)]
      [Header("┌─┬─┬─┐  0 is the front of '웃'")][Space(-19)]
      [Header("  ７  ０  １")][Space(-19)]
      [Header("├─┼─┼─┤  '웃' is this gameObject")][Space(-19)]
      [Header("  ６  웃  ２")][Space(-19)]
      [Header("├─┼─┼─┤  the order of the sections")][Space(-19)]
      [Header("  ５  ４  ３")][Space(-19)]
      [Header("└─┴─┴─┘  is clockwise ↻ from 0 to 7")][Space(-13)]
      [Header("웃 change depending where # │̲̲̅̅⦿̲̅°̲̅l̲̅ is")]
      //-----------------------------------------------------⦿°Ṑṏ [̲̅  [⦿°]̲̅
      
      [Header("Rendering: Animator/Sprites")]
      [Tooltip("Enable or Disable the animator to change between sprite rendering to animations")]
      public bool useAnimator = false;//----------------------------------------------[this bool is for Enable or Disable the animator to change between sprite rendering to animations]
      public Sprite[] sprites = new Sprite[8];//--------------------------------------[this is a array of sprite objects to contain the 2d images]
      [Header("Character: Simetric/Asimetric")]
      [Tooltip("the 2,3,4 sprites/animations will be flipX and shown when the 6,7,8 are called")]
      public bool useMirror;//--------------------------------------------------------[a publicc bool to change the rendering]
      
      enum Facing {Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft};//----[an enumeration where we create and name each of the directions we want]       
      private Facing _facing;//-------------------------------------------------------[A variable of the Facing type that we just invented, and assign it a default state]                                                   
     
      private float angle;//----------------------------------------------------------[A variable float is created to monitor the value of an angle]
      private float sign=1;//---------------------------------------------------------[a variable to tell if the angle is positive or negative]
      private float signAngle;//------------------------------------------------------[did we really need this?, is just to display the Real value of the angle]
      
      private Animator animator;//----------------------------------------------------[a animator variable to get the animator component of the billboard]
      private SpriteRenderer _sprite;//-----------------------------------------------[a spriteRenderer variable to assign the sprite renderer script of an object to this code]                                 
      private Transform _billboard;//-------------------------------------------------[another transform variable, for the object where the bildboard is]                                    
      private Transform _t;//---------------------------------------------------------[another transform variable, for the object where this script is]
      private Vector3 direction;//----------------------------------------------------[the diren e the camera isrelative to this object]
      private bool tOutcome;//--------------------------------------------------------[this bool is used to tell if the animator controller has a certain parameter with the function ContainsParam]

      

 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※Unity Editor settings※』═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      void OnValidate(){
         
         if (billboard == null){
            Debug.Log("you need to assing the object billboard that has the Animator and Sprite Renderer components to use this script");
         }else{
            animator = billboard.GetComponent<Animator>();
         }
          
         if (sprites.Length != 8){
          sprites = new Sprite[8];//--------------------------------------------------[lock the elements of the list to 8 in the unity editor, because the script is meant to work with 8 faces, this is not necesary if you use the animator, is just for the sprites]
         }//--------------------------------------------------------------------------[having lesss tha 8 spaces in the array, with "useMirror" disabled and "useAnimator" disabled gives an error witout this]
         
         if(useAnimator){ 
            if(animator != null){
               
               animator.enabled = false;
               animator.enabled = true;//---------------------------------------------[this nonsense is just to refresh the Animator component, cause it doesn't refresh by default in the unity editor mode] 
               
            }
            
            if (animator != null && animator.isActiveAndEnabled){
               
               tOutcome = ContainsParam("direction");
               if (tOutcome == false){ 
                  Debug.Log("you need to create a INT parameter called 'direction' in the animatorController in order to work with the script '2D character In a 3D world'");
                  }
            
            }
         }else{
            animator.enabled = false;//-----------------------------------------------[disable the animator, because the animator overwrite the sprite rendering of this script]
         }
      }
 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※ ContainsParam function ※』═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      public bool ContainsParam(  string _ParamName ){
        foreach( AnimatorControllerParameter param in animator.parameters ){
            if( param.name == _ParamName )
            return true;
        }
        return false;
      }
 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※ Void Awake is called before the first frame update ※』═══════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      void Awake(){

         _t = transform;//------------------------------------------------------------[we assign the transformation data of the object in which this script is located to this variable "_t" ]
         
         if (billboard != null){
            animator = billboard.GetComponent<Animator>();
            _sprite = billboard.GetComponent<SpriteRenderer>();
            _billboard = billboard.transform;
         }

      }

 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※ Update is called once per frame ※』══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      void Update(){

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────         
         
         Vector3 forward = _t.forward;//----------------------------------------------[Get a forward vector of this object and keep it in the variable "foward"]

         forward.y = 0;//-------------------------------------------------------------[In the "forward" vector, we set the "y" coordinate to be equal to 0, so that the vector becomes two-dimensional in the "x" and "z" plane]
         
         Vector3 direction2 = _t.InverseTransformPoint(TheCamera.position);//---------[another vector, calculates the relative position of one object (this) to the other (camera)] [InverseTransformPoint Transforms position from world space to local space] 
         
         sign = (direction2.x >= 0) ? -1 : 1;//---------------------------------------[we use this to know the sign of the angle]
         
         angle = Vector3.Angle(direction, forward);//---------------------------------[we use it to know the angle, calculating the smallest angle between the 2 vectors, The result is never greater than 180 degrees]
         
         signAngle = angle * sign;//--------------------------------------------------[this is to know the full angle from -180 to 180, it can be eaasyly turn into 0 to 360 by just adding 180]
      
       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
       
         direction = TheCamera.position - _t.position ;//-----------------------------[another vector, calculates the relative position of one object (this) to the other (camera)]  
         direction.y = 0;//-----------------------------------------------------------[0 in the "Y" axis of the 3D vector "direction" to make it 2D on X,Z]
        _billboard.rotation = Quaternion.LookRotation(-direction, _t.up);//-----------[use the reverse targetpoint and upwards vector, create a quaternion to look at the rotation of this vectors, then instruct the billboard to follow this same rotation]
       
       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

         if(useMirror){
            Miror();//----------------------------------------------------------------[use the miror function]
         }else{
            _sprite.flipX = false;//--------------------------------------------------[if you are using animation dont worry, the animation overrides spriteRenderer coming from a code like this]
         }

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

         if (animator.isActiveAndEnabled == true){
            animator.SetInteger("direction",(int)_facing);//--------------------------[the value of the facing operation is send to the parameter 'direction' in the animatorController of the billboard object]
         }else{
            _sprite.sprite = sprites[(int)_facing];//---------------------------------[the value of the facing operation is used to select a sprite from the array 'sprites' and render it in the spriteRenderer component]
         }

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        
         /*//Debbuging SHIIIIIIIIIIITTTTTTTTTTTTTTT
         animator.SetFloat("angles",signAngle);
         Debug.DrawRay(transform.position, direction,Color.green);
         Debug.DrawRay(transform.position, direction2,Color.blue);  */
        
        //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      }

 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
 
 // ╔════════════════════════════════════════════════════════════『※ Mirror function ※』══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      public void Miror(){

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
          
          switch(_facing ){
             case Facing.DownLeft://--------------------------------------------------[if facing is downleft then change it to be downright and flip the sprite horizontally]
             _facing  = Facing.DownRight;
             _sprite.flipX = true;
             break;
             case Facing.Left://------------------------------------------------------[if facing is left then change it to be right and flip the sprite horizontally]
             _facing  = Facing.Right;
             _sprite.flipX = true;
             break;
             case Facing.UpLeft://----------------------------------------------------[if facing is UpLeft then change it to be UpRight and flip the sprite horizontally]
             _facing  = Facing.UpRight;
             _sprite.flipX = true;
             break;
             default://---------------------------------------------------------------[if none of these cases are detected then don't flip the sprite]
             _sprite.flipX = false;
             break;
           }

           /*⦿Note )⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╮
            this switch-case is just because you can't fill 8 spaces in a list with only 5 objects.(why would you want that?- to make simmetric characters with a more simple spritesheet/animations)
            so when the last 3 objects are called the 2nd,3rd and 4th faces will be shown and then flipped horizontally.
            this script is made so you can work with a minumun of 5 sprites/animations, you never call the last 3 sprites if "useMirror" is active.
            or make a more detailed(assimetric) character using 8 sprites/animations if "useMirror" is inactive.
            simetric characters looks the same in both left and rigth side of their bodies, assimetric characters looks diferent in either side of their bodies.
           ╰⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╯*/
       
        //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      }
 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔═════════════════════════════════════════════════════════════[LateUpdate is called after all Update functions have been called]══════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
    public virtual void LateUpdate(){//-----------------------------------------------------------[CALCULATING FACING DIRECTIONS]
      //-------------------------------------------------------------------[here the conditions of the angle are assigned, whether it is to the right or the left]
      if(angle < 22.5f)
		{
		_facing = Facing.Up; //------------------------------------------------[if the character is looking between -22.5 and 22.5 degrees he will stay facing forward]
		}
		else if(angle < 67.5f) //---------------------------------------[if the character is looking less than 67.5 degrees and more than the previous range]
		{
		_facing = sign < 0 ? Facing.UpRight : Facing.UpLeft; //---------------[if the value is negative it changes to the other state]
      }
		else if(angle < 112.5f)//---------------------------------------[if the character is looking less than 112.5 degrees and more than the previous range]
		{
		_facing = sign < 0 ? Facing.Right : Facing.Left;//--------------------[if the value is negative it changes to the other state]
      }
		else if(angle < 157.5f)//---------------------------------------[if the character is looking less than 157.5 degrees and more than the previous range]
		{
		_facing = sign < 0 ? Facing.DownRight : Facing.DownLeft;//------------[if the value is negative it changes to the other state]
      }
		else
		{
		_facing = Facing.Down; //----------------------------------------------[if none of these conditions is fulfilled it means that the character is showing his back]
		}
		}
 // ╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

}