using Godot;
using System;

public class lightPotion : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	private void _on_Area2D_body_entered(object body)
	{
		if(body.GetType().Name.ToString() == "Player"){
			
			QueueFree();
		}
	}
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}




