using Godot;

[Tool]
public partial class DoubleSliderPlugin : EditorPlugin
{
    public override void _EnterTree()
    {
        // 加载脚本和图标
        var script = GD.Load<Script>("res://addons/double_slider/DoubleSlider.cs");
       // var icon = GD.Load<Texture2D>("res://addons/double_slider/icon.png"); // 可选

        // 关键：注册为 Range 类型，Godot 会自动归类到 Slider 下
        AddCustomType("DoubleSlider", "Range", script, null);
    }

    public override void _ExitTree()
    {
        RemoveCustomType("DoubleSlider");
    }
}