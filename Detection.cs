using Godot;
using System;

public class Detection : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	KinematicBody2D Player = null;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void _on_Detection_body_entered(KinematicBody2D body)
	{
		Player = body;
	}


private void _on_Detection_body_exited(KinematicBody2D body)
	{
		Player = null;
	}
	
	public bool see_player(){
		return Player != null;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}



