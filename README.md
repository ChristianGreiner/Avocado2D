# Avocado2D

Avocado2D is a type of gameengine for [MonoGame](http://www.monogame.net/).

This project is work in progress.

Roadmap:
- [x] Entity-Component-System (WIP)
- [x] SceneManager
- [x] Collider (WIP)
- [x] Collision-Detection  (WIP)
- [x] InputManager (WIP)
- [ ] SoundHandling
- [ ] Network
- [ ] TileMap
- [ ] Simple UI-System

## Basics

**Create a Scene**
```csharp
// AvocadoGame.cs
var myScene = new Scene("MyScene", this);
SceneManager.AddScene(myScene);

// draws the scene
SceneManager.SetActiveScene("MyScene");
```
You can also inherit from the scene-class.
```csharp

public class MainMenu : Scene
{
  // do your stuff in the scene.
}

```


**Create a GameObject**

```csharp
var player = new GameObject("Player");

// add some components to the gameobject 
player.AddComponent<PlayerController>();
var healthComponent = player.AddComponent<HealthComponent>();
healthComponent.Health = 100;

// removes a component from the gameobject
player.RemoveComponent<HealthComponent>();

// assign the gameobject to the scene
myScene.AddGameObject(player);

```

**Create a custom component**

A simple data-holding component (no logic)
```csharp
public class MyFancyComponent : Component
{
    public int FancyValue { get; set; }
}

```

A behavior (logic) component:
```csharp
public class MyFancyComponent : Behavior
{
    public int FancyValue { get; set; }
    
    // modify an other component
    public override OnInitialize()
    {
        var otherComponent = GameObject.GetComponent<OtherComponent>();
        otherComponent.MovementSpeed = 20f;
    }
    
    public override void Update(GameTime gameTime)
    {
        FancyValue++;
    }
}

```

A drawable component (e.g. for gui stuff):
```csharp
public class MyFancyButtonComponent : Drawable
{
    public override OnInitialize()
    {
        // ... 
    }
    
    public override void Draw(SpriteBatch spriteBatch)
    {
        // draw you stuff
    }
}

```

Which components are already implemented?
- [x] Sprite
- [x] Collider (WIP)
- [ ] AudioSource
- [ ] GUI-Stuff (Button, Canvas, etc)
- [ ] AnimatedSprite
- [ ] ...
