// A. Frame
//
// This example demonstrates how to use OFrame to build GUI layouts using OKitty.
// It covers:
//   - ScreenGui container
//   - Frame creation and parenting
//   - Scaling and offset layout vectors
//   - Frame rotation
//   - Layering (render order)
//   - Clipping (child rendering restriction)
//   - Extend and Shrink auto-size rules
//   - Real-time dynamic auto resizing caused by child movement
//
// Result:
//   A window containing three frames:
//     - A red parent frame
//     - A blue child frame that shrinks to fit its green child
//     - A green frame that moves around using absolute pixel offsets
//
//   Because the green frame moves in absolute coordinates,
//   the blue frame expands or shrinks depending on its autosize mode.

using OKitty;
using static OKitty.OkInterface;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OTest.Interfaces;

public static class Frame
{
    // Moving frame reference

    private static OFrame frameGreen;

    // Time accumulator for animation

    private static float t = 0;

    public static void Start()
    {
        // Create a window with a fixed border and a dark background.

        OWindowOptions options = new OWindowOptions()
        {
            Name = "Frame",
            BackgroundColor = OColor.Black,
            Border = OWindow.OWindowBorder.Fixed,
            PresentAfterCallback = true
        };

        OWindow window = new OWindow(options);

        // ScreenGui acts as the root container for all interface elements.

        OScreenGui gui = new OScreenGui(window.Scenes.Main);

        // Positioned using scale, sized using scale, slightly rotated.

        OFrame frameRed = new OFrame(gui, "RedFrame")
        {
            Position = OLayoutVector2<float, float>.Quarter, // 25% from top-left
            Size = OLayoutVector2<float, float>.Half, // 50% of window
            Rotation = 10,
            Layer = 1,
            BackgroundColor = OColor.Red,
            Clip = true,
            AutoSizeRule = IOInterface.OAutoSizeRule.None
        };

        // Notice: Uses shrink-both autosizing.

        OFrame frameBlue = new OFrame(frameRed, "BlueFrame")
        {
            Position = new OLayoutVector2<float, float>(new OVector2<float>(0.1f, 0.1f)), // 10% inset
            Size = new OLayoutVector2<float, float>(new OVector2<float>(0.8f, 0.8f)), // 80% of parent
            BackgroundColor = OColor.Blue,
            Layer = 2,
            Clip = true,
            AutoSizeRule = IOInterface.OAutoSizeRule.ShrinkBoth
        };

        // This frame moves in absolute pixels so autosize works properly.

        frameGreen = new OFrame(frameBlue, "GreenFrame")
        {
            Position = OLayoutVector2<float, float>.Zero, // offset-only positioning
            Size = new OLayoutVector2<float, float>(new OVector2<float>(100, 100)), // absolute size
            BackgroundColor = OColor.Green,
            Layer = 3,
            Clip = false
        };

        // Moves the green frame in absolute coordinates, causing
        // the blue frame to shrink or expand based on autosize rules.

        window.OnUpdate += Update;

        // Run Application

        window.Initialize();
        window.Run();
        window.Dispose();
    }


    // Instead of using scale-space movement (0–1),
    // use absolute coordinates inside the blue frame.
    //
    // This allows green frame to move outside, causing shrink/extend properly.

    private static void Update(double deltaTime)
    {
        t += (float)deltaTime;

        // Compute oscillating absolute offset

        float px = MathF.Sin(t) * 150f + 200f; // moves between ~50 and ~350 px
        float py = MathF.Cos(t) * 120f + 150f;

        // Apply absolute movement (offset only)
        
        frameGreen.Position = new OLayoutVector2<float, float>(
            OVector2<float>.Zero, // no scale
            new OVector2<float>(px, py) // absolute offset
        );
    }
}
