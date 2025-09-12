# MiniGame System Documentation
---

### Unity Version
Unity Version 6.2 (6000.2.2f1)

---

## English Version

### System Workflow
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

### How to Create a New MiniGame
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

### Example MiniGames Implemented
- **DodgeTheCubesMiniGame**  
  Dodge falling obstacles. The player has HP; losing all HP = Defeat. Survive until the timer ends = Win.  

- **TapTheButtonMiniGame**  
  Tap a button rapidly. Reaching the target tap count before time runs out = Win, otherwise = Lose.  

- **ReactTimeSetMiniGame**  
  Reaction challenge: stop the timer within a defined range. Inside range = Win, outside range = Lose.  
  *(Inspired by “stop-the-timer” mini-games at restaurants like MK, Shabushi where players can win prizes.)*  

---

With this setup, the system will **automatically detect new mini-games** placed in the `Resources/Games/` folder.

---

## ภาษาไทย (Thai Version)

### วิธีการทำงานของระบบ
- ระบบถูกออกแบบให้เป็น **สถาปัตยกรรม MiniGame แบบโมดูลาร์** โดยใช้ `MiniGameManager` และ `MiniGameDescriptor`  
- แต่ละมินิเกมจะเป็น **Prefab** ที่มีสคริปต์ implement `MiniGame` (เช่น `Initialize`, `StartGame`, `StopGame`)  
- ภายในคลาส MiniGame จะสืบทอดจาก Interface ชื่อ `IMiniGame`  
- มินิเกมทุกตัวมี `MiniGameDescriptor` (`ScriptableObject`) ที่กำหนด:  
  - **Id**  
  - **Name**  
  - **Prefab**  
- ตอนรันจริง `MiniGameManager` จะโหลด descriptor ทั้งหมดจาก `Resources/Games/` แล้วสร้างเมนูให้เลือกเล่น  
- เมื่อเลือกเกม ระบบจะทำการ **instantiate prefab**, **initialize**, **run**, และ **cleanup** ให้อัตโนมัติ  
- โครงสร้างนี้ **เบา, ยืดหยุ่น** และสามารถเพิ่มมินิเกมใหม่ได้ง่าย **โดยไม่ต้องแก้ manager**  

---

### วิธีสร้าง MiniGame ใหม่
1. เขียนสคริปต์ใหม่ที่ implement `MiniGame` และใส่ logic ของเกม  
   - Override `StartGame()` เพื่อกำหนดค่าเริ่มต้นของเกม  
   - เมื่อเข้าเงื่อนไขเกมจบ ให้เรียก `Finish(bool success)` (`success = true` = ชนะ, `false` = แพ้)  

2. ทำ **Prefab** ที่มีสคริปต์นี้เป็น root  

3. สร้าง **MiniGameDescriptor**:  
   - **คลิกขวา → Create → MiniGame → Descriptor**  
   - กรอกข้อมูล:  
     - **IsEnabled** → เปิด/ปิดการแสดงผลเกมในเมนู  
     - **Id** → รหัสที่ไม่ซ้ำ  
     - **DisplayName** → ชื่อที่อ่านง่าย  
     - ลาก Prefab ลงไป  
   - เซฟ descriptor ไว้ในโฟลเดอร์ `Resources/Games/`  

---

### ตัวอย่าง MiniGame ที่มีอยู่แล้ว
- **DodgeTheCubesMiniGame**  
  หลบสิ่งกีดขวางที่ตกลงมา ผู้เล่นมีค่า HP ถ้า HP หมด = แพ้ ถ้าอยู่ครบเวลาที่กำหนด = ชนะ  

- **TapTheButtonMiniGame**  
  กดปุ่ม Tap Tap อย่างรวดเร็ว ถ้าทำจำนวนได้เกินเป้าก่อนหมดเวลา = ชนะ ถ้าหมดเวลาก่อน = แพ้  

- **ReactTimeSetMiniGame**  
  เกมทดสอบการตอบสนอง: ต้องกดหยุดเวลาให้อยู่ในช่วงที่กำหนด ถ้าอยู่ในช่วง = ชนะ ถ้าเกินช่วง = แพ้  
  *(คล้ายเกมกดหยุดเวลาที่ร้าน MK หรือ Shabushi ที่ให้ลูกค้าเล่นชิงรางวัล)*  

---

ระบบจะ **เจอมินิเกมใหม่ให้อัตโนมัติ** ถ้ามีการเพิ่ม descriptor ใน `Resources/Games/`

