<img src="image\icon.png" title="" alt="" data-align="center">

# Double HSlider for Godot 4.4

# <u>双向滑动块Godot 4.4版本</u>

<img title="" src="image\pic1.jpg" alt="" data-align="center">

### ● Use 使用

1. Download the "<mark>addons</mark>" directory and enable the "Double HSlider" plugin.
   下载addons目录，启用Double HSlider插件。

2. Add child nodes in the scene: Control-->Range-->DoubleHSlider.
   在场景中添加子节点：Control-->Range-->DoubleHSlider。

3. Connect the "<mark>ValueChanged</mark>" signal of the control and receive two values, LowerValue and UpperValue.
   连接控件的ValueChanged信号，接收LowerValue和UpperValue两个值。

4. Run the project and enjoy.
   运行项目，开始浪。

![](image/pic2.png)

### ● Features 特征

1. Coding by C#, so the Godot .Net version is required to compile this plugin.(Maybe someday I will write a gdscript version.)
   c#代码，所以你必须下载Godot .Net版本才能编译此插件。（或许某一个我会编写一个gdscript版本）

2. This is runtime version, so the control can only be used during project execution. The editor version is currently being planned.
   运行时版本，所以控件只能在项目运行中使用。编辑器版本正在计划中。

3. Double slider control that can obtain two values, lower and upper.
   双向滑块控件，可获取lower和Upper两个数值。

4. Each part of the control can change its color, transparency, and even shape (rewriting the drawing code in the _daraw() function).
   控件的每个零件可改变颜色、透明度，甚至形状（在_Draw函数中重写绘制代码）。

5. Ensure that smaller values do not exceed larger values. There are numerical constraints between MinValue, MaxValue, Step, LowerValue, and UpperValue.
   确保较小值不会超过较大值，MinValue、MaxValue、Step、LowerValue、UpperValue之间都有数值约束。

6. Bi directional updates between user interface and parameters.
   界面和参数之间双向更新。

<img title="" src="image/pic3.png" alt="" width="208"> <img title="" src="image/pic4.png" alt="" width="206"> <img title="" src="image/pic5.png" alt="" width="208">

### ● Road Map 计划

1. Fix bug and Adapt to various application scenarios.
   修复bug，适应各种应用场景。

2. Create a runtime version of gdscript.
   制作gdscript运行时版本。

3. Create a gdscript editor version.
   制作gdscript编辑器版本。
- If you have any issues or bugs, please submit a 'New issue'.
  有任何问题或bug，请提交一个“New issue”。

```cs
using Godot;

public partial class Test : Control
{
    [Export]
    DoubleHSlider doubleHSlider;
    [Export] Label lowerV, upperV;

    public override void _Ready()
    {
        doubleHSlider.ValueChanged += DoubleSlider_ValueChanged;
    }

    private void DoubleSlider_ValueChanged(float lower, float upper)
    {
        lowerV.Text = lower.ToString();
        upperV.Text = upper.ToString();
    }
}
```
