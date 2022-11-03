using Godot;
using System;

public class DeathScene : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	private void _on_RestartButton_pressed()
	{
		GetTree().ChangeScene("res://World1.tscn");
	}
	
	private void _on_QuitButton_pressed()
	{
		GetTree().Quit();
	}
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}




