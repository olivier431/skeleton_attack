using Godot;
using System;

public class Skeleton : KinematicBody2D
{
	Vector2 MyScale = new Vector2(1, 1);
	int direction = 1;
	bool direction_flip = false;
	
	private enum state{
		IDLE,
		WALK,
		ATTACK,
		HURT,
		DEATH
	};
	
	private int life = 3;
	
	private state currentState = state.WALK;
	
   	const int GRAVITY = 20;
	const int MAXFALLSPEED = 200;
	const int MAXSPEED = 10;
	public Vector2 Velocity;
	
	
	KinematicBody2D skeleton;
	Sprite sprite;
	AnimationPlayer animationPlayer;
	AnimationTree animationTree;
	AnimationNodeStateMachinePlayback animationState;
	RayCast2D floorDetector;
	
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
		sprite = GetNode<Sprite>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		floorDetector = GetNode<RayCast2D>("Floor");
		skeleton = GetNode<KinematicBody2D>("Skeleton");
		Walk_enter();
	}

	public override void _PhysicsProcess(float delta)
	{      
		switch (currentState){
			case state.WALK:
				Detect_direction_change();
				Walk(delta);
				break;
		};
		
	}
	
	public void Walk(float delta){
		if(direction_flip){
			MyScale.x *= direction;
			MyScale.y = 3;
			
			Scale = MyScale;
			
			direction_flip = false;
			GD.Print($"Scale : {MyScale.y}" );
			
		}
		
		Velocity.y += GRAVITY;
		
		Velocity.x = MAXSPEED * direction;
		
		Velocity = MoveAndSlide(Velocity, Vector2.Up);
		//GD.Print($"Scale : {Scale.y}" );
	
	}
	
	public void Detect_direction_change(){
		if(IsOnFloor()){
			if(!floorDetector.IsColliding()){
				flip_direction();
				//GD.Print($"direction_flipped : {Scale}" );
				MyScale.y = 1;
			}
		}
	}
	
	public void flip_direction(){
		direction *= -1;
		direction_flip = true;
		
	}
	
	public void Walk_enter(){
		animationState.Travel("Walk");
		currentState = state.WALK;
	}

}
