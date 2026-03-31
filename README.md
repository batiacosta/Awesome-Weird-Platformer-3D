# 🎮 Platformer Game

Un juego tipo **plataformas** donde el jugador puede moverse libremente usando **teclado** (AWSD o flechas) o **gamepad**.  
El proyecto incluye un sistema de **object pooling**, una **UI completa**, y un **Game Manager** que controla los estados principales del juego.

---

## 🕹️ Controles

- **Teclado**
  - `W` / `↑` → Saltar
  - `A` / `←` → Mover a la izquierda
  - `D` / `→` → Mover a la derecha
  - `S` / `↓` → Agacharse / bajar

- **Gamepad**
  - Stick izquierdo → Mover
  - `A` / `Cross` → Saltar
  - `B` / `Circle` → Agacharse / bajar

---

## Link del juego

- Web:  https://play.unity.com/en/games/bd4bf954-572e-4ce0-9fe8-ab355123e0c2/the-weird-platformer
- PC: https://github.com/batiacosta/Awesome-Weird-Platformer-3D/releases/tag/Demo

## ⚙️ Características

### 🔄 Object Pooling
- Sistema de reutilización de objetos para enemigos, proyectiles y coleccionables.
- Mejora el rendimiento evitando instanciaciones y destrucciones constantes.

### 🖥️ Interfaz de Usuario (UI)
- HUD con vida, puntuación y número de vidas.
- Menú de pausa y pantalla de fin de juego.
- Diseño adaptable a diferentes resoluciones.

### 🎯 Game Manager
- Control centralizado de los estados del juego:
  - **Menú Principal**
  - **Jugando**
  - **Pausado**
  - **Game Over**
- Maneja transiciones y comunica UI con la lógica de juego.

---

## 🛠️ Arquitectura

- **PlayerController**: gestiona entrada, movimiento y animaciones.
- **ObjectPool**: administra objetos reutilizables.
- **UIManager**: actualiza HUD y menús.
- **GameManager**: controla el flujo y estados del juego.

---

## 🚀 Cómo empezar

1. Clona el repositorio.
2. Abre el proyecto en Unity (recomendado 2021.3 o superior).
3. Ejecuta la escena `MainScene.unity`.
4. Juega con teclado o conecta un gamepad.

---

## 📌 Notas

- El diseño está pensado para ser extensible: se pueden añadir fácilmente nuevos estados, paneles de UI u objetos en el pool, y Scriptable Objects para variedad en los proyectiles.
- El sistema de entrada soporta tanto el **Input Manager clásico** como el **nuevo Input System** de Unity.

---


## 👥 Colaboradores

- Desarrollador: David Acosta
