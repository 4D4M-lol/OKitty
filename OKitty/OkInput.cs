// Imports

using System.Data;
using static OKitty.OkMath;
using static OKitty.OkScript;
using SDL3;

namespace OKitty;

// OkInput

public static class OkInput
{
    // Structs
    
    public class OKeyboard
    {
        // Enums
        
        [Flags]
        public enum OModifierKey
        {
            None = 0,
            LeftShift = 1 << 0,
            RightShift = 1 << 1,
            Shift = LeftShift | RightShift,
            Level5Shift = 1 << 2,
            LeftControl = 1 << 3,
            RightControl = 1 << 4,
            Control = LeftControl | RightControl,
            LeftAlt = 1 << 5,
            RightAlt = 1 << 6,
            Alt = LeftAlt | RightAlt,
            LeftGui = 1 << 7,
            RightGui = 1 << 8,
            Gui = LeftGui | RightGui,
            ScrollLock = 1 << 9,
            CapsLock = 1 << 10,
            NumLock = 1 << 11,
            Mode = 1 << 12
        }

        public enum OKeyboardKey
        {
            Unknown = 0x00000000,
            Return = 0x0000000D,
            Escape = 0x0000001B,
            Backspace = 0x00000008,
            Tab = 0x00000009,
            Space = 0x00000020,
            Exclamation = 0x00000021,
            DoubleQuote = 0x00000022,
            Hashtag = 0x00000023,
            Dollar = 0x00000024,
            Percent = 0x00000025,
            Ampersand = 0x00000026,
            SingleQuote = 0x00000027,
            LeftParentheses = 0x00000028,
            RightParentheses = 0x00000029,
            Asterisk = 0x0000002A,
            Plus = 0x0000002B,
            Comma = 0x0000002C,
            Minus = 0x0000002D,
            Period = 0x0000002E,
            Slash = 0x0000002F,
            Number0 = 0x00000030,
            Number1 = 0x00000031,
            Number2 = 0x00000032,
            Number3 = 0x00000033,
            Number4 = 0x00000034,
            Number5 = 0x00000035,
            Number6 = 0x00000036,
            Number7 = 0x00000037,
            Number8 = 0x00000038,
            Number9 = 0x00000039,
            Colon = 0x0000003A,
            Semicolon = 0x0000003B,
            Less = 0x0000003C,
            Equals = 0x0000003D,
            Greater = 0x0000003E,
            Question = 0x0000003F,
            At = 0x00000040,
            LeftBracket = 0x0000005B,
            Backslash = 0x0000005C,
            RightBracket = 0x0000005D,
            Caret = 0x0000005E,
            Underscore = 0x0000005F,
            Backtick = 0x00000060,
            LetterA = 0x00000061,
            LetterB = 0x00000062,
            LetterC = 0x00000063,
            LetterD = 0x00000064,
            LetterE = 0x00000065,
            LetterF = 0x00000066,
            LetterG = 0x00000067,
            LetterH = 0x00000068,
            LetterI = 0x00000069,
            LetterJ = 0x0000006A,
            LetterK = 0x0000006B,
            LetterL = 0x0000006C,
            LetterM = 0x0000006D,
            LetterN = 0x0000006E,
            LetterO = 0x0000006F,
            LetterP = 0x00000070,
            LetterQ = 0x00000071,
            LetterR = 0x00000072,
            LetterS = 0x00000073,
            LetterT = 0x00000074,
            LetterU = 0x00000075,
            LetterV = 0x00000076,
            LetterW = 0x00000077,
            LetterX = 0x00000078,
            LetterY = 0x00000079,
            LetterZ = 0x0000007A,
            LeftBrace = 0x0000007B,
            Pipe = 0x0000007C,
            RightBrace = 0x0000007D,
            Tilde = 0x0000007E,
            Delete = 0x0000007F,
            PlusMinus = 0x000000B1,
            CapsLock = 0x40000039,
            Function1 = 0x4000003A,
            Function2 = 0x4000003B,
            Function3 = 0x4000003C,
            Function4 = 0x4000003D,
            Function5 = 0x4000003E,
            Function6 = 0x4000003F,
            Function7 = 0x40000040,
            Function8 = 0x40000041,
            Function9 = 0x40000042,
            Function10 = 0x40000043,
            Function11 = 0x40000044,
            Function12 = 0x40000045,
            PrintScreen = 0x40000046,
            ScrollLock = 0x40000047,
            Pause = 0x40000048,
            Insert = 0x40000049,
            Home = 0x4000004A,
            PageUp = 0x4000004B,
            End = 0x4000004D,
            PageDown = 0x4000004E,
            ArrowRight = 0x4000004F,
            ArrowLeft = 0x40000050,
            ArrowDown = 0x40000051,
            ArrowUp = 0x40000052,
            NumLockClear = 0x40000053,
            KeypadDivide = 0x40000054,
            KeypadMultiply = 0x40000055,
            KeypadMinus = 0x40000056,
            KeypadPlus = 0x40000057,
            KeypadEnter = 0x40000058,
            KeypadNumber1 = 0x40000059,
            KeypadNumber2 = 0x4000005A,
            KeypadNumber3 = 0x4000005B,
            KeypadNumber4 = 0x4000005C,
            KeypadNumber5 = 0x4000005D,
            KeypadNumber6 = 0x4000005E,
            KeypadNumber7 = 0x4000005F,
            KeypadNumber8 = 0x40000060,
            KeypadNumber9 = 0x40000061,
            KeypadNumber0 = 0x40000062,
            KeypadPeriod = 0x40000063,
            Application = 0x40000065,
            Power = 0x40000066,
            KeypadEquals = 0x40000067,
            Function13 = 0x40000068,
            Function14 = 0x40000069,
            Function15 = 0x4000006A,
            Function16 = 0x4000006B,
            Function17 = 0x4000006C,
            Function18 = 0x4000006D,
            Function19 = 0x4000006E,
            Function20 = 0x4000006F,
            Function21 = 0x40000070,
            Function22 = 0x40000071,
            Function23 = 0x40000072,
            Function24 = 0x40000073,
            Execute = 0x40000074,
            Help = 0x40000075,
            Menu = 0x40000076,
            Select = 0x40000077,
            Stop = 0x40000078,
            Again = 0x40000079,
            Undo = 0x4000007A,
            Cut = 0x4000007B,
            Copy = 0x4000007C,
            Paste = 0x4000007D,
            Find = 0x4000007E,
            Mute = 0x4000007F,
            VolumeUp = 0x40000080,
            VolumeDown = 0x40000081,
            KeypadComa = 0x40000085,
            KeypadEqualsAs400 = 0x40000086,
            Alterase = 0x40000099,
            SysReq = 0x4000009A,
            Cancel = 0x4000009B,
            Clear = 0x4000009C,
            Prior = 0x4000009D,
            Return2 = 0x4000009E,
            Separator = 0x4000009F,
            Out = 0x400000A0,
            Oper = 0x400000A1,
            ClearAgain = 0x400000A2,
            CrSel = 0x400000A3,
            ExSel = 0x400000A4,
            KeypadDoubleZero = 0x400000B0,
            KeypadTripleZero = 0x400000B1,
            ThousandSeparator = 0x400000B2,
            DecimalSeparator = 0x400000B3,
            CurrencyUnit = 0x400000B4,
            CurrencySubunit = 0x400000B5,
            KeypadLeftParentheses = 0x400000B6,
            KeypadRightParentheses = 0x400000B7,
            KeypadLeftBrace = 0x400000B8,
            KeypadRightBrace = 0x400000B9,
            KeypadTab = 0x400000BA,
            KeypadBackspace = 0x400000BB,
            KeypadA = 0x400000BC,
            KeypadB = 0x400000BD,
            KeypadC = 0x400000BE,
            KeypadD = 0x400000BF,
            KeypadE = 0x400000C0,
            KeypadF = 0x400000C1,
            KeypadXor = 0x400000C2,
            KeypadPower = 0x400000C3,
            KeypadPercent = 0x400000C4,
            KeypadLess = 0x400000C5,
            KeypadGreater = 0x400000C6,
            KeypadAmpersand = 0x400000C7,
            KeypadDoubleAmpersand = 0x400000C8,
            KeypadVerticalBar = 0x400000C9,
            KeypadDoubleVerticalBar = 0x400000CA,
            KeypadColon = 0x400000CB,
            KeypadHash = 0x400000CC,
            KeypadSpace = 0x400000CD,
            KeypadAt = 0x400000CE,
            KeypadExclamation = 0x400000CF,
            KeypadMemStore = 0x400000D0,
            KeypadMemRecall = 0x400000D1,
            KeypadMemClear = 0x400000D2,
            KeypadMemAdd = 0x400000D3,
            KeypadMemSubtract = 0x400000D4,
            KeypadMemMultiply = 0x400000D5,
            KeypadMemDivide = 0x400000D6,
            KeypadPlusMinus = 0x400000D7,
            KeypadClear = 0x400000D8,
            KeypadClearEntry = 0x400000D9,
            KeypadBinary = 0x400000DA,
            KeypadOctal = 0x400000DB,
            KeypadDecimal = 0x400000DC,
            KeypadHexadecimal = 0x400000DD,
            LeftControl = 0x400000E0,
            LeftShift = 0x400000E1,
            LeftAlt = 0x400000E2,
            LeftGui = 0x400000E3,
            RightControl = 0x400000E4,
            RightShift = 0x400000E5,
            RightAlt = 0x400000E6,
            RightGui = 0x400000E7,
            Mode = 0x40000101,
            Sleep = 0x40000102,
            Wake = 0x40000103,
            ChannelIncrement = 0x40000104,
            ChannelDecrement = 0x40000105,
            MediaPlay = 0x40000106,
            MediaPause = 0x40000107,
            MediaRecord = 0x40000108,
            MediaFastForward = 0x40000109,
            MediaRewind = 0x4000010A,
            MediaNextTrack = 0x4000010B,
            MediaPreviousTrack = 0x4000010C,
            MediaStop = 0x4000010D,
            MediaEject = 0x4000010E,
            MediaPlayPause = 0x4000010F,
            MediaSelect = 0x40000110,
            AcNew = 0x40000111,
            AcOpen = 0x40000112,
            AcClose = 0x40000113,
            AcExit = 0x40000114,
            AcSave = 0x40000115,
            AcPrint = 0x40000116,
            AcProperties = 0x40000117,
            AcSearch = 0x40000118,
            AcHome = 0x40000119,
            AcBack = 0x4000011A,
            AcForward = 0x4000011B,
            AcStop = 0x4000011C,
            AcRefresh = 0x4000011D,
            AcBookmarks = 0x4000011E,
            SoftLeft = 0x4000011F,
            SoftRight = 0x40000120,
            Call = 0x40000121,
            EndCall = 0x40000122,
            LeftTab = 0x20000001,
            Level5Shift = 0x20000002,
            MultiKeyCompose = 0x20000003,
            LeftMeta = 0x20000004,
            RightMeta = 0x20000005,
            LeftHyper = 0x20000006,
            RightHyper = 0x20000007
        }
        
        // Static Properties

        public OKeyboard(OWindow window, SDL.EventFilter filter)
        {
            _window = window;
        }

        private static readonly Dictionary<char, char> ShiftedSymbols = new()
        {
            ['1'] = '!',
            ['2'] = '@',
            ['3'] = '#',
            ['4'] = '$',
            ['5'] = '%',
            ['6'] = '^',
            ['7'] = '&',
            ['8'] = '*',
            ['9'] = '(',
            ['0'] = ')',
            ['-'] = '_',
            ['='] = '+',
            ['['] = '{',
            [']'] = '}',
            [';'] = ':',
            ['\''] = '"',
            [','] = '<',
            ['.'] = '>',
            ['/'] = '?',
            ['\\'] = '|',
            ['`'] = '~'
        };
        
        // Properties and Fields

        private OWindow _window;
        private SDL.EventFilter _filter;

        public OWindow Window => _window;
        public bool Initialized { get; private set; } = false;
        
        // Events

        public event OKeyboardEvents.OnInitialization? OnInitialization;
        public event OKeyboardEvents.OnKeyDown? OnKeyDown;
        public event OKeyboardEvents.OnKeyUp? OnKeyUp;
        
        // Methods

        public OKeyboard(OWindow window)
        {
            _window = window;
            _filter = Filter;
        }

        public void Initialize()
        {
            if (Initialized)
            {
                ODebugger.Warn("Keyboard can not be initialized twice.\n");

                return;
            }

            if (!_window.Initialized)
            {
                ODebugger.Warn("Window must be initialized first.\n");

                return;
            }

            (float x, float y) = (0, 0);

            if (SDL.IsMainThread())
            {
                SDL.AddEventWatch(_filter, IntPtr.Zero);
                SDL.GetMouseState(out x, out y);
            }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.AddEventWatch(_filter, IntPtr.Zero);
                    SDL.GetMouseState(out x, out y);
                }, IntPtr.Zero, true);

            Initialized = true;

            OnInitialization?.Invoke();
            ODebugger.Inform($"Initialized keyboard for \"{_window.Name}\".\n");
        }
        
        public void Press(OKeyboardKey key, OModifierKey modifier = OModifierKey.None, bool release = true)
        {
            SDL.Event ev = new();

            ev.Type = (uint)SDL.EventType.KeyDown;
            ev.Key.Key = (SDL.Keycode)key;

            if (modifier != OModifierKey.None)
            {
                if (modifier.HasFlag(OModifierKey.LeftShift))
                    ev.Key.Mod |= SDL.Keymod.LShift;

                if (modifier.HasFlag(OModifierKey.RightShift))
                    ev.Key.Mod |= SDL.Keymod.RShift;

                if (modifier.HasFlag(OModifierKey.Shift))
                    ev.Key.Mod |= SDL.Keymod.Shift;

                if (modifier.HasFlag(OModifierKey.Level5Shift))
                    ev.Key.Mod |= SDL.Keymod.Level5;

                if (modifier.HasFlag(OModifierKey.LeftControl))
                    ev.Key.Mod |= SDL.Keymod.LCtrl;

                if (modifier.HasFlag(OModifierKey.RightControl))
                    ev.Key.Mod |= SDL.Keymod.RCtrl;

                if (modifier.HasFlag(OModifierKey.Control))
                    ev.Key.Mod |= SDL.Keymod.Ctrl;

                if (modifier.HasFlag(OModifierKey.LeftAlt))
                    ev.Key.Mod |= SDL.Keymod.LAlt;

                if (modifier.HasFlag(OModifierKey.RightAlt))
                    ev.Key.Mod |= SDL.Keymod.RAlt;

                if (modifier.HasFlag(OModifierKey.Alt))
                    ev.Key.Mod |= SDL.Keymod.Alt;

                if (modifier.HasFlag(OModifierKey.LeftGui))
                    ev.Key.Mod |= SDL.Keymod.LGUI;

                if (modifier.HasFlag(OModifierKey.RightGui))
                    ev.Key.Mod |= SDL.Keymod.RGUI;

                if (modifier.HasFlag(OModifierKey.Gui))
                    ev.Key.Mod |= SDL.Keymod.GUI;

                if (modifier.HasFlag(OModifierKey.ScrollLock))
                    ev.Key.Mod |= SDL.Keymod.Scroll;

                if (modifier.HasFlag(OModifierKey.CapsLock))
                    ev.Key.Mod |= SDL.Keymod.Caps;

                if (modifier.HasFlag(OModifierKey.NumLock))
                    ev.Key.Mod |= SDL.Keymod.Num;

                if (modifier.HasFlag(OModifierKey.Mode))
                    ev.Key.Mod |= SDL.Keymod.Mode;
            }

            if (SDL.IsMainThread())
                SDL.PushEvent(ref ev);

            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.PushEvent(ref ev);
                }, IntPtr.Zero, false);

            ODebugger.Inform($"Key press simulated for \"{_window.Name}\".\n");

            if (release)
                Release(key, modifier);
        }

        public void Release(OKeyboardKey key, OModifierKey modifier = OModifierKey.None)
        {
            SDL.Event ev = new();
            ev.Type = (uint)SDL.EventType.KeyUp;
            ev.Key.Key = (SDL.Keycode)key;

            if (modifier != OModifierKey.None)
            {
                if (modifier.HasFlag(OModifierKey.LeftShift))
                    ev.Key.Mod |= SDL.Keymod.LShift;
                
                if (modifier.HasFlag(OModifierKey.RightShift))
                    ev.Key.Mod |= SDL.Keymod.RShift;
                
                if (modifier.HasFlag(OModifierKey.Shift))
                    ev.Key.Mod |= SDL.Keymod.Shift;
                
                if (modifier.HasFlag(OModifierKey.Level5Shift))
                    ev.Key.Mod |= SDL.Keymod.Level5;
                
                if (modifier.HasFlag(OModifierKey.LeftControl))
                    ev.Key.Mod |= SDL.Keymod.LCtrl;
                
                if (modifier.HasFlag(OModifierKey.RightControl))
                    ev.Key.Mod |= SDL.Keymod.RCtrl;
                
                if (modifier.HasFlag(OModifierKey.Control))
                    ev.Key.Mod |= SDL.Keymod.Ctrl;
                
                if (modifier.HasFlag(OModifierKey.LeftAlt))
                    ev.Key.Mod |= SDL.Keymod.LAlt;
                
                if (modifier.HasFlag(OModifierKey.RightAlt))
                    ev.Key.Mod |= SDL.Keymod.RAlt;
                
                if (modifier.HasFlag(OModifierKey.Alt))
                    ev.Key.Mod |= SDL.Keymod.Alt;
                
                if (modifier.HasFlag(OModifierKey.LeftGui))
                    ev.Key.Mod |= SDL.Keymod.LGUI;
                
                if (modifier.HasFlag(OModifierKey.RightGui))
                    ev.Key.Mod |= SDL.Keymod.RGUI;
                
                if (modifier.HasFlag(OModifierKey.Gui))
                    ev.Key.Mod |= SDL.Keymod.GUI;
                
                if (modifier.HasFlag(OModifierKey.ScrollLock))
                    ev.Key.Mod |= SDL.Keymod.Scroll;
                
                if (modifier.HasFlag(OModifierKey.CapsLock))
                    ev.Key.Mod |= SDL.Keymod.Caps;
                
                if (modifier.HasFlag(OModifierKey.NumLock))
                    ev.Key.Mod |= SDL.Keymod.Num;
                
                if (modifier.HasFlag(OModifierKey.Mode))
                    ev.Key.Mod |= SDL.Keymod.Mode;
            }

            if (SDL.IsMainThread())
                SDL.PushEvent(ref ev);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.PushEvent(ref ev);
                }, IntPtr.Zero, false);

            ODebugger.Inform($"Key release simulated for \"{_window.Name}\".\n");
        }

        public OModifierKey GetModifiers()
        {
            SDL.Keymod active = SDL.GetModState();
            OModifierKey modifier = OModifierKey.None;

            if (active == 0)
                return modifier;
            
            if (active.HasFlag(SDL.Keymod.LShift))
                modifier |= OModifierKey.LeftShift;
            
            if (active.HasFlag(SDL.Keymod.RShift)) 
                modifier |= OModifierKey.RightShift;
            
            if (active.HasFlag(SDL.Keymod.Level5))
                modifier |= OModifierKey.Level5Shift;
            
            if (active.HasFlag(SDL.Keymod.LCtrl))
                modifier |= OModifierKey.LeftControl;
            
            if (active.HasFlag(SDL.Keymod.RCtrl))
                modifier |= OModifierKey.RightControl;
            
            if (active.HasFlag(SDL.Keymod.LAlt))
                modifier |= OModifierKey.LeftAlt;
            
            if (active.HasFlag(SDL.Keymod.RAlt))
                modifier |= OModifierKey.RightAlt;
            
            if (active.HasFlag(SDL.Keymod.LGUI))
                modifier |= OModifierKey.LeftGui;
            
            if (active.HasFlag(SDL.Keymod.RGUI))
                modifier |= OModifierKey.RightGui;
            
            if (active.HasFlag(SDL.Keymod.Scroll))
                modifier |= OModifierKey.ScrollLock;
            
            if (active.HasFlag(SDL.Keymod.Caps))
                modifier |= OModifierKey.CapsLock;
            
            if (active.HasFlag(SDL.Keymod.Num))
                modifier |= OModifierKey.NumLock;
            
            if (active.HasFlag(SDL.Keymod.Mode))
                modifier |= OModifierKey.Mode;

            return modifier;
        }

        public HashSet<OKeyboardKey> GetKeys()
        {
            bool[] keyboardState = SDL.GetKeyboardState(out int _);
            SDL.Keymod modState = SDL.GetModState();
            HashSet<OKeyboardKey> keys = new();

            for (int i = 0; i < keyboardState.Length; i++)
            {
                if (keyboardState[i])
                {
                    SDL.Scancode scancode = (SDL.Scancode)i;
                    OKeyboardKey key = (OKeyboardKey)SDL.GetKeyFromScancode(scancode, modState, false);

                    keys.Add(key);
                }
            }

            return keys;
        }

        public bool IsModifierPressed(OModifierKey modifier, OModifierKey? active = null)
        {
            return (active ?? GetModifiers()).HasFlag(modifier);
        }

        public bool IsModifierReleased(OModifierKey modifier, OModifierKey? active = null)
        {
            return !IsModifierPressed(modifier, active);
        }

        public bool IsKeyPressed(OKeyboardKey key)
        {
            bool[] keyboardState = SDL.GetKeyboardState(out int _);

            return keyboardState[(int)SDL.GetScancodeFromKey((SDL.Keycode)key, out _)];
        }

        public bool IsKeyReleased(OKeyboardKey key)
        {
            return !IsKeyPressed(key);
        }

        public bool IsModifier(OKeyboardKey key)
        {
            return OModifierKey.TryParse(key.ToString(), false, out OModifierKey _);
        }

        public string GetValue(OKeyboardKey key, OModifierKey modifier)
        {
            string name = SDL.GetKeyName((SDL.Keycode)key);
            
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            if (name.Length == 1)
            {
                char character = name[0];
                bool shift = (modifier & OModifierKey.Shift) != 0;
                bool caps = modifier.HasFlag(OModifierKey.CapsLock);

                if (char.IsLetter(character))
                    return ((shift ^ caps) ? char.ToUpper(character) : char.ToLower(character)).ToString();

                if (shift && ShiftedSymbols.TryGetValue(character, out char shifted))
                    return shifted.ToString();

                return character.ToString();
            }

            return name;
        }

        private bool Filter(IntPtr _, ref SDL.Event ev)
        {
            if (ev.Window.WindowID != SDL.GetWindowID(_window.WindowHandle))
                return false;

            switch (ev.Type)
            {
                case (uint)SDL.EventType.KeyDown:
                    OKeyboardKey key = (OKeyboardKey)ev.Key.Key;
                    OModifierKey modifier = GetModifiers();

                    OnKeyDown?.Invoke(key, modifier);
                    
                    break;
                case (uint)SDL.EventType.KeyUp:
                    key = (OKeyboardKey)ev.Key.Key;
                    modifier = GetModifiers();

                    OnKeyUp?.Invoke(key, modifier);
                    
                    break;
            }
            
            return false;
        }
        
        // To String

        public override string ToString()
        {
            return $"Keyboard for \"{_window.Name}\"";
        }
    }

    public class OMouse
    {
        // Enums
        
        public enum OMouseButton
        {
            Left,
            Middle,
            Right,
            Extra1,
            Extra2
        }
        
        // Properties and Fields

        private OWindow _window;
        private SDL.EventFilter _filter;

        public OWindow Window => _window;
        public bool Initialized { get; private set; } = false;
        public OVector2<float> Position { get; private set; } = new OVector2<float>(-1, -1);
        
        // Events

        public event OMouseEvents.OnInitialization? OnInitialization;
        public event OMouseEvents.OnButtonDown? OnButtonDown;
        public event OMouseEvents.OnButtonUp? OnButtonUp;
        public event OMouseEvents.OnScroll? OnScroll;
        public event OMouseEvents.OnMove? OnMove;
        
        // Methods and Functions

        public OMouse(OWindow window)
        {
            _window = window;
            _filter = Filter;
        }

        public void Initialize()
        {
            if (Initialized)
            {
                ODebugger.Warn("Keyboard can not be initialized twice.\n");

                return;
            }

            if (!_window.Initialized)
            {
                ODebugger.Warn("Window must be initialized first.\n");

                return;
            }

            (float x, float y) = (0, 0);

            if (SDL.IsMainThread())
            {
                SDL.AddEventWatch(_filter, IntPtr.Zero);
                SDL.GetMouseState(out x, out y);
            }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.AddEventWatch(_filter, IntPtr.Zero);
                    SDL.GetMouseState(out x, out y);
                }, IntPtr.Zero, true);

            Position = new OVector2<float>(x, y);
            Initialized = true;

            OnInitialization?.Invoke();
            ODebugger.Inform($"Initialized mouse for \"{_window.Name}\".\n");
        }

        public void Click(OMouseButton button, OVector2<float>? position = null, byte clicks = 1)
        {
            if (position is null)
                position = Position;
            else
                Move(position ?? Position);

            SDL.Event ev = new SDL.Event();

            ev.Type = (uint)SDL.EventType.MouseButtonDown;
            ev.Window.WindowID = SDL.GetWindowID(_window.WindowHandle);
            ev.Button.Button = (byte)(button + 1);
            ev.Button.X = position.Value.X;
            ev.Button.Y = position.Value.Y;
            ev.Button.Clicks = clicks;

            if (SDL.IsMainThread())
                SDL.PushEvent(ref ev);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.PushEvent(ref ev);
                }, IntPtr.Zero, false);

            ev.Type = (uint)SDL.EventType.MouseButtonUp;

            if (SDL.IsMainThread())
                SDL.PushEvent(ref ev);
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.PushEvent(ref ev);
                }, IntPtr.Zero, false);
                
            ODebugger.Inform($"Mouse click simulated for \"{_window.Name}\".\n");
        }

        public void Move(OVector2<float> position, bool global = false)
        {
            SDL.Event ev = new SDL.Event();

            ev.Type = (uint)SDL.EventType.MouseMotion;
            ev.Window.WindowID = SDL.GetWindowID(_window.WindowHandle);
            ev.Motion.X = position.X;
            ev.Motion.Y = position.Y;

            if (SDL.IsMainThread())
            {
                if (global)
                    SDL.WarpMouseGlobal(position.X, position.Y);
                else
                    SDL.WarpMouseInWindow(_window.WindowHandle, position.X, position.Y);

                SDL.PushEvent(ref ev);

                Position = position;
            }
            else
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    if (global)
                        SDL.WarpMouseGlobal(position.X, position.Y);
                    else
                        SDL.WarpMouseInWindow(_window.WindowHandle, position.X, position.Y);

                    SDL.PushEvent(ref ev);

                    Position = position;
                }, IntPtr.Zero, false);
                
            ODebugger.Inform($"Mouse move simulated for \"{_window.Name}\".\n");
        }

        private bool Filter(IntPtr _, ref SDL.Event ev)
        {
            if (ev.Window.WindowID != SDL.GetWindowID(_window.WindowHandle))
                return false;

            switch (ev.Type)
            {
                case (uint)SDL.EventType.MouseButtonDown:
                    OMouseButton button = (OMouseButton)(ev.Button.Button - 1);
                    OVector2<float> position = new OVector2<float>(ev.Button.X, ev.Button.Y);
                    byte clicks = ev.Button.Clicks;

                    OnButtonDown?.Invoke(button, position, clicks);
                    
                    break;
                case (uint)SDL.EventType.MouseButtonUp:
                    button = (OMouseButton)(ev.Button.Button - 1);
                    position = new OVector2<float>(ev.Button.X, ev.Button.Y);
                    clicks = ev.Button.Clicks;
                    
                    OnButtonUp?.Invoke(button, position, clicks);
                    
                    break;
                case (uint)SDL.EventType.MouseWheel:
                    OVector2<float> scrolled = new OVector2<float>(ev.Wheel.X, ev.Wheel.Y);
                    bool flipped = ev.Wheel.Direction == SDL.MouseWheelDirection.Flipped;
                    
                    position = new OVector2<float>(ev.Wheel.MouseX, ev.Wheel.MouseY);
                    
                    OnScroll?.Invoke(scrolled, position, flipped);
                    
                    break;
                case (uint)SDL.EventType.MouseMotion:
                    position = new OVector2<float>(ev.Motion.X, ev.Motion.Y);
                    Position = position;
                    
                    OnMove?.Invoke(position);
                    
                    break;
            }
            
            return false;
        }
        
        // To String

        public override string ToString()
        {
            return $"Mouse for \"{_window.Name}\"";
        }
    }
}