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
	const int MAXSPEED = 30;
	public Vector2 Velocity;
	
	bool is_player_nearby = false;
	KinematicBody2D player;
	
	KinematicBody2D skeleton;
	Sprite sprite;
	AnimationPlayer animationPlayer;
	AnimationTree animationTree;
	AnimationNodeStateMachinePlayback animationState;
	RayCast2D floorDetector;
	RayCast2D wallDetector;
	
	public override void _Ready()
	{
		Velocity = Vector2.Zero;
		sprite = GetNode<Sprite>("Sprite");
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");
		floorDetector = GetNode<RayCast2D>("Floor");
		wallDetector = GetNode<RayCast2D>("Wall");
		skeleton = GetNode<KinematicBody2D>("Skeleton");
		Walk_enter();
	}
	
	public void _on_PlayerDetector_body_entered(object body)
	{
		if(body.GetType().Name.ToString() == "Player"){
			is_player_nearby = true;
			player = (KinematicBody2D)body;
			Attack_enter();
		}
	}


public void _on_PlayerDetector_body_exited(object body)
{
	is_player_nearby = false;
	player = null;
	Walk_enter();
}


	public override void _PhysicsProcess(float delta)
	{      
		switch (currentState){
			case state.WALK:
				Detect_direction_change();
				Walk(delta);
				break;
			case state.ATTACK:
				Attack(delta);
				break;	
		};
		
	}
	
	public void Walk(float delta){
		if(direction_flip){
			direction_flip = false;
		}
		
		Velocity.y += GRAVITY;

		if(Velocity.y > MAXFALLSPEED) {
			Velocity.y = MAXFALLSPEED;
		}
		Velocity.x = MAXSPEED * direction;
		
		Velocity = MoveAndSlide(Velocity, Vector2.Up);
	
	}
	
	public void Attack(float delta){
		if(is_player_nearby){
			animationState.Travel("Attack");
		}
	
	}
	
	public void Detect_direction_change(){
		if(IsOnFloor()){
			if(!(floorDetector.IsColliding()) || (wallDetector.IsColliding())){
				flip_direction();
				GD.Print(direction);
				MyScale.x *= direction;
				Scale = MyScale;
				GD.Print(MyScale);
				GD.Print(Scale);
				
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
	
	public void Attack_enter(){
		animationState.Travel("Attack");
		currentState = state.ATTACK;
	}

}


