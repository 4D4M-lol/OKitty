// Imports

using static OKitty.OkInstance;
using static OKitty.OkMath;
using static OKitty.OkScript;
using static OKitty.OkStyling;
using System.Collections.ObjectModel;

namespace OKitty;

// OkInterface

public static class OkInterface
{
    // Interfaces

    public interface IOInterface : IOInstance
    {
        // Enums

        public enum OAutoSizeMode
        {
            None,
            ExtendHorizontally,
            ExtendVertically,
            ExtendBoth,
            ShrinkHorizontally,
            ShrinkVertically,
            ShrinkBoth
        }

        // Properties

        public bool Active { get; set; }
        public bool Visible { get; set; }
        public OVector2<float> AbsoluteSize { get; }
        public OVector2<float> AbsolutePosition { get; }
        public OAutoSizeMode AutoSizeMode { get; set; }
        public OLayoutVector2<float, float> Size { get; set; }
        public OLayoutVector2<float, float> Position { get; set; }
        public float Rotation { get; set; }
        public int Layer { get; set; }
        public OColor BackgroundColor { get; set; }
        public float BackgroundOpacity { get; set; }
        public bool Clip { get; set; }
    }

    public interface IOGui : IOInstance
    {
        // Enums

        public enum OGuiType
        {
            Shape,
            Geometry    
        }

        // Properties

        public OGuiType Type { get; }
        public int Layer { get; set; }
        public bool Active { get; set; }
        public bool Visible { get; set; }
        public bool Clip { get; set; }
    }

    // Classes

    public static class OShapes
    {
        // Enums

        public enum OTriangleType
        {
            Acute,
            Equilateral,
            Isosceles,
            Obtuse,
            Right,
            Scalene
        }

        // Method and Functions

        public static OShapeInfo Triangle(OVector2<float> size, OVector2<float> position, float rotation, int layer, OTriangleType type, OColor color, OShapeInfo? mask = null)
        {
            (OVector2<float> p1, OVector2<float> p2, OVector2<float> p3) = (new OVector2<float>(), new OVector2<float>(), new OVector2<float>());

            switch (type)
            {
                case OTriangleType.Acute:
                    p1 = new OVector2<float>(0, size.Y);
                    p2 = new OVector2<float>(size.X, size.Y);
                    p3 = new OVector2<float>(size.X * 0.25f, 0);

                    break;
                case OTriangleType.Equilateral:
                    float side = size.X;
                    float height = side * (float)Math.Sqrt(3) / 2f;

                    p1 = new OVector2<float>(0, height);
                    p2 = new OVector2<float>(side, height);
                    p3 = new OVector2<float>(side / 2f, 0);

                    break;
                case OTriangleType.Isosceles:
                    p1 = new OVector2<float>(0, size.Y);
                    p2 = new OVector2<float>(size.X, size.Y);
                    p3 = new OVector2<float>(size.X / 2f, 0);

                    break;
                case OTriangleType.Obtuse:
                    p1 = new OVector2<float>(0, size.Y);
                    p2 = new OVector2<float>(size.X, size.Y);
                    p3 = new OVector2<float>(size.X * 0.2f, 0);

                    break;
                case OTriangleType.Right:
                    p2 = new OVector2<float>(size.X, 0);
                    p3 = new OVector2<float>(0, size.Y);

                    break;
                case OTriangleType.Scalene:
                    p1 = new OVector2<float>(0, size.Y);
                    p2 = new OVector2<float>(size.X, size.Y * 0.8f);
                    p3 = new OVector2<float>(size.X * 0.1f, 0);

                    break;
            }

            if (rotation != 0)
            {
                OVector2<float> center = size / 2;
                float cos = MathF.Cos(rotation * MathF.PI / 180.0f);
                float sin = MathF.Sin(rotation * MathF.PI / 180.0f);

                p1 = new OVector2<float>(
                    ((p1.X - center.X) * cos - (p1.Y - center.Y) * sin) + center.X,
                    ((p1.X - center.X) * sin + (p1.Y - center.Y) * cos) + center.Y
                );
                p2 = new OVector2<float>(
                    ((p2.X - center.X) * cos - (p2.Y - center.Y) * sin) + center.X,
                    ((p2.X - center.X) * sin + (p2.Y - center.Y) * cos) + center.Y
                );
                p3 = new OVector2<float>(
                    ((p3.X - center.X) * cos - (p3.Y - center.Y) * sin) + center.X,
                    ((p3.X - center.X) * sin + (p3.Y - center.Y) * cos) + center.Y
                );
            }

            p1 += position;
            p2 += position;
            p3 += position;

            List<OLineInfo> lines = new List<OLineInfo>()
            {
                new OLineInfo() { Start = p1, End = p2, Color = color },
                new OLineInfo() { Start = p2, End = p3, Color = color },
                new OLineInfo() { Start = p3, End = p1, Color = color }
            };
            OShapeInfo shape = new OShapeInfo()
            {
                Lines = lines,
                Mask = mask,
                Color = color,
                Layer = layer
            };

            return shape;
        }

        public static OShapeInfo Rectangle(OVector2<float> size, OVector2<float> position, float rotation, int layer, OColor color, OShapeInfo? mask = null)
        {
            (OVector2<float> p1, OVector2<float> p2, OVector2<float> p3, OVector2<float> p4) = (
                new OVector2<float>(), new OVector2<float>(size.X, 0), new OVector2<float>(size.X, size.Y),
                new OVector2<float>(0, size.Y)
            );

            if (rotation != 0)
            {
                OVector2<float> center = size / 2;
                float radius = rotation * MathF.PI / 180.0f;
                float cos = MathF.Cos(radius);
                float sin = MathF.Sin(radius);

                p1 = new OVector2<float>(
                    ((p1.X - center.X) * cos - (p1.Y - center.Y) * sin) + center.X,
                    ((p1.X - center.X) * sin + (p1.Y - center.Y) * cos) + center.Y
                );
                p2 = new OVector2<float>(
                    ((p2.X - center.X) * cos - (p2.Y - center.Y) * sin) + center.X,
                    ((p2.X - center.X) * sin + (p2.Y - center.Y) * cos) + center.Y
                );
                p3 = new OVector2<float>(
                    ((p3.X - center.X) * cos - (p3.Y - center.Y) * sin) + center.X,
                    ((p3.X - center.X) * sin + (p3.Y - center.Y) * cos) + center.Y
                );
                p4 = new OVector2<float>(
                    ((p4.X - center.X) * cos - (p4.Y - center.Y) * sin) + center.X,
                    ((p4.X - center.X) * sin + (p4.Y - center.Y) * cos) + center.Y
                );
            }

            p1 += position;
            p2 += position;
            p3 += position;
            p4 += position;

            List<OLineInfo> lines = new List<OLineInfo>()
            {
                new OLineInfo() { Start = p1, End = p2, Color = color },
                new OLineInfo() { Start = p2, End = p3, Color = color },
                new OLineInfo() { Start = p3, End = p4, Color = color },
                new OLineInfo() { Start = p4, End = p1, Color = color }
            };
            OShapeInfo shape = new OShapeInfo()
            {
                Lines = lines,
                Mask = mask,
                Color = color,
                Layer = layer
            };

            return shape;
        }
        
        public static OShapeInfo RoundedRectangle(
            OVector2<float> size, OVector2<float> position, float rotation, int layer,  (float topLeft, float topRight, float bottomRight, float bottomLeft) radius, OColor color,
            int smoothness = 24, OShapeInfo? mask = null
        )
        {
            float width = size.X;
            float height = size.Y;
            float maxRadius = MathF.Min(width, height) / 2;
            float tl = Math.Clamp(radius.topLeft, 0, maxRadius);
            float tr = Math.Clamp(radius.topRight, 0, maxRadius);
            float br = Math.Clamp(radius.bottomRight, 0, maxRadius);
            float bl = Math.Clamp(radius.bottomLeft, 0, maxRadius);
            List<OVector2<float>> points = new List<OVector2<float>>();

            if (tl > 0)
                AddArc(points, new OVector2<float>(tl, tl), 180, 270, tl, smoothness);
            else
                points.Add(new OVector2<float>(0, 0));
            
            if (tr > 0)
                points.Add(new OVector2<float>(width - tr, 0));
            else
                points.Add(new OVector2<float>(width, 0));
            
            if (tr > 0)
                AddArc(points, new OVector2<float>(width - tr, tr), 270, 360, tr, smoothness);
            
            if (br > 0)
                points.Add(new OVector2<float>(width, height - br));
            else
                points.Add(new OVector2<float>(width, height));
            
            if (br > 0)
                AddArc(points, new OVector2<float>(width - br, height - br), 0, 90, br, smoothness);

            if (bl > 0)
                points.Add(new OVector2<float>(bl, height));
            else
                points.Add(new OVector2<float>(0, height));
            
            if (bl > 0)
                AddArc(points, new OVector2<float>(bl, height - bl), 90, 180, bl, smoothness);
            
            if (tl > 0)
                points.Add(new OVector2<float>(0, tl));
            
            if (rotation != 0)
            {
                OVector2<float> center = size / 2;
                float rad = rotation * MathF.PI / 180.0f;
                float cos = MathF.Cos(rad);
                float sin = MathF.Sin(rad);

                for (int i = 0; i < points.Count; i++)
                {
                    OVector2<float> point = points[i];
                    points[i] = new OVector2<float>(
                        ((point.X - center.X) * cos - (point.Y - center.Y) * sin) + center.X,
                        ((point.X - center.X) * sin + (point.Y - center.Y) * cos) + center.Y
                    );
                }
            }
            
            for (int i = 0; i < points.Count; i++)
                points[i] += position;
            
            List<OLineInfo> lines = new List<OLineInfo>();
            
            for (int i = 0; i < points.Count; i++)
            {
                int next = (i + 1) % points.Count;
                
                lines.Add(new OLineInfo()
                {
                    Start = points[i],
                    End = points[next],
                    Color = color
                });
            }
            
            return new OShapeInfo()
            {
                Lines = lines,
                Mask = mask,
                Color = color,
                Layer = layer
            };
        }

        public static OShapeInfo RoundedRectangle(OVector2<float> size, OVector2<float> position, float rotation, int layer, float radius, OColor color, int smoothness = 24, OShapeInfo? mask = null)
        {
            return RoundedRectangle(size, position, rotation, layer, (radius, radius, radius, radius), color, smoothness, mask);
        }

        public static OShapeInfo Circle(float radius, OVector2<float> position, int layer, OColor color, int smoothness = 24, OShapeInfo? mask = null)
        {
            float diameter = radius * 2;
            OVector2<float> size = new OVector2<float>(diameter, diameter);
            OVector2<float> center = new OVector2<float>(position.X - radius, position.Y - radius);

            return RoundedRectangle(size, center, 0, layer, radius, color, smoothness, mask);
        }

        private static void AddArc(List<OVector2<float>> points, OVector2<float> center, float startAngleDeg, float endAngleDeg, float r, int smoothness)
        {
            if (r <= 0)
                return;

            float start = startAngleDeg * MathF.PI / 180;
            float end = endAngleDeg * MathF.PI / 180;
            int segments = smoothness;
            
            for (int i = 0; i <= segments; i++)
            {
                float t = i / (float)segments;
                float angle = start + (end - start) * t;
                
                points.Add(new OVector2<float>(
                    center.X + MathF.Cos(angle) * r,
                    center.Y + MathF.Sin(angle) * r
                ));
            }
        }
    }

    public class OScreenGui : IOGui
    {
        // Properties and Fields

        private IOInstance? _parent;
        private List<IOInstance> _children;
        private List<IOModifier> _modifiers;

        public IOGui.OGuiType Type => IOGui.OGuiType.Shape;
        public HashSet<string> Tags { get; } = new HashSet<string>();
        public string Name { get; set; } = "OScreenGui";
        public int Layer { get; set; } = 0;
        public bool Active { get; set; } = true;
        public bool Visible { get; set; } = true;
        public bool Clip { get; set; } = false;

        public IOInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                _parent?.RemoveChild(this);

                _parent = value;

                _parent?.AddChild(this);
            }
        }

        // Events

        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;

        // Methods and Functions

        public OScreenGui(IOInstance? parent = null, string name = "OScreenGui")
        {
            _parent = parent;
            _children = new List<IOInstance>();
            _modifiers = new List<IOModifier>();

            Name = name;

            _parent?.AddChild(this);
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public void AddChild(IOInstance child)
        {
            if (_children.Contains(child))
                return;

            _children.Add(child);

            if (child.Parent != this)
                child.Parent = this;

            OnChildAdded?.Invoke(child);
        }

        public void RemoveChild(IOInstance child)
        {
            if (!_children.Remove(child))
                return;

            if (child.Parent == this)
                child.Parent = null;

            OnChildRemoved?.Invoke(child);
        }

        public ReadOnlyCollection<IOModifier> GetModifiers()
        {
            return new ReadOnlyCollection<IOModifier>(_modifiers);
        }

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().FirstOrDefault();
        }

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier
        {
            if (HasModifier<TModifier>())
            {
                ODebugger.Warn($"\"{Name}\" already have a(n) {modifier.GetType().Name} modifier.");

                return;
            }

            _modifiers.Add(modifier);

            if (modifier.Parent != this)
                modifier.Parent = this;
        }

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            TModifier? modifier = GetModifier<TModifier>();

            if (modifier is not null)
            {
                if (modifier.Parent == this)
                    modifier.Parent = null;

                _modifiers.Remove(modifier);
            }
        }

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().Any();
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            foreach (IOInstance child in _children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            foreach (IOInstance child in _children)
                if (child is TInstance instance)
                    return instance;

            return null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
            {
                if (next.Name == name)
                    return next;

                if (next is OStorage storage && storage.Window.Name == name)
                    return storage.Window;

                if (next is OScenes scenes && scenes.Window.Name == name)
                    return scenes.Window;
            }

            return null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
            {
                for (IOInstance? next = Parent; next != null; next = next.Parent)
                {
                    if (next is OStorage storage)
                        return storage.Window as TPrototype;

                    if (next is OScenes scenes)
                        return scenes.Window as TPrototype;
                }

                return null;
            }

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype instance)
                    return instance;

            return null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return Parent is TPrototype;
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
                return IsADescendantOf<OStorage>() || IsADescendantOf<OScenes>();

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype)
                    return true;

            return false;
        }

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            OScreenGui clone = new OScreenGui()
            {
                Parent = _parent,
                Name = Name,
                Layer = Layer,
                Active = Active,
                Visible = Visible,
                Clip = Clip
            };

            foreach (string tag in Tags)
                clone.Tags.Add(tag);

            if (cloneChildren || cloneDescendants)
                foreach (IOInstance child in _children)
                {
                    IOInstance? childClone = (IOInstance?)child.Clone(cloneDescendants, cloneDescendants);

                    if (childClone != null)
                        clone.AddChild(childClone);
                }

            return clone;
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();

            _children.Clear();
        }

        public ORenderInfo? Render()
        {
            if (!Visible)
                return null;

            if (_parent is not OScene scene)
                return null;

            OWindow? window = scene.FindFirstAncestorWhichIsA<OWindow>();

            if (window is null)
                return null;

            ORenderInfo renderInfo = new ORenderInfo();
            IOInstance[] children = _children.ToArray();
            IOModifier[] modifiers = _modifiers.ToArray();

            foreach (IOModifier modifier in modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PreRender))
                    modifier.Process<object, object>( IOModifier.OModifierCallTime.PreRender, this);

            foreach (IOModifier modifier in modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PreRenderChildren))
                    modifier.Process<object, object>( IOModifier.OModifierCallTime.PreRenderChildren, this);

            foreach (IOInstance child in children)
            {
                ORenderInfo? childInfo = child.Render();

                if (childInfo is null)
                    continue;

                foreach (ODrawingInfo drawingInfo in childInfo.Drawings)
                {
                    ODrawingInfo clippedDrawing = ODrawingInfo.Empty;

                    foreach (OShapeInfo shapeInfo in drawingInfo.Shapes)
                    {
                        OShapeInfo clipped = OShapeInfo.Clip(shapeInfo);

                        clippedDrawing.Shapes.Add(new OShapeInfo()
                        {
                            Lines = clipped.Lines,
                            Color = clipped.Color,
                            Mask = Clip ? window.SafeArea : null,
                            Layer = clipped.Layer
                        });
                    }

                    renderInfo.Drawings.Add(clippedDrawing);
                }

                foreach (IOModifier modifier in modifiers) 
                    if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.RenderChildren)) 
                        modifier.Process<ORenderInfo?, ORenderInfo?>(IOModifier.OModifierCallTime.RenderChildren, childInfo);
            }

            foreach (IOModifier modifier in modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PostRenderChildren))
                    renderInfo = modifier.Process<ORenderInfo?, ORenderInfo?>(IOModifier.OModifierCallTime.PostRenderChildren, renderInfo) ?? renderInfo;

            foreach (IOModifier modifier in modifiers)
                if (modifier.Active && modifier.CallTime.HasFlag(IOModifier.OModifierCallTime.PostRender))
                    renderInfo = modifier.Process<ORenderInfo?, ORenderInfo?>(IOModifier.OModifierCallTime.PostRender, renderInfo) ?? renderInfo;

            return renderInfo;
        }
    }

    public class OFrame : IOInterface
    {
        // Properties and Fields

        private IOInstance? _parent;
        private List<IOInstance> _children;
        private List<IOModifier> _modifiers;
        private OVector2<float> _absoluteSize;
        private OVector2<float> _absolutePosition;
        private float _rotation;
        private float _backgroundOpacity;

        public OVector2<float> AbsoluteSize => _absoluteSize;
        public OVector2<float> AbsolutePosition => _absolutePosition;
        public HashSet<string> Tags { get; } = new HashSet<string>();
        public string Name { get; set; } = "OFrame";
        public bool Active { get; set; } = true;
        public bool Visible { get; set; } = true;
        public IOInterface.OAutoSizeMode AutoSizeMode { get; set; } = IOInterface.OAutoSizeMode.None;
        public OLayoutVector2<float, float> Size { get; set; } = OLayoutVector2<float, float>.Half;
        public OLayoutVector2<float, float> Position { get; set; } = OLayoutVector2<float, float>.Zero;
        public int Layer { get; set; } = 0;
        public OColor BackgroundColor { get; set; } = OColor.White;
        public bool Clip { get; set; } = false;

        public IOInstance? Parent
        {
            get => _parent;
            set
            {
                if (_parent == value)
                    return;

                _parent?.RemoveChild(this);

                _parent = value;

                _parent?.AddChild(this);
            }
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = Math.Clamp(value, -180, 180);
        }

        public float BackgroundOpacity
        {
            get => _backgroundOpacity;
            set => _backgroundOpacity = Math.Clamp(value, 0, 1);
        }

        // Events

        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;

        // Methods and Functions

        public OFrame(IOInstance? parent = null, string name = "OFrame")
        {
            _parent = parent;
            _children = new List<IOInstance>();
            _modifiers = new List<IOModifier>();
            _absoluteSize = OVector2<float>.NegativeOne;
            _absolutePosition = OVector2<float>.NegativeOne;
            _rotation = 0;
            _backgroundOpacity = 1;

            Name = name;

            parent?.AddChild(this);
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return new ReadOnlyCollection<IOInstance>(_children);
        }

        public void AddChild(IOInstance child)
        {
            if (_children.Contains(child))
                return;

            _children.Add(child);

            if (child.Parent != this)
                child.Parent = this;

            OnChildAdded?.Invoke(child);
        }

        public void RemoveChild(IOInstance child)
        {
            if (!_children.Remove(child))
                return;

            if (child.Parent == this)
                child.Parent = null;

            OnChildRemoved?.Invoke(child);
        }

        public ReadOnlyCollection<IOModifier> GetModifiers()
        {
            return new ReadOnlyCollection<IOModifier>(_modifiers);
        }

        public TModifier? GetModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().FirstOrDefault();
        }

        public void AddModifier<TModifier>(TModifier modifier)
            where TModifier : class, IOModifier
        {
            if (HasModifier<TModifier>())
            {
                ODebugger.Warn($"\"{Name}\" already have a(n) {modifier.GetType().Name} modifier.");

                return;
            }

            _modifiers.Add(modifier);

            _modifiers = _modifiers
                .OrderByDescending((IOModifier modifier) => modifier.Priority)
                .ToList();

            if (modifier.Parent != this)
                modifier.Parent = this;
        }

        public void RemoveModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            TModifier? modifier = GetModifier<TModifier>();

            if (modifier is not null)
            {
                if (modifier.Parent == this)
                    modifier.Parent = null;

                _modifiers.Remove(modifier);
            }
        }

        public bool HasModifier<TModifier>()
            where TModifier : class, IOModifier
        {
            return _modifiers.OfType<TModifier>().Any();
        }

        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();

            descendants.AddRange(_children);

            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());

            return new ReadOnlyCollection<IOInstance>(descendants);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildSelector(Func<IOInstance, int, bool> selector)
        {
            List<IOInstance> selected = _children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> Descendantselector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            foreach (IOInstance child in _children)
                if (child.Name == name)
                    return child;

            return null;
        }

        public TInstance? FindFirstChildWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            foreach (IOInstance child in _children)
                if (child is TInstance instance)
                    return instance;

            return null;
        }

        public IOInstance? FindFirstDescendantNamed(string name)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.Name == name)
                    return descendant;

            return null;
        }

        public TInstance? FindFirstDescendantWhichIsA<TInstance>()
            where TInstance : class, IOInstance
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant is TInstance instance)
                    return instance;

            return null;
        }

        public IOPrototype? FindFirstAncestorNamed(string name)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
            {
                if (next.Name == name)
                    return next;

                if (next is OStorage storage && storage.Window.Name == name)
                    return storage.Window;

                if (next is OScenes scenes && scenes.Window.Name == name)
                    return scenes.Window;
            }

            return null;
        }

        public TPrototype? FindFirstAncestorWhichIsA<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
            {
                for (IOInstance? next = Parent; next != null; next = next.Parent)
                {
                    if (next is OStorage storage)
                        return storage.Window as TPrototype;

                    if (next is OScenes scenes)
                        return scenes.Window as TPrototype;
                }

                return null;
            }

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype instance)
                    return instance;

            return null;
        }

        public bool IsAChildOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            return Parent is TPrototype;
        }

        public bool IsADescendantOf<TPrototype>()
            where TPrototype : class, IOPrototype
        {
            if (typeof(TPrototype) == typeof(OWindow))
                return IsADescendantOf<OStorage>() || IsADescendantOf<OScenes>();

            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next is TPrototype)
                    return true;

            return false;
        }

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            OFrame clone = new OFrame()
            {
                Parent = _parent,
                Name = Name,
                Active = Active,
                Visible = Visible,
                AutoSizeMode = AutoSizeMode,
                Size = Size,
                Position = Position,
                Rotation = _rotation,
                Layer = Layer,
                BackgroundColor = BackgroundColor,
                BackgroundOpacity = _backgroundOpacity,
                Clip = Clip
            };

            foreach (string tag in Tags)
                clone.Tags.Add(tag);

            if (cloneChildren || cloneDescendants)
                foreach (IOInstance child in _children)
                {
                    IOInstance? childClone = (IOInstance?)child.Clone(cloneDescendants, cloneDescendants);

                    if (childClone != null)
                        clone.AddChild(childClone);
                }

            return clone;
        }

        public void Dispose()
        {
            foreach (IOInstance child in _children)
                child.Dispose();

            _children.Clear();
        }

        public ORenderInfo? Render()
        {
            if (_parent is not IOGui && _parent is not IOInterface)
                return null;

            if (!Visible)
                return null;

            OWindow? window = _parent.FindFirstAncestorWhichIsA<OWindow>();

            if (window is null)
                return null;

            IOInstance[] children = _children.ToArray();
            IOModifier[] modifiers = _modifiers.ToArray();

            if (_parent is IOGui)
            {
                _absoluteSize = new OVector2<float>((window.Size.X * Size.Scale.X) + Size.Offset.X, (window.Size.Y * Size.Scale.Y) + Size.Offset.Y);
                _absolutePosition = new OVector2<float>((window.Size.X * Position.Scale.X) + Position.Offset.X, (window.Size.Y * Position.Scale.Y) + Position.Offset.Y);
            }
            else if (_parent is IOInterface parentInterface)
            {
                _absoluteSize = new OVector2<float>((parentInterface.AbsoluteSize.X * Size.Scale.X) + Size.Offset.X, (parentInterface.AbsoluteSize.Y * Size.Scale.Y) + Size.Offset.Y);
                _absolutePosition = new OVector2<float>(
                    (parentInterface.AbsoluteSize.X * Position.Scale.X) + Position.Offset.X + parentInterface.AbsolutePosition.X,
                    (parentInterface.AbsoluteSize.Y * Position.Scale.Y) + Position.Offset.Y + parentInterface.AbsolutePosition.Y
                );
            }

            (OVector2<float>, OVector2<float>) layoutResult = RunModifierPipeline<(OVector2<float>, OVector2<float>), (OVector2<float>, OVector2<float>)>(
                modifiers, IOModifier.OModifierCallTime.Layout, (_absoluteSize, _absolutePosition)
            );

            if (layoutResult != default)
                (_absoluteSize, _absolutePosition) = layoutResult;

            (OVector2<float>, OVector2<float>) layoutBeforeResult = RunModifierPipeline<(OVector2<float>, OVector2<float>), (OVector2<float>, OVector2<float>)>(
                modifiers, IOModifier.OModifierCallTime.LayoutBeforeChildren, (_absoluteSize, _absolutePosition)
            );

            if (layoutBeforeResult != default)
                (_absoluteSize, _absolutePosition) = layoutBeforeResult;

            foreach (IOInstance child in _children)
                RunModifierPipeline<IOInstance, IOInstance>(modifiers, IOModifier.OModifierCallTime.LayoutChildren, child);

            OShapeInfo baseFrame = OShapes.Rectangle(_absoluteSize, _absolutePosition, _rotation, Layer, BackgroundColor);
            ODrawingInfo baseDrawing = ODrawingInfo.Empty;

            baseDrawing.Shapes.Add(baseFrame);

            baseDrawing = RunModifierPipeline<ODrawingInfo, (ODrawingInfo, OShapeInfo)>(modifiers, IOModifier.OModifierCallTime.PreRender, (baseDrawing, baseFrame)) ?? baseDrawing;
            baseDrawing = RunModifierPipeline<ODrawingInfo, (ODrawingInfo, OShapeInfo)>(modifiers, IOModifier.OModifierCallTime.Render, (baseDrawing, baseFrame)) ?? baseDrawing;
            baseDrawing = RunModifierPipeline<ODrawingInfo, (ODrawingInfo, OShapeInfo)>(modifiers, IOModifier.OModifierCallTime.PostRender, (baseDrawing, baseFrame)) ?? baseDrawing;

            List<ODrawingInfo> childDrawings = new List<ODrawingInfo>();

            foreach (IOInstance child in children)
            {
                ORenderInfo? childInfo = child.Render();

                if (childInfo == null)
                    continue;

                childDrawings.AddRange(childInfo.Drawings);
            }

            for (int i = 0; i < childDrawings.Count; i++)
                childDrawings[i] = RunModifierPipeline<ODrawingInfo, ODrawingInfo>(modifiers, IOModifier.OModifierCallTime.PreRenderChildren, childDrawings[i]) ?? childDrawings[i];

            for (int i = 0; i < childDrawings.Count; i++)
                childDrawings[i] = RunModifierPipeline<ODrawingInfo, ODrawingInfo>(modifiers, IOModifier.OModifierCallTime.RenderChildren, childDrawings[i]) ?? childDrawings[i];

            for (int i = 0; i < childDrawings.Count; i++)
                childDrawings[i] = RunModifierPipeline<ODrawingInfo, ODrawingInfo>(modifiers, IOModifier.OModifierCallTime.PostRenderChildren, childDrawings[i]) ?? childDrawings[i];

            if (AutoSizeMode != IOInterface.OAutoSizeMode.None && childDrawings.Count > 0)
                ApplyAutoSize(childDrawings, ref baseFrame);

            ORenderInfo renderInfo = new ORenderInfo();

            renderInfo.Drawings.Add(baseDrawing);

            foreach (ODrawingInfo drawing in childDrawings)
            {
                ODrawingInfo clippedDrawing = ODrawingInfo.Empty;

                foreach (OShapeInfo shape in drawing.Shapes)
                {
                    OShapeInfo clipped = OShapeInfo.Clip(shape);

                    clippedDrawing.Shapes.Add(new OShapeInfo()
                    {
                        Lines = clipped.Lines,
                        Color = clipped.Color,
                        Mask = Clip ? baseFrame : null,
                        Layer = clipped.Layer + Layer
                    });
                }

                renderInfo.Drawings.Add(clippedDrawing);
            }

            return renderInfo;
        }
        
        private TReturn? RunModifierPipeline<TReturn, TParams>(IOModifier[] modifiers, IOModifier.OModifierCallTime callTime, TParams parameters)
        {
            foreach (IOModifier modifier in modifiers)
            {
                if (!modifier.Active || !modifier.CallTime.HasFlag(callTime))
                    continue;

                if (!modifier.CanProcess<TReturn, TParams>())
                    continue;

                TReturn? result = modifier.Process<TReturn, TParams>(callTime, parameters);

                if (result is not null)
                    return result;
            }

            return default;
        }

        private void ApplyAutoSize(List<ODrawingInfo> childDrawings, ref OShapeInfo baseFrame)
        {
            if (AutoSizeMode == IOInterface.OAutoSizeMode.None || childDrawings.Count == 0)
                return;

            GetBoundingBox(childDrawings, out float minX, out float minY, out float maxX, out float maxY);

            float childWidth = maxX - minX;
            float childHeight = maxY - minY;
            float currentX = _absoluteSize.X;
            float currentY = _absoluteSize.Y;
            float newX = currentX;
            float newY = currentY;
            float frameLeft = _absolutePosition.X;
            float frameTop = _absolutePosition.Y;
            float frameRight = frameLeft + currentX;
            float frameBottom = frameTop + currentY;
            bool extendH = AutoSizeMode is IOInterface.OAutoSizeMode.ExtendHorizontally or IOInterface.OAutoSizeMode.ExtendBoth;
            bool extendV = AutoSizeMode is IOInterface.OAutoSizeMode.ExtendVertically or IOInterface.OAutoSizeMode.ExtendBoth;
            bool shrinkH = AutoSizeMode is IOInterface.OAutoSizeMode.ShrinkHorizontally or IOInterface.OAutoSizeMode.ShrinkBoth;
            bool shrinkV = AutoSizeMode is IOInterface.OAutoSizeMode.ShrinkVertically or IOInterface.OAutoSizeMode.ShrinkBoth;

            if (extendH && maxX > frameRight)
                newX = maxX - frameLeft;

            if (extendH && minX < frameLeft)
                newX = frameRight - minX;

            if (shrinkH)
                newX = childWidth;

            newX = Math.Max(1, newX);

            if (extendV && maxY > frameBottom)
                newY = maxY - frameTop;

            if (extendV && minY < frameTop)
                newY = frameBottom - minY;

            if (shrinkV)
                newY = childHeight;

            newY = Math.Max(1, newY);

            _absoluteSize = new OVector2<float>(newX, newY);
            
            baseFrame = OShapes.Rectangle(_absoluteSize, _absolutePosition, _rotation, Layer, BackgroundColor);
        }

        private static void GetBoundingBox(List<ODrawingInfo> drawings, out float minX, out float minY, out float maxX, out float maxY)
        {
            minX = float.MaxValue;
            minY = float.MaxValue;
            maxX = float.MinValue;
            maxY = float.MinValue;

            foreach (ODrawingInfo drawing in drawings)
                foreach (OShapeInfo shape in drawing.Shapes)
                {
                    foreach (OLineInfo line in shape.Lines)
                    {
                        if (line.Start.X < minX)
                            minX = line.Start.X;

                        if (line.Start.Y < minY)
                            minY = line.Start.Y;

                        if (line.Start.X > maxX)
                            maxX = line.Start.X;

                        if (line.Start.Y > maxY)
                            maxY = line.Start.Y;

                        if (line.End.X < minX)
                            minX = line.End.X;

                        if (line.End.Y < minY)
                            minY = line.End.Y;

                        if (line.End.X > maxX)
                            maxX = line.End.X;

                        if (line.End.Y > maxY)
                            maxY = line.End.Y;
                    }
                }
        }

        // To String

        public override string ToString()
        {
            return $"<OFrame Name=\"{Name}\" Size=({Size} Position=({Position}) Rotation={_rotation}°>";
        }
    }
}