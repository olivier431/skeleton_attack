using Godot;
using System;

public class World1 : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	Timer LightTimer;
	Light2D light;
	Vector2 lightScale = new Vector2(); 
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LightTimer = GetNode<Timer>("LightTimer");   
		light = GetNode<Light2D>("Player/Light2D");   
	}
	
	private void _on_LightPotion_body_entered(object body)
	{
		if(body.GetType().Name.ToString() == "Player"){
			LightTimer.Start();
			lightScale.x = 100;
			lightScale.y = 100;
			light.Scale = lightScale;
		}
	}
	
	private void _on_Portail_body_entered(object body)
	{
		if (body.GetType().Name.ToString() == "Player"){
				GetTree().ChangeScene("res://World2.tscn");
			}
	}
	
	private void _on_LightTimer_timeout()
	{
		lightScale.x = 0.6f;
		lightScale.y = 0.6f;
		light.Scale = lightScale;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}









