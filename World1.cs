using Godot;
using System;

public class World1 : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	AudioStreamPlayer Music;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Music = GetNode<AudioStreamPlayer>("MusicWorld1");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
  	public override void _Process(float delta)
	{
	 	if (Input.IsActionJustPressed("MusicPause"))
			{
				Music.Playing = !Music.Playing;
			}
	}
}
