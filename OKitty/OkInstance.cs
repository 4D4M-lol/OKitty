// Imports

using System.Collections.ObjectModel;
using System.Data;
using static OKitty.OkMath;
using static OKitty.OkScript;

namespace OKitty;

// OkInstance

public static class OkInstance
{
    // Interfaces

    public interface IOPrototype : IDisposable
    {
        // Properties
        
        public string Icon { get; }
        public string InstanceName { get; }
        public string Name { get; set; }
        
        // Methods

        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants);
        public ORenderInfo? Render();

        public string ToString()
        {
            return $"<{InstanceName} Name=\"{Name}\">";
        }
    }
    
    public interface IOInstance : IOPrototype
    {
        // Properties
        
        public IOInstance? Parent { get; set; }
        public HashSet<string> Tags { get; }
        
        // Methods and Functions

        public ReadOnlyCollection<IOInstance> GetChildren();
        public ReadOnlyCollection<IOInstance> GetDescendants();

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> ChildrenSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();
            List<IOInstance> selected = children.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public ReadOnlyCollection<IOInstance> DescendantsSelector(Func<IOInstance, int, bool> selector)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();
            List<IOInstance> selected = descendants.Where(selector).ToList();

            return new ReadOnlyCollection<IOInstance>(selected);
        }

        public IOInstance? FindFirstChildNamed(string name)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();

            foreach (IOInstance child in children)
                if (child.Name == name)
                    return child;
            
            return null;
        }

        public IOInstance? FindFirstChildWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> children = GetChildren();

            foreach (IOInstance child in children)
                if (child.InstanceName == instance)
                    return child;
            
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
        
        public IOInstance? FindFirstDescendantWhichIsA(string instance)
        {
            ReadOnlyCollection<IOInstance> descendants = GetDescendants();

            foreach (IOInstance descendant in descendants)
                if (descendant.InstanceName == instance)
                    return descendant;
            
            return null;
        }

        public bool IsAChildOf(string instance)
        {
            return Parent is not null && Parent.InstanceName == instance;
        }

        public bool IsADescendantOf(string instance)
        {
            for (IOInstance? next = Parent; next != null; next = next.Parent)
                if (next.InstanceName == instance)
                    return true;

            return false;
        }
        
        // Events

        public event OInstanceEvents.OnChildAdded OnChildAdded;
        public event OInstanceEvents.OnChildRemoved OnChildRemoved;
    }
    
    // Structs
    
    public struct OEdgeInfo
    {
        // Properties
        
        public required OVector2<float> Start { get; init; }
        public required OVector2<float> End { get; init; }
        
        // To String

        public override string ToString()
        {
            return $"{Start} - {End}";
        }
    }
    
    public struct OFaceInfo
    {
        // Properties
        
        public required List<OEdgeInfo> Edges { get; init; }
        
        // To String

        public override string ToString()
        {
            return $"[{{string.Join(\", \", Edges)}}]";
        }
    }
    
    // Classes
    
    public class ORenderInfo
    {
        // Properties and Fields

        public List<OFaceInfo> Faces { get; private set; }
        public OFaceInfo? Mask { get; set; }
        
        // Methods and Functions

        public ORenderInfo(List<OFaceInfo>? faces = null, OFaceInfo? mask = null)
        {
            Faces = faces ?? new List<OFaceInfo>();
            Mask = mask;
        }
        
        // To String

        public override string ToString()
        {
            string faces = string.Join(", ", Faces);
            string mask = Mask is not null ? $"{Mask}" : "[null]";
            
            return $"M{mask} F{faces}";
        }
    }
    
    public class OScenes : IOInstance
    {
        // Properties and Fields
        
        private OWindow _window;
        private OScene _main;
        private List<OScene> _scenes;
    
        IOInstance? IOInstance.Parent { get; set; } = null;
    
        public OWindow Window => _window;
        public OScene Main => _main;
        public string Icon => "󰉏";
        public string InstanceName => "OScenes";
        public string Name { get; set; } = "OScenes";
        public HashSet<string> Tags { get; } = new();
    
        // Events

        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;
    
        // Methods and Functions
        
        public OScenes(OWindow window, OScene main, string name = "OScenes")
        {
            _window = window;
            _main = main ?? ODebugger.Throw(new ArgumentNullException(nameof(main)));
            _scenes = new List<OScene>();
            Name = name;
            
            _scenes.Add(_main);
            
            _main.Parent = this;
        }
    
        public ReadOnlyCollection<OScene> GetScenes()
        {
            List<IOInstance> children = new List<IOInstance>();
            
            children.Add(_main);
            children.AddRange(_scenes);
    
            return new ReadOnlyCollection<IOInstance>(children);
        }

        public ReadOnlyCollection<IOInstance> GetChildren()
        {
            return GetScenes();
        }
    
        public ReadOnlyCollection<IOInstance> GetDescendants()
        {
            List<IOInstance> descendants = new List<IOInstance>();
            
            descendants.AddRange(GetChildren());
            
            foreach (IOInstance descendant in descendants)
                descendants.AddRange(descendant.GetDescendants());
            
            return new ReadOnlyCollection<IOInstance>(descendants);
        }
    
        public IOPrototype? Clone(bool cloneChildren, bool cloneDescendants)
        {
            return null;
        }
    
        public void Dispose()
        {
            foreach (IOInstance scene in _scenes)
                scene.Dispose();
    
            _scenes.Clear();
        }
    
        public ORenderInfo? Render()
        {
            // TODO!
    
            return null;
        }
    
        public void AddScene(OScene scene)
        {
            if (scene is null)
            {
                ODebugger.Throw(new ArgumentNullException(nameof(scene)));

                return;
            }
                
            if (!_scenes.Contains(scene))
                {
                    _scenes.Add(scene);

                    scene.Parent = this;

                    OnChildAdded?.Invoke(scene);
                }
        }
    
        public bool RemoveScene(OScene scene)
        {
            if (scene == _main)
                return false;
                
            if (_scenes.Remove(scene))
            {
                scene.Parent = null;
                
                OnChildRemoved?.Invoke(scene);
                
                return true;
            }
            
            return false;
        }
    
        public bool ContainsScene(string name)
        {
            return _scenes.Select((OScene scene) => scene.Name).Contains(name);
        }
    
        public bool ContainsScene(OScene scene)
        {
            return _scenes.Contains(scene);
        }
    
        public override string ToString()
        {
            return $"<OScenes Window=\"{_window.Name}\" Name=\"{Name}\" Main=\"{Main.Name}\">";
        }
    }
    
    public class OScene : IOInstance
    {
        // Properties and Fields
    
        private IOInstance? _parent;
    
        public string Icon => "󰈟";
        public string InstanceName => "OScene";
        public string Name { get; set; } = "OScene";
        public bool Main { get; private set; } = false;
    
        // Events
        
        public event OInstanceEvents.OnChildAdded? OnChildAdded;
        public event OInstanceEvents.OnChildRemoved? OnChildRemoved;

        // Methods and Functions

        public OScene(IOInstance? parent = null, string name = "OScene", bool main = false)
        {
            Name = name;
            Main = main;

            if (main && parent is null)
            {
                ODebugger.Throw(new ArgumentException("Parent must be provided if this scene is a main scene."));

                return;
            }

            if (main && !(parent is OScenes scenes))
            {
                ODebugger.Throw(new ArgumentException("Parent must be of type OScenes if this scene is a main scene."));

                return;
            }

            if (main && scenes.Main != this)
            {
                ODebugger.Throw(new ArgumentException("This scene is not the main scene of the provided OScenes instance."));

                return;
            }

            _parent = parent;
        }
    }
}