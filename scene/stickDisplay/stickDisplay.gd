extends Node2D
@export var axis:Vector2
@export var square:bool = false
@export var gateName:String

@onready var circleGate:Node2D = $CircleGate
@onready var squareGate:Node2D = $SquareGate
@onready var stickPosition:Node2D = $StickPosition
@onready var stickPositionPrevious:Node2D = $StickPositionPrevious
@onready var streak:TextureRect = $Streak
@onready var label:Label = $Label

var wasAxisMax:bool = true
var gateOpacity:float = 0.125

func _ready():
	DisplayServer.mouse_set_mode(DisplayServer.MOUSE_MODE_HIDDEN)
	if square:
		squareGate.show()
		circleGate.hide()
	label.text = gateName

func _physics_process(delta):
	update(delta)

func update(delta:float):
	stickPositionPrevious.position = stickPosition.position
	stickPosition.position = axis * (128 - 0)
	gateOpacity = maxf(gateOpacity - 0.375 * delta, 0.125)
	var axisMaxed:bool = false
	const maxThreshold:float = 0.999
	if square:
		if abs(axis.x) >= maxThreshold or abs(axis.y) >= maxThreshold:
			axisMaxed = true
	else:
		if axis.length() >= maxThreshold:
			axisMaxed = true
	axisMaxed = false #lol
	if axisMaxed:
		if !wasAxisMax:
			gateOpacity = 0.25
		wasAxisMax = true
	else:
		wasAxisMax = false
	circleGate.modulate.r = gateOpacity
	squareGate.modulate.r = gateOpacity
	
	streak.position.x = stickPosition.position.x
	streak.position.y = stickPosition.position.y - 8
	
	var difference:Vector2 = stickPositionPrevious.position - stickPosition.position
	
	streak.rotation = difference.angle()
	streak.size.x = difference.length()
