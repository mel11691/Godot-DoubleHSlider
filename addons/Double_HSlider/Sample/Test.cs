using Godot;
using System;

public partial class Test : Control
{
    [Export]
    DoubleSlider doubleSlider;

    public override void _Ready()
    {
        doubleSlider.ValueChanged += DoubleSlider_ValueChanged;
    }

    private void DoubleSlider_ValueChanged(float lower, float upper)
    {
        GD.Print(lower,":", upper);
    }
}
