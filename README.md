# Quiz Game Prototype – Project Documentation

## 📌 Overview

This project is a prototype of a quiz game built with a modular and scalable architecture. The focus was on reusability, flexibility, and clean code structure suitable for further extension.

---

## 🚀 Project Initialization

The implementation began with the setup of two core systems that I had previously developed and refined over time:

### ✅ Reused Systems

- **Object Pooling System**
  - Includes:
    - `PoolObjectManager` – the central manager for pooling logic
    - `ObjectPool` – stores and reuses pooled objects
- **GUI Management System**
  - Main component: `UIManager`
  - Contains two abstract base lists:
    - `BasicPopup`
    - `BasicScreen`
  - These allow for easy management of all UI screens and popups.

---

## 🧩 Game Startup

To launch the game, I implemented the `EntryPoint` class, which acts as the **main controller** of the application. Its responsibilities:

- Initializes all systems:
  - `PoolObjectManager`
  - `UIManager`
  - `GamePlayManager`
- Starts the overall game flow

### 🧠 `GamePlayManager`

The `GamePlayManager` handles all game logic and data. It includes:

- A **list of categories**, each containing its own questions and answer options
- Dynamic UI adaptation based on category structure:
  - Automatically adds more buttons when needed
  - Adjusts question count depending on category content
  - Adapts the number of answers per question independently for each category

This ensures the game is **data-driven**, requiring no hardcoding or manual UI changes when new content is added.

---

## 🗂️ Category & Question System

Based on experience with similar quiz games, I designed a system to easily add:

- New **categories**
- New **questions** within each category

### 🔹 `CategoryConfig` (ScriptableObject)

Holds category data:
- Category type
- Name
- List of questions

> Although the current example includes only one category, the system is built for unlimited expansion.

### 🧠 Flexible Answers

- Answer buttons are generated dynamically
- The number of answer options is unlimited
- Correctness is determined by comparing the **button index** to the **correct answer index**

This approach was chosen over string matching to allow for future quiz types (e.g., image- or emoji-based questions).

---

## 🎉 Confetti System (Success Feedback)

One of the challenges was implementing a **confetti effect** inside a `Canvas`.

### ❌ Initial Attempt
Using Unity's `Particle System` — not rendered in Canvas.

### ✅ Final Solution
- Used **DOTween** for programmatic animation
- Avoided frame-based animation for better control
- Fully customizable via the `ConfettiEffectUI` class

### 🔧 Configurable Parameters
- Cone angle
- Number of confetti particles
- Flight distance
- Lifetime (disappearance time)

---

## 🧠 Summary

This prototype emphasizes:
- Modularity
- Reusability
- Scalability
- Clean architecture

All systems were built with future expansion in mind, including support for more categories, questions, answer types, and feedback animations.
