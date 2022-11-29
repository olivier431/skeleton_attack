using Godot;
using System;

public class Victory : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void _on_MainMenu_pressed()
	{
		GetTree().ChangeScene("res://Menu.tscn");
	}
	
	private void _on_Quit_pressed()
	{
		GetTree().Quit();
	}
	
	private void _on_Restart_pressed()
	{
		GetTree().ChangeScene("res://World1.tscn");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}









