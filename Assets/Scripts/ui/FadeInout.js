//--------------------------------------------------------------------
//                        Public parameters
//--------------------------------------------------------------------
 
public var fadeOutTexture : Texture2D;
public var fadeSpeed = 0.2;
 
var drawDepth = -1000;
 
//--------------------------------------------------------------------
//                       Private variables
//--------------------------------------------------------------------
 
 var alpha = 0.0; 
 var fadeDir = 1.0;
 
//--------------------------------------------------------------------
//                       Runtime functions
//--------------------------------------------------------------------
 
function OnGUI()
{
	alpha += fadeDir * fadeSpeed * Time.deltaTime;	
	alpha = Mathf.Clamp01(alpha);	
 
	GUI.color.a = alpha;
 
	GUI.depth = drawDepth;
	GUI.DrawTexture(Rect(0, 0, Screen.width, Screen.height), fadeOutTexture, ScaleMode.ScaleToFit);
}
  
function fadeIn()
{
	fadeDir = -1.0;	
}
  
function fadeOut()
{
	fadeDir = 1.0;	
}
 
function Start()
{

}
