# 🎮 Game Jam 2026 - Developer Guide

Welcome to the repo! This document outlines where files go and how we can work together efficiently without breaking the project.

## 📂 Folder Structure
**Everything we make goes inside `Assets/_Game/`.** Do not modify `Plugins` or `Packages` unless we are adding a new tool.

```text
Assets/
├── _Game/                  <-- 🟢 ALL OUR CUSTOM WORK
│   ├── Animations/         <-- Controllers & Clips
│   ├── Art/                <-- Sprites, Textures, Materials, Models
│   ├── Audio/              <-- SFX and Music
│   ├── Prefabs/            <-- ⭐️ EDIT THESE, NOT SCENES!
│   │   ├── Characters/     (Player, Enemies)
│   │   ├── Environment/    (Platforms, Props)
│   │   └── UI/
│   └── Scripts/            <-- C# Code
│       ├── Player/
│       ├── Enemy/
│       └── Managers/       (GameManager, Audio, UI)
│
├── Plugins/                <-- 🔴 3rd Party Assets (Do not edit scripts here)
├── Scenes/                 <-- Game Levels
│   ├── _Sandbox/           <-- Create your own test scene here (e.g., "Dev_Test")
│   └── Levels/             <-- Final Game Scenes
└── Settings/               <-- Unity Project Settings (Input, URP, etc.)
```

# Tips and tricks

## 1. The "Prefab" Rule
* Never build the player or enemy logic directly in the Scene.
* Make them Prefabs and store them in _Game/Prefabs/.
* If you need to change the Player's speed or sprite, open the Prefab, edit it, and save. This updates it everywhere automatically.


## 2. The "Sandbox" Rule (Avoid Merge Conflicts)

* Avoid working in the Main Scene simultaneously. Unity cannot merge scene files well.
* Create a generic scene for yourself in Assets/Scenes/_Sandbox/ (e.g., Test_Movement.unity).
* Test your mechanics there. When it works, turn it into a Prefab and drop it into the Main Scene.

