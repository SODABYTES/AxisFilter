extends CanvasLayer
@onready var displayRaw:Node2D = $Anchor/DisplayRaw
@onready var displayCircle:Node2D = $Anchor/DisplayCircle
@onready var displayCross:Node2D = $Anchor/DisplayCross
@onready var displaySquare:Node2D = $Anchor/DisplaySquare
@onready var fade:ColorRect = $Fade
var initialTimer:float = 0.5

func _physics_process(delta):
	updateDisplay()
	if initialTimer >= 0:
		initialTimer -= 1 * delta
	else:
		fade.modulate.a = maxf(fade.modulate.a - 0.25 * delta, 0)

func updateDisplay():
	const zoneMin:float = 0.1
	const zoneMax:float = 0.9
	var raw:Vector2 = Vector2(Input.get_joy_axis(0, JOY_AXIS_LEFT_X), Input.get_joy_axis(0, JOY_AXIS_LEFT_Y))
	displayRaw.axis = raw
	displayCircle.axis = AxisFilter.ApplyCircleDeadzone(raw, zoneMin, zoneMax)
	displayCross.axis = AxisFilter.ApplyCrossDeadzone(raw, zoneMin, zoneMax)
	displaySquare.axis = AxisFilter.ApplySquareDeadzone(raw, zoneMin, zoneMax)
