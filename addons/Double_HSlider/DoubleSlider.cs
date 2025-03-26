using Godot;

[Tool]
[GlobalClass]
public partial class DoubleSlider : Range
{
    // 导出属性，可在编辑器中调整
    [Export] public float LowerValue = 20.0f;
    [Export] public float UpperValue = 80.0f;
    [Export] public Color SliderColor = new Color(0.5f, 0.5f, 0.5f);
    [Export] public Color RangeColor = new Color(0.2f, 0.6f, 1.0f);
    [Export] public Color GrabberColor = new Color(1.0f, 1.0f, 1.0f);
    [Export] public float GrabberSize = 10.0f;
    [Export] public float SliderHeight = 4.0f;

   
    // 信号定义
    [Signal] public delegate void ValueChangedEventHandler(float lower, float upper);

    private bool _draggingLower = false;
    private bool _draggingUpper = false;

    public override void _Ready()
    {
        // 确保初始值有效
        ClampValues();
    }

    public override void _Draw()
    {
        var size = GetRect().Size;
        var sliderY = size.Y / 2;

        // 绘制背景滑块
        DrawRect(new Rect2(0, sliderY - SliderHeight / 2, size.X, SliderHeight), SliderColor);

        // 计算手柄位置
        var lowerPos = GetPositionFromValue(LowerValue, size.X);
        var upperPos = GetPositionFromValue(UpperValue, size.X);

        // 绘制范围
        DrawRect(new Rect2(lowerPos, sliderY - SliderHeight / 2, upperPos - lowerPos, SliderHeight), RangeColor);

        // 绘制手柄
        DrawCircle(new Vector2(lowerPos, sliderY), GrabberSize, GrabberColor);
        DrawCircle(new Vector2(upperPos, sliderY), GrabberSize, GrabberColor);
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.Pressed)
                {
                    // 检查点击了哪个手柄
                    var mousePos = GetLocalMousePosition();
                    var lowerPos = GetPositionFromValue(LowerValue, GetRect().Size.X);
                    var upperPos = GetPositionFromValue(UpperValue, GetRect().Size.X);
                    var sliderY = GetRect().Size.Y / 2;

                    _draggingLower = (mousePos.DistanceTo(new Vector2(lowerPos, sliderY)) <= GrabberSize);
                    _draggingUpper = !_draggingLower && (mousePos.DistanceTo(new Vector2(upperPos, sliderY)) <= GrabberSize);
                }
                else
                {
                    _draggingLower = false;
                    _draggingUpper = false;
                }
            }
        }
        else if (@event is InputEventMouseMotion motionEvent && (_draggingLower || _draggingUpper))
        {
            var size = GetRect().Size;
            var mouseX = Mathf.Clamp(GetLocalMousePosition().X, 0, size.X);
            var newValue = GetValueFromPosition(mouseX, size.X);

            if (_draggingLower)
            {
                LowerValue = Mathf.Min(newValue, UpperValue);
            }
            else if (_draggingUpper)
            {
                UpperValue = Mathf.Max(newValue, LowerValue);
            }

            ClampValues();
            QueueRedraw();
            EmitSignal(SignalName.ValueChanged, LowerValue, UpperValue);
        }
    }

    private float GetPositionFromValue(float value, float width)
    {
        return (float)((value - MinValue) / (MaxValue - MinValue) * width);
    }

    private float GetValueFromPosition(float position, float width)
    {
        var normalized = position / width;
        var value = MinValue + normalized * (MaxValue - MinValue);
        return ApplyStep((float)value);
    }

    private float ApplyStep(float value)
    {
        if (Step <= 0) return value;
          return (float)((float)Mathf.Round(value / Step) * Step);
    }

    private void ClampValues()
    {
        LowerValue = (float)Mathf.Clamp(ApplyStep(LowerValue), MinValue, MaxValue);
        UpperValue = (float)Mathf.Clamp(ApplyStep(UpperValue), MinValue, MaxValue);

        if (LowerValue > UpperValue)
        {
            LowerValue = UpperValue;
        }
    }
}