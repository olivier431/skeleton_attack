using Godot;
using System;

public class OptionMenu : Control
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void _on_BackToMainMenu_pressed()
	{
		GetTree().ChangeScene("res://Menu.tscn");
	}
	
	private void _on_CheckBox_pressed()
	{
		Music.stopMusic();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}






