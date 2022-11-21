using Godot;
using System;

public class Background1 : Godot.ParallaxBackground
{
	[Export]
	float Cloud_Speed = -15f;

	ParallaxLayer cloudLayer;
	Sprite cloudSprite;

	public override void _Ready()
	{        
		cloudLayer = GetNode<ParallaxLayer>("Cloud");
		cloudSprite = cloudLayer.GetNode<Sprite>("Sprite");
	}

	public override void _Process(float delta)
	{
	   cloudLayer.MotionOffset = 
		new Vector2(
			cloudLayer.MotionOffset.x + (Cloud_Speed * delta),
			 0);          
	}
}
