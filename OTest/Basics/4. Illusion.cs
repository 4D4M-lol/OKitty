// 4. Illusion
//
// This example demonstrates how to create a looping visual illusion
// by rendering multiple shrinking rectangles. Each rectangle gradually
// becomes smaller every frame, and once it becomes too small, it resets
// back to its original full size.
//
// The effect looks like a shrinking tunnel, especially when combined
// with a color gradient.
//
// Concepts shown in this example:
//
// - Rendering shapes using OShapes
// - Layered animations (multiple shapes updated every frame)
// - Using OColorSequence to generate smooth gradients
// - Recreating shapes each frame to animate size changes
// - Working with deltaTime for frame-rate-independent animation
//

using OKitty;
using static OKitty.OkInstance;
using static OKitty.OkInterface;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OTest.Basics;

public static class Illusion
{
    // Animation speed and window size

    private const int SPEED = 360;
    private const int WINDOW_WIDTH = 800;
    private const int WINDOW_HEIGHT = 600;

    // Window and animation data

    private static OWindow window;

    // Gradient used to color the rectangles (Nebula = blues, purples, reds)

    private static OColorSequence gradient = OColorSequence.Nebula;

    // List of all rectangles in the illusion

    private static List<OShapeInfo> rectangles = new List<OShapeInfo>();

    public static void Start()
    {
        // Configure the window
        
        OWindowOptions options = new OWindowOptions()
        {
            Name = "Illusion",
            Size = new OVector2<int>(WINDOW_WIDTH, WINDOW_HEIGHT),
            Border = OWindow.OWindowBorder.Fixed,
            BackgroundColor = OColor.Black,
            PresentAfterCallback = true  // Required for custom rendering inside OnUpdate
        };

        window = new OWindow(options);

        // Attach frame update callback

        window.OnUpdate += Update;

        // Create multiple rectangles of different sizes, centered

        for (int i = 0; i < 15; i++)
        {
            // Scale controls initial size variation

            float scale = 1f - (i * 0.08f);

            float width = WINDOW_WIDTH * scale;
            float height = WINDOW_HEIGHT * scale;

            OVector2<float> size = new OVector2<float>(width, height);

            // Centered positioning

            float posX = (WINDOW_WIDTH - width) / 2f;
            float posY = (WINDOW_HEIGHT - height) / 2f;

            OVector3<float> position = new OVector3<float>(posX, posY, 0);

            // Initial rectangle, white (color will shift during animation)

            OShapeInfo rectangle = OShapes.Rectangle(size, position, 0, OColor.White);

            rectangles.Add(rectangle);
        }

        // Run window lifecycle

        window.Initialize();
        window.Run();
        window.Dispose();
    }

    private static void Update(double deltaTime)
    {
        // Amount each rectangle shrinks each frame

        float shrinkAmount = 50f * (float)deltaTime;

        List<OShapeInfo> updated = new List<OShapeInfo>();

        // Update all rectangles

        foreach (OShapeInfo rectangle in rectangles)
        {
            // Compute the current size from the shape's lines

            OVector2<float> size = new OVector2<float>(
                rectangle.Lines[0].End.X - rectangle.Lines[0].Start.X,
                rectangle.Lines[1].End.Y - rectangle.Lines[0].Start.Y
            );

            float width = size.X - shrinkAmount;
            float height = size.Y - shrinkAmount;

            // Reset to full size when too small

            if (width < 1 || height < 1)
            {
                width = WINDOW_WIDTH;
                height = WINDOW_HEIGHT;
            }

            // Recalculate size and centered position

            OVector2<float> newSize = new OVector2<float>(width, height);
            float posX = (WINDOW_WIDTH - width) / 2f;
            float posY = (WINDOW_HEIGHT - height) / 2f;

            OVector3<float> newPosition = new OVector3<float>(posX, posY, 0);

            // Gradient color based on size ratio
            
            float time = width / WINDOW_WIDTH;
            OColor color = gradient.GetValue(time);

            // Recreate the rectangle at the new size

            OShapeInfo newShape = OShapes.Rectangle(newSize, newPosition, 0, color);

            updated.Add(newShape);
        }

        // Apply updated list

        rectangles = updated;

        // Render all rectangles
        
        foreach (OShapeInfo rectangle in rectangles)
            window.Renderer.RenderLines(rectangle.Lines);
    }
}