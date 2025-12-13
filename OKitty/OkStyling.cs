// Imports

using static OKitty.OkInstance;
using static OKitty.OkInterface;
using static OKitty.OkMath;
using static OKitty.OkScript;
using SDL3;
using System.Collections.ObjectModel;

namespace OKitty;

// OkStyling

public static class OkStyling
{
    // Structs
    
    public struct OColor
    {
        // Enums
        
        public enum OColors : uint
        {
            // A
            
            AliceBlue = 0xFFF0F8FF,
            AntiqueWhite = 0xFFFAEBD7,
            Aqua = 0xFF00FFFF,
            Aquamarine = 0xFF7FFFD4,
            Azure = 0xFFF0FFFF,
            
            // B
            
            Beige = 0xFFF5F5DC,
            Bisque = 0xFFFFE4C4,
            Black = 0xFF000000,
            BlanchedAlmond = 0xFFFFEBCD,
            Blue = 0xFF0000FF,
            BlueViolet = 0xFF8A2BE2,
            Brown = 0xFFA52A2A,
            BurlyWood = 0xFFDEB887,
            
            // C
            
            CadetBlue = 0xFF5F9EA0,
            Chartreuse = 0xFF7FFF00,
            Chocolate = 0xFFD2691E,
            Coral = 0xFFFF7F50,
            CornflowerBlue = 0xFF6495ED,
            Cornsilk = 0xFFFFF8DC,
            Crimson = 0xFFDC143C,
            Cyan = 0xFF00FFFF,
            
            // D

            DarkBlue = 0xFF00008B,
            DarkCyan = 0xFF008B8B,
            DarkGoldenRod = 0xFFB8860B,
            DarkGray = 0xFFA9A9A9,
            DarkGreen = 0xFF006400,
            DarkKhaki = 0xFFBDB76B,
            DarkMagenta = 0xFF8B008B,
            DarkOliveGreen = 0xFF556B2F,
            DarkOrange = 0xFFFF8C00,
            DarkOrchid = 0xFF9932CC,
            DarkRed = 0xFF8B0000,
            DarkSalmon = 0xFFE9967A,
            DarkSeaGreen = 0xFF8FBC8F,
            DarkSlateBlue = 0xFF483D8B,
            DarkSlateGray = 0xFF2F4F4F,
            DarkTurquoise = 0xFF00CED1,
            DarkViolet = 0xFF9400D3,
            DeepPink = 0xFFFF1493,
            DeepSkyBlue = 0xFF00BFFF,
            DimGray = 0xFF696969,
            DodgerBlue = 0xFF1E90FF,
            
            // F

            FireBrick = 0xFFB22222,
            FloralWhite = 0xFFFFFAF0,
            ForestGreen = 0xFF228B22,
            Fuchsia = 0xFFFF00FF,
            
            // G

            Gainsboro = 0xFFDCDCDC,
            GhostWhite = 0xFFF8F8FF,
            Gold = 0xFFFFD700,
            GoldenRod = 0xFFDAA520,
            Gray = 0xFF808080,
            Green = 0xFF008000,
            GreenYellow = 0xFFADFF2F,
            
            // H

            HoneyDew = 0xFFF0FFF0,
            HotPink = 0xFFFF69B4,
            
            // I

            IndianRed = 0xFFCD5C5C,
            Indigo = 0xFF4B0082,
            Ivory = 0xFFFFFFF0,
            
            // K

            Khaki = 0xFFF0E68C,
            
            // L

            Lavender = 0xFFE6E6FA,
            LavenderBlush = 0xFFFFF0F5,
            LawnGreen = 0xFF7CFC00,
            LemonChiffon = 0xFFFFFACD,
            LightBlue = 0xFFADD8E6,
            LightCoral = 0xFFF08080,
            LightCyan = 0xFFE0FFFF,
            LightGoldenRodYellow = 0xFFFAFAD2,
            LightGray = 0xFFD3D3D3,
            LightGreen = 0xFF90EE90,
            LightPink = 0xFFFFB6C1,
            LightSalmon = 0xFFFFA07A,
            LightSeaGreen = 0xFF20B2AA,
            LightSkyBlue = 0xFF87CEFA,
            LightSlateGray = 0xFF778899,
            LightSteelBlue = 0xFFB0C4DE,
            LightYellow = 0xFFFFFFE0,
            Lime = 0xFF00FF00,
            LimeGreen = 0xFF32CD32,
            Linen = 0xFFFAF0E6,
            
            // M

            Magenta = 0xFFFF00FF,
            Maroon = 0xFF800000,
            MediumAquaMarine = 0xFF66CDAA,
            MediumBlue = 0xFF0000CD,
            MediumOrchid = 0xFFBA55D3,
            MediumPurple = 0xFF9370DB,
            MediumSeaGreen = 0xFF3CB371,
            MediumSlateBlue = 0xFF7B68EE,
            MediumSpringGreen = 0xFF00FA9A,
            MediumTurquoise = 0xFF48D1CC,
            MediumVioletRed = 0xFFC71585,
            MidnightBlue = 0xFF191970,
            MintCream = 0xFFF5FFFA,
            MistyRose = 0xFFFFE4E1,
            Moccasin = 0xFFFFE4B5,
            
            // N

            NavajoWhite = 0xFFFFDEAD,
            Navy = 0xFF000080,
            
            // O

            OldLace = 0xFFFDF5E6,
            Olive = 0xFF808000,
            OliveDrab = 0xFF6B8E23,
            Orange = 0xFFFFA500,
            OrangeRed = 0xFFFF4500,
            Orchid = 0xFFDA70D6,
            
            // P

            PaleGoldenRod = 0xFFEEE8AA,
            PaleGreen = 0xFF98FB98,
            PaleTurquoise = 0xFFAFEEEE,
            PaleVioletRed = 0xFFDB7093,
            PapayaWhip = 0xFFFFEFD5,
            PeachPuff = 0xFFFFDAB9,
            Peru = 0xFFCD853F,
            Pink = 0xFFFFC0CB,
            Plum = 0xFFDDA0DD,
            PowderBlue = 0xFFB0E0E6,
            Purple = 0xFF800080,
            
            // R

            RebeccaPurple = 0xFF663399,
            Red = 0xFFFF0000,
            RosyBrown = 0xFFBC8F8F,
            RoyalBlue = 0xFF4169E1,
            
            // S

            SaddleBrown = 0xFF8B4513,
            Salmon = 0xFFFA8072,
            SandyBrown = 0xFFF4A460,
            SeaGreen = 0xFF2E8B57,
            SeaShell = 0xFFFFF5EE,
            Sienna = 0xFFA0522D,
            Silver = 0xFFC0C0C0,
            SkyBlue = 0xFF87CEEB,
            SlateBlue = 0xFF6A5ACD,
            SlateGray = 0xFF708090,
            Snow = 0xFFFFFAFA,
            SpringGreen = 0xFF00FF7F,
            SteelBlue = 0xFF4682B4,
            
            // T

            Tan = 0xFFD2B48C,
            Teal = 0xFF008080,
            Thistle = 0xFFD8BFD8,
            Tomato = 0xFFFF6347,
            Turquoise = 0xFF40E0D0,
            
            // V

            Violet = 0xFFEE82EE,
            
            // W

            Wheat = 0xFFF5DEB3,
            White = 0xFFFFFFFF,
            WhiteSmoke = 0xFFF5F5F5,
            
            // Y

            Yellow = 0xFFFFFF00,
            YellowGreen = 0xFF9ACD32
        }

        public enum OColorShades : uint
        {
            // Red Shades

            Red100 = 0xFFFFEBEE,
            Red200 = 0xFFFFCDD2,
            Red300 = 0xFFEF9A9A,
            Red400 = 0xFFE57373,
            Red500 = 0xFFF44336,
            Red600 = 0xFFE53935,
            Red700 = 0xFFD32F2F,
            Red800 = 0xFFC62828,
            Red900 = 0xFFB71C1C,

            // Orange Shades

            Orange100 = 0xFFFFE0B2,
            Orange200 = 0xFFFFCC80,
            Orange300 = 0xFFFFB347,
            Orange400 = 0xFFFFA726,
            Orange500 = 0xFFFF9800,
            Orange600 = 0xFFFB8C00,
            Orange700 = 0xFFF57C00,
            Orange800 = 0xFFEF6C00,
            Orange900 = 0xFFE65100,

            // Yellow Shades

            Yellow100 = 0xFFFFF9C4,
            Yellow200 = 0xFFFFF59D,
            Yellow300 = 0xFFFFF176,
            Yellow400 = 0xFFFFEE58,
            Yellow500 = 0xFFFFEB3B,
            Yellow600 = 0xFFFDD835,
            Yellow700 = 0xFFFBC02D,
            Yellow800 = 0xFFF9A825,
            Yellow900 = 0xFFF57F17,

            // Green Shades

            Green100 = 0xFFE8F5E9,
            Green200 = 0xFFC8E6C9,
            Green300 = 0xFFA5D6A7,
            Green400 = 0xFF81C784,
            Green500 = 0xFF66BB6A,
            Green600 = 0xFF558B2F,
            Green700 = 0xFF388E3C,
            Green800 = 0xFF33691E,
            Green900 = 0xFF1B5E20,

            // Cyan Shades

            Cyan100 = 0xFFE0F7FA,
            Cyan200 = 0xFFB2EBF2,
            Cyan300 = 0xFF80DEEA,
            Cyan400 = 0xFF4DD0E1,
            Cyan500 = 0xFF00BCD4,
            Cyan600 = 0xFF00ACC1,
            Cyan700 = 0xFF0097A7,
            Cyan800 = 0xFF00838F,
            Cyan900 = 0xFF006064,

            // Blue Shades

            Blue100 = 0xFFBBDEFB,
            Blue200 = 0xFF90CAF9,
            Blue300 = 0xFF64B5F6,
            Blue400 = 0xFF42A5F5,
            Blue500 = 0xFF2196F3,
            Blue600 = 0xFF1E88E5,
            Blue700 = 0xFF1976D2,
            Blue800 = 0xFF1565C0,
            Blue900 = 0xFF0D47A1,

            // Purple Shades

            Purple100 = 0xFFE1BEE7,
            Purple200 = 0xFFCE93D8,
            Purple300 = 0xFFBA68C8,
            Purple400 = 0xFFAB47BC,
            Purple500 = 0xFF9C27B0,
            Purple600 = 0xFF8E24AA,
            Purple700 = 0xFF7B1FA2,
            Purple800 = 0xFF6A1B9A,
            Purple900 = 0xFF4A148C,

            // Pink Shades

            Pink100 = 0xFFFCE4EC,
            Pink200 = 0xFFF8BBD0,
            Pink300 = 0xFFF48FB1,
            Pink400 = 0xFFF06292,
            Pink500 = 0xFFE91E63,
            Pink600 = 0xFFD81B60,
            Pink700 = 0xFFC2185B,
            Pink800 = 0xFFAD1457,

            // Gray Shades

            Gray100 = 0xFFF5F5F5,
            Gray200 = 0xFFEEEEEE,
            Gray300 = 0xFFE0E0E0,
            Gray400 = 0xFFBDBDBD,
            Gray500 = 0xFF9E9E9E,
            Gray600 = 0xFF757575,
            Gray700 = 0xFF616161,
            Gray800 = 0xFF424242,
            Gray900 = 0xFF212121,

            // White Shades

            White100 = 0xFFFFFFFF,
            White200 = 0xFFFAFAFA,
            White300 = 0xFFF5F5F5,

            // Black Shades

            Black100 = 0xFF1A1A1A,
            Black200 = 0xFF0D0D0D,
            Black300 = 0xFF000000
        }
    
        // Static Properties

        public static readonly OColor Red = new OColor(OColors.Red);
        public static readonly OColor Orange = new OColor(OColors.Orange);
        public static readonly OColor Yellow = new OColor(OColors.Yellow);
        public static readonly OColor Brown = new OColor(OColors.Brown);
        public static readonly OColor Green = new OColor(OColors.Green);
        public static readonly OColor Lime = new OColor(OColors.Lime);
        public static readonly OColor Blue = new OColor(OColors.Blue);
        public static readonly OColor Purple = new OColor(OColors.Purple);
        public static readonly OColor Pink = new OColor(OColors.Pink);
        public static readonly OColor White = new OColor(OColors.White);
        public static readonly OColor Black = new OColor(OColors.Black);
        public static readonly OColor Transparent = new OColor(0U);
        
        // Properties and Fields
        
        public uint Hex { get; }
        public (byte Alpha, byte Red, byte Green, byte Blue) Argb { get; }
        public (byte Alpha, ushort Hue, byte Saturation, byte Value) Ahsv { get; }
        public (byte Alpha, ushort Hue, byte Saturation, byte Lightness) Ahsl { get; }
        public (byte Alpha, byte Cyan, byte Magenta, byte Yellow, byte Key) Acmyk { get; }
        
        // Methods and Functions

        public OColor(uint hex)
        {
            Hex = hex;
            Argb = ((byte)((hex >> 24) & 0xFF), (byte)((hex >> 16) & 0xFF), (byte)((hex >> 8) & 0xFF), (byte)(hex & 0xFF));
            
            {
                byte red = Argb.Red;
                byte green = Argb.Green;
                byte blue = Argb.Blue;
                byte max = Math.Max(red, Math.Max(green, blue));
                byte min = Math.Min(red, Math.Min(green, blue));
                byte delta = (byte)(max - min);
                ushort hue = 0;
                
                if (delta != 0)
                {
                    if (max == red)
                        hue = (ushort)(60 * ((green - blue) / (float)delta) % 360);
                    else if (max == green)
                        hue = (ushort)(60 * ((blue - red) / (float)delta + 2));
                    else
                        hue = (ushort)(60 * ((red - green) / (float)delta + 4));
                }
                
                if (hue < 0)
                    hue += 360;

                byte saturation = max == 0 ? (byte)0 : (byte)(delta * 255 / max);
                byte value = max;

                Ahsv = (Argb.Alpha, hue, saturation, value);
            }
            
            {
                byte red = Argb.Red;
                byte green = Argb.Green;
                byte blue = Argb.Blue;
                byte max = Math.Max(red, Math.Max(green, blue));
                byte min = Math.Min(red, Math.Min(green, blue));
                ushort hue = Ahsv.Hue;
                byte lightness = (byte)((max + min) / 2);
                byte saturation = 0;
                
                if (max != min)
                    saturation = (byte)((Ahsv.Value - Math.Abs(2 * lightness - Ahsv.Value)) * 255 / Ahsv.Value);

                Ahsl = (Argb.Alpha, hue, saturation, lightness);
            }
            
            {
                float red = Argb.Red / 255f;
                float green = Argb.Green / 255f;
                float blue = Argb.Blue / 255f;
                float key = 1 - Math.Max(red, Math.Max(green, blue));
                byte cyan = (byte)((1 - red - key) / (1 - key) * 255);
                byte magenta = (byte)((1 - green - key) / (1 - key) * 255);
                byte yellow = (byte)((1 - key - key) / (1 - key) * 255);

                Acmyk = (Argb.Alpha, cyan, magenta, yellow, (byte)(key * 255));
            }
        }

        public OColor(OColors color) : this((uint)color)
        {
            
        }

        public OColor(OColorShades shade) : this((uint)shade)
        {
            
        }
        
        public OColor(double alpha, double red, double green, double blue)
        {
            alpha = Math.Clamp(alpha, 0.0, 1.0);
            red = Math.Clamp(red,   0.0, 1.0);
            green = Math.Clamp(green, 0.0, 1.0);
            blue = Math.Clamp(blue,  0.0, 1.0);

            byte a = (byte)(alpha * 255.0);
            byte r = (byte)(red   * 255.0);
            byte g = (byte)(green * 255.0);
            byte b = (byte)(blue  * 255.0);
            uint hex = ((uint)a << 24) | ((uint)r << 16) | ((uint)g << 8) | b;

            this = new OColor(hex);
        }


        public static OColor FromArgb(byte alpha, byte red, byte green, byte blue)
        {
            return new OColor(((uint)alpha << 24) | ((uint)red << 16) | ((uint)green << 8) | blue);
        }

        public static OColor FromAhsv(byte alpha, ushort hue, byte saturation, byte value)
        {
            float hueNormalized = hue / 360f;
            float saturationNormalized = saturation / 255f;
            float valueNormalized = value / 255f;
            int segment = (int)(hueNormalized * 6);
            float fractional = hueNormalized * 6 - segment;
            float primary = valueNormalized * (1 - saturationNormalized);
            float secondary = valueNormalized * (1 - fractional * saturationNormalized);
            float tertiary = valueNormalized * (1 - (1 - fractional) * saturationNormalized);
            float red, green, blue;
            
            switch (segment % 6)
            {
                case 0:
                    red = valueNormalized; green = tertiary; blue = primary;
                    
                    break;
                case 1:
                    red = secondary; green = valueNormalized; blue = primary;
                    
                    break;
                case 2:
                    red = primary; green = valueNormalized; blue = tertiary;
                    
                    break;
                case 3:
                    red = primary; green = secondary; blue = valueNormalized;
                    
                    break;
                case 4:
                    red = tertiary; green = primary; blue = valueNormalized;
                    
                    break;
                case 5:
                    red = valueNormalized; green = primary; blue = secondary;
                    
                    break;
                default:
                    red = green = blue = 0;
                    
                    break;
            }
            
            return FromArgb(alpha, (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
        }

        public static OColor FromAhsl(byte alpha, ushort hue, byte saturation, byte lightness)
        {
            float hueNormalized = hue / 360f;
            float saturationNormalized = saturation / 255f;
            float lightnessNormalized = lightness / 255f;
            float red, green, blue;
            
            if (saturationNormalized == 0)
                red = green = blue = lightnessNormalized;
            else
            {
                float compOne = lightnessNormalized < 0.5f
                    ? lightnessNormalized * (1 + saturationNormalized)
                    : lightnessNormalized + saturationNormalized - lightnessNormalized * saturationNormalized;
                float compTwo = 2 * lightnessNormalized - compOne;
                float[] hueComps = new float[3] { hueNormalized + 1f/3, hueNormalized, hueNormalized - 1f/3 };
                float[] rgbComps = new float[3];
                
                for (int index = 0; index < 3; index++)
                {
                    if (hueComps[index] < 0)
                        hueComps[index] += 1;
                    
                    if (hueComps[index] > 1)
                        hueComps[index] -= 1;
                    
                    if (hueComps[index] < 1f/6)
                        rgbComps[index] = compTwo + (compOne - compTwo) * 6 * hueComps[index];
                    else if (hueComps[index] < 1f/2)
                        rgbComps[index] = compOne;
                    else if (hueComps[index] < 2f/3)
                        rgbComps[index] = compTwo + (compOne - compTwo) * (2f/3 - hueComps[index]) * 6;
                    else
                        rgbComps[index] = compTwo;
                }
                
                red = rgbComps[0]; green = rgbComps[1]; blue = rgbComps[2];
            }

            return FromArgb(alpha, (byte)(red * 255), (byte)(green * 255), (byte)(blue * 25));
        }

        public static OColor FromAcmyk(byte alpha, byte cyan, byte magenta, byte yellow, byte key)
        {
            float cyanNormalized = cyan / 255f;
            float magentaNormalized = magenta / 255f;
            float yellowNormalized = yellow / 255f;
            float keyNormalized = key / 255f;
            float red = (1 - cyanNormalized) * (1 - keyNormalized);
            float green = (1 - magentaNormalized) * (1 - keyNormalized);
            float blue = (1 - yellowNormalized) * (1 - keyNormalized);
            
            return FromArgb(alpha, (byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
        }

        // Equals and Hashing

        public override bool Equals(object? obj)
        {
            if (obj is OColor other)
                return this == other;

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Hex, Argb, Ahsv, Ahsl, Acmyk);
        }
        
        // Binary Operators

        public static OColor operator +(OColor left, OColor right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha + right.Argb.Alpha, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red + right.Argb.Red, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green + right.Argb.Green, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue + right.Argb.Blue, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator +(OColor left, byte right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha + right, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red + right, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green + right, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue + right, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator -(OColor left, OColor right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha - right.Argb.Alpha, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red - right.Argb.Red, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green - right.Argb.Green, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue - right.Argb.Blue, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator -(OColor left, byte right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha - right, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red - right, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green - right, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue - right, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator *(OColor left, OColor right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha * right.Argb.Alpha, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red * right.Argb.Red, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green * right.Argb.Green, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue * right.Argb.Blue, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator *(OColor left, byte right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha * right, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red * right, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green * right, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue * right, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator /(OColor left, OColor right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha / right.Argb.Alpha, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red / right.Argb.Red, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green / right.Argb.Green, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue / right.Argb.Blue, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator /(OColor left, byte right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha / right, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red / right, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green / right, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue / right, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator %(OColor left, OColor right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha % right.Argb.Alpha, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red % right.Argb.Red, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green % right.Argb.Green, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue % right.Argb.Blue, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static OColor operator %(OColor left, byte right)
        {
            byte alpha = (byte)Math.Clamp(left.Argb.Alpha % right, 0, 255);
            byte red = (byte)Math.Clamp(left.Argb.Red % right, 0, 255);
            byte green = (byte)Math.Clamp(left.Argb.Green % right, 0, 255);
            byte blue = (byte)Math.Clamp(left.Argb.Blue % right, 0, 255);

            return FromArgb(alpha, red, green, blue);
        }

        public static bool operator ==(OColor left, OColor right)
        {
            bool alpha = left.Argb.Alpha == right.Argb.Alpha;
            bool red = left.Argb.Red == right.Argb.Red;
            bool green = left.Argb.Green == right.Argb.Green;
            bool blue = left.Argb.Blue == right.Argb.Blue;

            return alpha && red && green && blue;
        }

        public static bool operator !=(OColor left, OColor right)
        {
            return !(left == right);
        }

        public static bool operator >(OColor left, OColor right)
        {
            bool alpha = left.Argb.Alpha > right.Argb.Alpha;
            bool red = left.Argb.Red > right.Argb.Red;
            bool green = left.Argb.Green > right.Argb.Green;
            bool blue = left.Argb.Blue > right.Argb.Blue;

            return alpha && red && green && blue;
        }

        public static bool operator >=(OColor left, OColor right)
        {
            return left > right || left == right;
        }

        public static bool operator <(OColor left, OColor right)
        {
            bool alpha = left.Argb.Alpha < right.Argb.Alpha;
            bool red = left.Argb.Red < right.Argb.Red;
            bool green = left.Argb.Green < right.Argb.Green;
            bool blue = left.Argb.Blue < right.Argb.Blue;

            return alpha && red && green && blue;
        }

        public static bool operator <=(OColor left, OColor right)
        {
            return left < right || left == right;
        }
        
        // To String

        public override string ToString()
        {
            return $"#{Hex:x8}".ToUpper();
        }
    }

    // Class

    public class OSizeConstraint : IOModifier
    {
        // Properties and Fields

        private IOPrototype? _parent;
        private bool _active;
        private OVector2<float> _minSize;

        public IOModifier.OModifierCallTime CallTime => IOModifier.OModifierCallTime.Layout | IOModifier.OModifierCallTime.Behavior;
        public IOModifier.OModifierPriority Priority => IOModifier.OModifierPriority.Normal;
        public OVector2<float> MaxSize = new OVector2<float>(float.MaxValue, float.MaxValue);

        public IOPrototype? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                if (value?.HasModifier<OSizeConstraint>() ?? false)
                {
                    ODebugger.Warn($"\"{value.Name}\" already have an OSizeConstraint modifier.");

                    return;
                }

                if (_parent is OWindow window)
                    SetWindowSizeConstraint(window.WindowHandle, null, null);

                _parent?.RemoveModifier<OSizeConstraint>();

                _parent = value;

                _parent?.AddModifier(this);
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value)
                    return;

                if (_parent is OWindow window)
                {
                    if (value)
                        SetWindowSizeConstraint(window.WindowHandle, null, null);
                    else
                        SetWindowSizeConstraint(window.WindowHandle, _minSize, MaxSize);
                }

                _active = value;
            }
        }

        public OVector2<float> MinSize
        {
            get => _minSize;
            set
            {
                if (value <= MaxSize)
                    _minSize = value;
            }
        }

        // Methods and Functions

        public OSizeConstraint(IOPrototype? parent = null)
        {
            _active = true;
            _minSize = new OVector2<float>(float.MinValue, float.MinValue);

            Parent = parent;
        }

        public bool CanProcess<TReturn, TParams>()
        {
            return (typeof(TReturn) == typeof((OVector2<float>, OVector2<float>)) && typeof(TParams) == typeof((OVector2<float>, OVector2<float>)))
                || (typeof(TReturn) == typeof(object) && typeof(TParams) == typeof(double));
        }

        public TReturn? Process<TReturn, TParams>(IOModifier.OModifierCallTime callTime, TParams parameters)
        {
            if (callTime.HasFlag(IOModifier.OModifierCallTime.Layout) && _parent is IOInterface)
                if (parameters is ValueTuple<OVector2<float>, OVector2<float>> value)
                {
                    OVector2<float> size = value.Item1;
                    float width = MathF.Min(MaxSize.X, MathF.Max(size.X, _minSize.X));
                    float height = MathF.Min(MaxSize.Y, MathF.Max(size.Y, _minSize.Y));

                    return (TReturn)(object)((new OVector2<float>(width, height), value.Item2));
                }

            if (callTime.HasFlag(IOModifier.OModifierCallTime.Behavior) && _parent is OWindow window)
                if (parameters is double)
                {
                    SetWindowSizeConstraint(window.WindowHandle, _minSize, MaxSize);

                    return default;
                }

            return (TReturn)(object)parameters!;
        }

        private static void SetWindowSizeConstraint(IntPtr handle, OVector2<float>? min, OVector2<float>? max)
        {
            if (SDL.IsMainThread())
            {
                SDL.SetWindowMinimumSize(handle, (int)(min?.X ?? 0), (int)(min?.Y ?? 0));
                SDL.SetWindowMaximumSize(handle, (int)(max?.X ?? 0), (int)(max?.Y ?? 0));
                SDL.SyncWindow(handle);
            }
            else
            {
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowMinimumSize(handle, (int)(min?.X ?? 0), (int)(min?.Y ?? 0));
                    SDL.SetWindowMaximumSize(handle, (int)(max?.X ?? 0), (int)(max?.Y ?? 0));
                    SDL.SyncWindow(handle);
                }, IntPtr.Zero, false);
            }
        }

        // To String

        public override string ToString()
        {
            return $"<OSizeConstraint Active={Active} MinSize=({MinSize}) MaxSize=({MaxSize})>";
        }
    }

    public class OAspectRatioConstraint : IOModifier
    {
        // Properties and Fields

        private IOPrototype? _parent;
        private bool _active;
        
        public IOModifier.OModifierCallTime CallTime => IOModifier.OModifierCallTime.Layout | IOModifier.OModifierCallTime.Behavior;
        public IOModifier.OModifierPriority Priority => IOModifier.OModifierPriority.Normal;
        public float Ratio { get; set; } = 1;

        public IOPrototype? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                if (value?.HasModifier<OAspectRatioConstraint>() ?? false)
                {
                    ODebugger.Warn($"\"{value.Name}\" already have an OAspectRatioConstraint modifier.");

                    return;
                }

                if (_parent is OWindow window)
                    SetWindowRatio(window.WindowHandle, 0);

                _parent?.RemoveModifier<OAspectRatioConstraint>();

                _parent = value;

                _parent?.AddModifier(this);
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                if (_active == value)
                    return;

                if (_parent is OWindow window)
                {
                    if (value)
                        SetWindowRatio(window.WindowHandle, Ratio);
                    else
                        SetWindowRatio(window.WindowHandle, 0);
                }

                _active = value;
            }
        }

        // Methods and Functions

        public OAspectRatioConstraint(IOPrototype? parent = null)
        {
            _active = true;

            Parent = parent;
        }

        public bool CanProcess<TReturn, TParams>()
        {
            return (typeof(TReturn) == typeof((OVector2<float>, OVector2<float>)) && typeof(TParams) == typeof((OVector2<float>, OVector2<float>)))
                || (typeof(TReturn) == typeof(object) && typeof(TParams) == typeof(double));
        }

        public TReturn? Process<TReturn, TParams>(IOModifier.OModifierCallTime callTime, TParams parameters)
        {
            if (callTime.HasFlag(IOModifier.OModifierCallTime.Layout) && _parent is IOInterface)
                if (parameters is ValueTuple<OVector2<float>, OVector2<float>> value && Ratio > 0)
                {
                    OVector2<float> size = value.Item1;
                    float width = size.X;
                    float height = size.Y;
                    float targetWidth = height * Ratio;
                    float targetHeight = width / Ratio;

                    if (targetWidth <= width)
                        width = targetWidth;
                    else
                        height = targetHeight;

                    return (TReturn)(object)((new OVector2<float>(width, height), value.Item2));
                }

            if (callTime.HasFlag(IOModifier.OModifierCallTime.Behavior) && _parent is OWindow window)
                if (parameters is double)
                {
                    SetWindowRatio(window.WindowHandle, Ratio);

                    return default;
                }

            return (TReturn)(object)parameters!;
        }

        private static void SetWindowRatio(IntPtr handle, float ratio)
        {
            if (SDL.IsMainThread())
            {
                SDL.SetWindowAspectRatio(handle, ratio, ratio);
                SDL.SyncWindow(handle);
            }
            else
            {
                SDL.RunOnMainThread((IntPtr _) =>
                {
                    SDL.SetWindowAspectRatio(handle, ratio, ratio);
                    SDL.SyncWindow(handle);
                }, IntPtr.Zero, false);
            }
        }

        // To String

        public override string ToString()
        {
            return $"<OAspectRatioConstraint Active={Active} Ratio={Ratio}>";
        }
    }

    // public class OBorderStyling : IOModifier
    // {
    //     // Enums

    //     public enum OBorderPlacement
    //     {
    //         Inner,
    //         Center,
    //         Outer
    //     }

    //     // Properties and Fields

    //     private IOPrototype? _parent;

    //     public IOModifier.OModifierCallTime CallTime => IOModifier.OModifierCallTime.PostRender;
    //     public IOModifier.OModifierPriority Priority => IOModifier.OModifierPriority.Normal;
    //     public bool Active { get; set; } = true;
    //     public int Smoothness { get; set; } = 128;
    //     public float TopThickness { get; set; } = 8;
    //     public float RightThickness { get; set; } = 8;
    //     public float BottomThickness { get; set; } = 8;
    //     public float LeftThickness { get; set; } = 8;
    //     public OColor TopColor { get; set; } = OColor.Black;
    //     public OColor RightColor { get; set; } = OColor.Black;
    //     public OColor BottomColor { get; set; } = OColor.Black;
    //     public OColor LeftColor { get; set; } = OColor.Black;
    //     public OLayoutVector<float, float> TopLeftRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
    //     public OLayoutVector<float, float> TopRightRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
    //     public OLayoutVector<float, float> BottomRightRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
    //     public OLayoutVector<float, float> BottomLeftRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
    //     public OLayoutVector2<float, float> Offset { get; set; } = OLayoutVector2<float, float>.Zero;
    //     public OBorderPlacement Placement { get; set; } = OBorderPlacement.Outer;

    //     public IOPrototype? Parent
    //     {
    //         get => _parent;
    //         set
    //         {
    //             if (_parent == value)
    //                 return;

    //             if (value?.HasModifier<OBorderStyling>() ?? false)
    //             {
    //                 ODebugger.Warn($"\"{value.Name}\" already have an OBorderStyling modifier.");

    //                 return;
    //             }

    //             _parent?.RemoveModifier<OBorderStyling>();

    //             _parent = value;

    //             _parent?.AddModifier(this);
    //         }
    //     }

    //     public float Thickness
    //     {
    //         set
    //         {
    //             TopThickness = value;
    //             RightThickness = value;
    //             BottomThickness = value;
    //             LeftThickness = value;
    //         }
    //     }

    //     public OColor Color
    //     {
    //         set
    //         {
    //             TopColor = value;
    //             RightColor = value;
    //             BottomColor = value;
    //             LeftColor = value;
    //         }
    //     }

    //     public OLayoutVector<float, float> Radius
    //     {
    //         set
    //         {
    //             TopLeftRadius = value;
    //             TopRightRadius = value;
    //             BottomRightRadius = value;
    //             BottomLeftRadius = value;
    //         }
    //     }

    //     // Methods and Functions

    //     public OBorderStyling(IOPrototype? parent = null)
    //     {
    //         Parent = parent;
    //     }

    //     public bool CanProcess<TReturn, TParams>()
    //     {
    //         return typeof(TReturn) == typeof(OShapeInfo) && typeof(TParams) == typeof(OShapeInfo);
    //     }

    //     public TReturn? Process<TReturn, TParams>(IOModifier.OModifierCallTime callTime, TParams parameters)
    //     {
            
    //     }

    //     private static TReturn? CastBack<TReturn, TParams>(OShapeInfo outputShape, TParams original)
    //     {
    //         if (typeof(TReturn) == typeof(OShapeInfo))
    //             return (TReturn)(object)outputShape;

    //         if (typeof(TReturn) == typeof(ValueTuple<OShapeInfo>))
    //             return (TReturn)(object)(outputShape);

    //         return (TReturn)(object)original!;
    //     }

    //     // To String

    //     public override string ToString()
    //     {
    //         return $"<OBorderStyling Active={Active} Placement={Placement}>";
    //     }
    // }

    public class OCornerStyling : IOModifier
    {
        // Properties and Fields

        private IOPrototype? _parent;

        public IOModifier.OModifierCallTime CallTime => IOModifier.OModifierCallTime.Render;
        public IOModifier.OModifierPriority Priority => IOModifier.OModifierPriority.Normal;
        public bool Active { get; set; } = true;
        public int Smoothness { get; set; } = 128;
        public OLayoutVector<float, float> TopLeftRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
        public OLayoutVector<float, float> TopRightRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
        public OLayoutVector<float, float> BottomRightRadius { get; set; } = new OLayoutVector<float, float>(0, 8);
        public OLayoutVector<float, float> BottomLeftRadius { get; set; } = new OLayoutVector<float, float>(0, 8);

        public IOPrototype? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                if (value?.HasModifier<OCornerStyling>() ?? false)
                {
                    ODebugger.Warn($"\"{value.Name}\" already have an OCornerStyling modifier.");

                    return;
                }

                _parent?.RemoveModifier<OCornerStyling>();

                _parent = value;

                _parent?.AddModifier(this);
            }
        }

        public OLayoutVector<float, float> Radius
        {
            set
            {
                TopLeftRadius = value;
                TopRightRadius = value;
                BottomRightRadius = value;
                BottomLeftRadius = value;
            }
        }

        // Methods and Functions

        public OCornerStyling(IOPrototype? parent = null)
        {
            Parent = parent;
        }

        public bool CanProcess<TReturn, TParams>()
        {
            return typeof(TReturn) == typeof(ODrawingInfo) && typeof(TParams) == typeof(ValueTuple<ODrawingInfo, OShapeInfo>);
        }

        public TReturn? Process<TReturn, TParams>(IOModifier.OModifierCallTime callTime, TParams parameters)
        {
            if (!CanProcess<TReturn, TParams>())
                return CastBack<TReturn, TParams>(default!, parameters);

            if (callTime != IOModifier.OModifierCallTime.Render)
                return CastBack<TReturn, TParams>(default!, parameters);

            if (parameters is not ValueTuple<ODrawingInfo, OShapeInfo> infos)
                return CastBack<TReturn, TParams>(default!, parameters);

            (ODrawingInfo drawing, OShapeInfo shape) = infos;

            if (_parent is not IOInterface gui)
                return CastBack<TReturn, TParams>(drawing, parameters);
            
            ReadOnlyCollection<OVector2<float>> points = shape.GetPoints();

            if (shape.Lines.Count != 4 || points.Count != 4)
                return CastBack<TReturn, TParams>(drawing, parameters);
            
            for (int i = 0; i < 4; i++)
            {
                OVector2<float> current = points[i];
                OVector2<float> prev = points[(i + 3) % 4];
                OVector2<float> next = points[(i + 1) % 4];
                OVector2<float> v1 = prev - current;
                OVector2<float> v2 = next - current;
                float dot = OVector2<float>.Dot(v1, v2);
                float det = v1.X * v2.Y - v1.Y * v2.X;
                float between = MathF.Atan2(det, dot) * (180f / MathF.PI);
                float angle = MathF.Abs(between);

                if (MathF.Abs(angle - 90f) > 5f)
                    return (TReturn)(object)drawing;
            }

            float min = MathF.Min(gui.AbsoluteSize.X, gui.AbsoluteSize.Y);
            float max = min / 2;
            float topLeftRadius = (min * TopLeftRadius.Scale) + TopLeftRadius.Offset;
            float topRightRadius = (min * TopRightRadius.Scale) + TopRightRadius.Offset;
            float bottomRightRadius = (min * BottomRightRadius.Scale) + BottomRightRadius.Offset;
            float bottomLeftRadius = (min * BottomLeftRadius.Scale) + BottomLeftRadius.Offset;

            topLeftRadius = MathF.Min(topLeftRadius, max);
            topRightRadius = MathF.Min(topRightRadius, max);
            bottomRightRadius = MathF.Min(bottomRightRadius, max);
            bottomLeftRadius = MathF.Min(bottomLeftRadius, max);

            (float topLeft, float topRight, float bottomRight, float bottomLeft) radius = (topLeftRadius, topRightRadius, bottomRightRadius, bottomLeftRadius);
            OShapeInfo rounded = OShapes.RoundedRectangle(gui.AbsoluteSize, gui.AbsolutePosition, gui.Rotation, shape.Layer, radius, shape.Color, Smoothness, shape.Mask);

            shape.Lines.Clear();
            shape.Lines.AddRange(rounded.Lines);

            return CastBack<TReturn, TParams>(drawing, parameters);
        }
        
        private static TReturn? CastBack<TReturn, TParams>(ODrawingInfo outputDrawing, TParams original)
        {
            if (typeof(TReturn) == typeof(ODrawingInfo))
                return (TReturn)(object)outputDrawing;

            return (TReturn)(object)original!;
        }

        // To String

        public override string ToString()
        {
            return $"<OCornerStyling Active={Active} TopLeftRadius={TopLeftRadius} TopRightRadius={TopRightRadius} BottomRightRadius={BottomRightRadius} BottomLeftRadius={BottomLeftRadius}>";
        }
    }    
}