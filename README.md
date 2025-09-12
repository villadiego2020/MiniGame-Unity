# MiniGame System Documentation

## System Workflow
- The system is designed with a **modular MiniGame architecture** using `MiniGameManager` and `MiniGameDescriptor`.  
- Each mini-game is a **Prefab** with a script that implements `MiniGame` (e.g., `Initialize`, `StartGame`, `StopGame`).  
- The `MiniGame` class inherits from the `IMiniGame` interface.  
- Every mini-game has a `MiniGameDescriptor` (`ScriptableObject`) that defines:  
  - **Id**  
  - **Name**  
  - **Prefab**  
- At runtime, `MiniGameManager` loads all descriptors from `Resources/Games/`, then generates a menu for selection.  
- When a game is chosen, it automatically **instantiates the prefab**, **initializes**, **runs**, and **cleans up**.  
- The structure is **lightweight, flexible**, and allows adding new mini-games **without modifying the manager**.  

---

## How to Create a New MiniGame
1. Write a new script that implements `MiniGame` and add your game logic.  
   - Override `StartGame()` for initialization.  
   - Call `Finish(bool success)` when the game ends (`success = true` for Win, `false` for Loss).  

2. Create a **Prefab** with this script as the root component.  

3. Create a **MiniGameDescriptor**:  
   - **Right Click → Create → MiniGame → Descriptor**  
   - Fill in:  
     - **IsEnabled** → Whether the game appears in the menu.  
     - **Id** → Unique identifier.  
     - **DisplayName** → User-friendly name.  
     - Drag and drop the Prefab.  
   - Save the descriptor file under `Resources/Games/`.  

---

## Example MiniGames Implemented inside project
- **DodgeTheCubesMiniGame**  
  Dodge falling obstacles. The player has HP; losing all HP = Defeat. Survive until the timer ends = Win.  

- **TapTheButtonMiniGame**  
  Tap a button rapidly. Reaching the target tap count before time runs out = Win, otherwise = Lose.  

- **ReactTimeSetMiniGame**  
  Reaction challenge: stop the timer within a defined range. Inside range = Win, outside range = Lose.  
  *(Inspired by “stop-the-timer” mini-games at restaurants like MK, Shabushi where players can win prizes.)*  

---

With this setup, the system will **automatically detect new mini-games** placed in the `Resources/Games/` folder.
