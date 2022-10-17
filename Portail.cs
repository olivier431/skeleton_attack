using Godot;
using System;

public class Portail : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public override void _Process(float delta)
	{
	 	var frames = GetOverlappingBodies();
		foreach (var frame in frames){
			GD.Print(frame);
//			if(frame.GetType() == KinematicBody2D){
//				GetTree().ChangeScene("World2.tscn");
//			}
		}
		
		
  	}
}

 
