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
    //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      [Header("╔══════════════════════╗")][Space(-13)]
      [Header("║              ２-D CHARACTERS              ║")][Space(-13)]
      [Header("║                IN A ３-D WORLD                ║")][Space(-13)]
      [Header("║                                                                  ║")][Space(-13)]
      [Header("║                By∼@DANIEL3VEL             ║")][Space(-13)]
      [Header("╚══════════════════════╝")][Space(10)]
    //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

      [Tooltip("Drag the Main Camera object here")]
      public Transform TheCamera;//---------------------------------------------------[transform called TheCamera is created and put serializeField to get the camera position in this code]              
      [Tooltip("Drag the object that has the Animator and Sprite Renderer components here")]
      public GameObject billboard;//--------------------------------------------------[the object that contains the Animator and Sprite Renderer components, our "BillBoard"]
      
     //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      [Header("Instructions:")][Space(-13)] 
      [Header("┌─┬─┬─┐  0 is the front of '웃'")][Space(-19)]
      [Header("  ７  ０  １")][Space(-19)]
      [Header("├─┼─┼─┤  '웃' is this gameObject")][Space(-19)]
      [Header("  ６  웃  ２")][Space(-19)]//------------------------------------------[this is just a small graphic to explain the logic of the script, it makes more sense in the unity inspector interface]
      [Header("├─┼─┼─┤  the order of the sections")][Space(-19)]
      [Header("  ５  ４  ３")][Space(-19)]
      [Header("└─┴─┴─┘  is clockwise ↻ from 0 to 7")][Space(-13)]
      [Header("웃 change depending where # │̲̲̅̅⦿̲̅°̲̅l̲̅ is")]
      //-----------------------------------------------------⦿°Ṑṏ [̲̅  [⦿°]̲̅
     //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      
      [Header("Rendering: Animator/Sprites")]
      [Tooltip("Enable or Disable the animator to change between sprite rendering to animations")]
      public bool useAnimator = false;//-------------------------------------------------[this bool is for Enable or Disable the animator to change between sprite rendering to animations]
      [Tooltip("if enabled you can render a character with only 4 directions instead of 8")]
      public bool use4Directions = false;//----------------------------------------------[if true you can render a character with only 4 directions instead of 8]

      public List<Sprite> sprites = new List<Sprite>(8);//------------------------------[list of sprites to contain the 2d images]
      [HideInInspector]
      public List<Sprite> HiddenSprites8 = new List<Sprite>(8);//-----------------------[hidden list of sprites to save the 8 direction configuration]
      [HideInInspector]
      public List<Sprite> HiddenSprites4 = new List<Sprite>(4);//-----------------------[hidden list of sprites to save the 4 direction configuration]
      
      [Header("Character: Simetric/Asimetric")]
      [Tooltip("the 2,3,4 sprites/animations will be flipX and shown when the 6,7,8 are called")]
      public bool useMirror;//--------------------------------------------------------[public bool to change the sprite rendering method, fom using 5 sprites with mirror to using 8 sprites without the mirror]
      
      enum Facing {Up, UpRight, Right, DownRight, Down, DownLeft, Left, UpLeft};//----[an enumeration where we create and name each of the directions we want, in this case 8 directions]       
      private Facing _facing;//-------------------------------------------------------[variable of the Facing type that we just invented, and assign it a default state]                                                   
     
      private float angle;//----------------------------------------------------------[variable float is created to monitor the value of an angle]
      private float sign=1;//---------------------------------------------------------[variable to tell if the angle is positive or negative]
      private float signAngle;//------------------------------------------------------[did we really need this?, is just to display the Real value of the angle, for now is just for debugging]
      
      private Animator animator;//----------------------------------------------------[animator variable to get the animator component of the billboard]
      private SpriteRenderer _sprite;//-----------------------------------------------[spriteRenderer variable to assign the sprite renderer script of an object to this code]                                 
      private Transform _billboard;//-------------------------------------------------[another transform variable, for the object where the bildboard is]                                    
      private Transform _t;//---------------------------------------------------------[another transform variable, for the object where this script is]
      private Vector3 direction;//----------------------------------------------------[the direction of the camera is relative to this object]
      private bool tOutcome;//--------------------------------------------------------[this bool is used to tell if the animator controller has a certain parameter with the function ContainsParam]

      

 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※Unity Editor settings※』═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      void OnValidate(){//-----------------------------------[this is just to assing some values at the beggining in the unity editor]
         
         if (billboard == null){
            Debug.Log("you need to assing the object billboard that has the Animator and Sprite Renderer components to use this script");
         }else{
            animator = billboard.GetComponent<Animator>();
         }
      //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
         
           //------------------------------------------------[lock the elements of the list in unity inspector, because the script is meant to only work with 4 and 8 faces, is just for the Images, not necesary for the animation]
             
         if (use4Directions==true){
            if (sprites.Count == 4) {
               HiddenSprites4.Clear();
               HiddenSprites4.InsertRange(0,sprites);
            }else if (sprites.Count != 4) {
               sprites.Clear();
               sprites.InsertRange(0,HiddenSprites4);
            }
         }//-------------------------------------------------[this parts are to exchange data between the hiden lists and the list of sprites, so you can have a profile for each of the methods without reseting the lists]
         if (use4Directions==false){
            
            if (sprites.Count == 8) {
               HiddenSprites8.Clear();
               HiddenSprites8.InsertRange(0,sprites);
            }else if (sprites.Count != 8) {
               sprites.Clear();
               sprites.InsertRange(0,HiddenSprites8);

            }
         }
        
      //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
         
         
            //--------------------------------------------------------------------------[having lesss tha 8 spaces in the array, with "useMirror" disabled and "useAnimator" disabled gives an error witout this]
         
         if(useAnimator){ 
            if(animator != null){
               
               animator.enabled = false;
               animator.enabled = true;//---------------------------------------------[this nonsense is just to refresh the Animator component, cause it doesn't refresh by default in the unity editor mode] 
               
            }
            
            if (animator != null && animator.isActiveAndEnabled){
               
               tOutcome = ContainsParam("direction");
               if (tOutcome == false){ 
                  Debug.Log("you need to create a INT parameter called 'direction' in the animatorController of the billdoard object in order to work with the script '2D character In a 3D world' animation feature");
                  }
            
            }
         }else{
            animator.enabled = false;//-----------------------------------------------[disable the animator, because the animator overwrite the sprite rendering of this script]
         }
      }
 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※ ContainsParam function ※』═════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      public bool ContainsParam(  string _ParamName ){//--------------------------------------[check if in the animator controller has any parameter to work with]
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

         _t = transform;//------------------------------------------------------------[we assign the transformation data of the object in which this script is located to the variable "_t" ]
         
         if (billboard != null){//----------------------------------------------------[if we detect the billboard then get the animator, the spriterenderer and transform components in these variables]
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
         
         Vector3 forward = _t.forward;//----------------------------------------------[Get the actual forward vector of this object and keep it in the variable "foward"]

         forward.y = 0;//-------------------------------------------------------------[In the "forward" vector, we set the "y" direction to be equal to 0, so that the vector becomes a two-dimensional vector in the "x" and "z" plane]
         
         Vector3 direction2 = _t.InverseTransformPoint(TheCamera.position);//---------[another vector, calculates the relative position of one object (this) to the other (camera)] [InverseTransformPoint Transforms position from world space to local space] 
         
         sign = (direction2.x >= 0) ? -1 : 1;//---------------------------------------[variable to get the sign of the angle (positive or negative)]
         
         angle = Vector3.Angle(direction, forward);//---------------------------------[we use it to know the angle, calculating the smallest angle between the 2 vectors, The result is never greater than 180 degrees]
         
         signAngle = angle * sign;//--------------------------------------------------[this is to know the full angle from -180 to 180, it can be easily turn into 0 to 360 by just adding 180]
      
       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
       
         direction = TheCamera.position - _t.position ;//-----------------------------[another vector, calculates the relative position of one object (this) to the other (camera)]  
         direction.y = 0;//-----------------------------------------------------------[0 in the "Y" axis of the 3D vector "direction" to make it a 2D vector on X,Z]
        _billboard.rotation = Quaternion.LookRotation(-direction, _t.up);//-----------[use the inverse of the targetpoint vector and the upwards vector of THIS object, to create a quaternion to look at the rotation of this vectors, then order the billboard to follow this same rotation]
       
       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

         if(useMirror){
            Miror();//----------------------------------------------------------------[use the miror function]
         }else{
            _sprite.flipX = false;//--------------------------------------------------[if you are using animation dont worry, the animatior overrides spriteRenderer coming from a code like this]
         }

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────

       if (use4Directions == false){
         if (animator.isActiveAndEnabled == true){
            animator.SetInteger("direction",(int)_facing);//--------------------------[the value of the facing operation is send to the parameter 'direction' in the animatorController of the billboard object]
         }else{
            _sprite.sprite = sprites[(int)_facing];//---------------------------------[the value of the facing operation is used to select a sprite from the list 'sprites' and render it in the spriteRenderer component]
         }
       }else if(use4Directions == true){
         if (animator.isActiveAndEnabled == true){
            animator.SetInteger("direction",(int)_facing);//--------------------------[the value of the facing operation is send to the parameter 'direction' in the animatorController of the billboard object]
         }else{
            _sprite.sprite = sprites[(int)_facing/2];//-------------------------------[the value of the facing operation is divided by 2...  facing can only be [0,2,4,6] thanks to "use 4 directions" and divided by 2 we get [0,1,2,3] to fit the small list]
         }
       }
       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
        
         /*//Debbuging for the angles
         animator.SetFloat("angles",signAngle);
         Debug.DrawRay(transform.position, direction,Color.green);
         Debug.DrawRay(transform.position, direction2,Color.blue);  */
        
        //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      }

 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
 
 // ╔═════════════════════════════════════════════════════════════[LateUpdate is called after all Update functions have been called]══════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
    public virtual void LateUpdate(){//-----------------------------------------------------------[CALCULATING FACING DIRECTIONS]

      if (use4Directions == false){//⦿──────────────────────────────────────────[here the 8 directions are assigned, and if its to the right or the left]────────────────────────────────────────────────────────────────────────────
         //-------------------------------------------------------------------
         if(angle < 22.5f)
   	 	{
	   	  _facing = Facing.Up; //------------------------------------------------[if the character is looking between 0 and 22.5 degrees the facing is Up]
	   	}
   		else if(angle < 67.5f) //---------------------------------------[if the character is looking less than 67.5 degrees and more than 22.5 degrees then...]
   		{
   		  _facing = sign < 0 ? Facing.UpRight : Facing.UpLeft; //---------------[if the value is positive then facing is Up-Right, if the value is negative facing changes to Up-Left]
         }
   		else if(angle < 112.5f)//---------------------------------------[if the character is looking less than 112.5 degrees and more than 67.5 degrees then...]
   		{
   		  _facing = sign < 0 ? Facing.Right : Facing.Left;//--------------------[if the value is positive then facing is Right, if the value is negative facing changes to Left]
         }
   		  else if(angle < 157.5f)//---------------------------------------[if the character is looking less than 157.5 degrees and more than 112.5 degrees then...]
   		{
   		  _facing = sign < 0 ? Facing.DownRight : Facing.DownLeft;//------------[if the value is positive then facing is Down-Right, if the value is negative facing changes to Down-Left]
         }
   		else
   		{
   		  _facing = Facing.Down; //----------------------------------------------[if none of these conditions is fulfilled it means that the character is showing its back]
   		}

      }else if(use4Directions == true){//⦿─────────────────────────────────────[here the 4 directions are assigned, and if its to the right or the left]─────────────────────────────────────────────────────────────────────────────
   
         if(angle < 45.0f)
		   {
   		  _facing = Facing.Up; //------------------------------------------------[if the character is looking between 0º and 45º degrees the facing is Up]
   		}
   		else if(angle < 135.0f)//----------------------------------------------[if the character is looking less than 135º degrees and more than 45º degrees then...]
   		{
   		  _facing = sign < 0 ? Facing.Right : Facing.Left;//---------------------------[if the value is positive then facing is Right, if the value is negative facing changes to Left]
         }
   		else
   		{
   		  _facing = Facing.Down; //----------------------------------------------[if none of these conditions is fulfilled it means that the character is showing its back]
   		}
      }
      
	 }
 // ╚═════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

 // ╔════════════════════════════════════════════════════════════『※ Mirror function ※』══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
 //⦿
      public void Miror(){

       //⦿──────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
          if (use4Directions == false){//-------------------------------------------------[for 8 faces]
            switch(_facing ){
             case Facing.DownLeft://--------------------------------------------------[if facing downleft is detected then change the sprite to render downright and flip the sprite horizontally]
             _facing  = Facing.DownRight;
             _sprite.flipX = true;
             break;
             case Facing.Left://------------------------------------------------------[if facing left is detected then change the sprite to render right and flip the sprite horizontally]
             _facing  = Facing.Right;
             _sprite.flipX = true;
             break;
             case Facing.UpLeft://----------------------------------------------------[if facing UpLeft is detected then change the sprite to render UpRight and flip the sprite horizontally]
             _facing  = Facing.UpRight;
             _sprite.flipX = true;
             break;
             default://---------------------------------------------------------------[if none of these cases are detected then don't flip the sprite]
             _sprite.flipX = false;
             break;
           }
          }

          /*⦿Note )⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╮
            this switch-case is just because you can't fill a list of 8 spaces with only 5 objects.(why would you want that?- well, to make simmetric characters with less spritesheet/animations)
            so when the left side sprites are called the right sides will be shown and then flipped horizontally.
            this part of the script is made so you can work with a minumun of 5 sprites/animations, if "useMirror" is active the last 3 sprites never render.
            or instead, if "useMirror" is dissable you can work with more detailed(assimetric) characters using 8 sprites/animations.
            simetric characters looks the same in both left and rigth sides of their bodies / assimetric characters looks diferent in each side and requires more sprites.
           ╰⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╯*/
       
          if (use4Directions == true){//-------------------------------------------------[for 4 faces]

            switch(_facing ){
             
             case Facing.Left://------------------------------------------------------[if facing left is detected then change the sprite to render right and flip the sprite horizontally]
             _facing  = Facing.Right;
             _sprite.flipX = true;
             break;

             default://---------------------------------------------------------------[if none of these cases are detected then don't flip the sprite]
             _sprite.flipX = false;
             break;
           }
          }
          /*⦿Note )⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╮
            the same but this just flips the right face to become the left one, to work with just 3 sprites per billboard or for the animation to work 3 sprites per frame
          ╰⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯⎯╯*/

           
        //⦿─────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────
      }
 // ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝

}