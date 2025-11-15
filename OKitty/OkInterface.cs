// Imports

using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkStyling;

namespace OKitty;

// OkInterface

public class OkInterface
{
    // Interfaces

    public interface IOInterface : IOInstance
    {
        // Enums

        [Flags]
        public enum OAutoSizeRule
        {
            None = 0,
            Horizontal = 1 << 0,
            Vertical = 1 << 1,
            Both = Horizontal | Vertical
        }
        
        // Properties

        public bool Active { get; set; }
        public bool Visible { get; set; }
        public OVector2<float> AbsoluteSize { get; }
        public OVector2<float> AbsolutePosition { get; }
        public OAutoSizeRule AutoSizeRule { get; set; }
        public OLayoutVector2<float, float> Size { get; set; }
        public OLayoutVector2<float, float> Position { get; set; }
        public float Rotation { get; set; }
        public int Layer { get; set; }
        public OColor BackgroundColor { get; set; }
        public float BackgroundOpacity { get; set; }
        public bool Clip { get; set; }
    }
}