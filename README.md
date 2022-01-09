---
title: Augmented Reality Concert
author: Sébastien Friedberg and Kevin Michael Frick
date: December 2021
---

# Introduction

This application is an augmented reality environment which simulates a concert, developed for the Virtual and Augmented Reality course held for the Master in Innovation and Research in Informatics by Prof. Pelechano, Prof. Andujar and Prof. Fairen at Universitat Politècnica de Catalunya in the academic year 2021/2022.

The application was developed using Unity 2020.3 and Vuforia.
  
# Demo
[![demo of our application](https://img.youtube.com/vi//0.jpg)](https://www.youtube.com/watch?v=)

# Environment Registration

The registration of the environment is done via markers (stored in a custom Vuforia database). For this application, three diferent markers have been designed:

- One marker for the musician
- One marker for its instrument
- One marker for the music repair person

The world reference system is defined by the device.

# Available interactions

- The player can bring an instrument to the musician using *two different interaction methods*, either by dragging or moving the marker, and he will start playing
- A bar indicates the health of the instrument, which decays over time. 
- When the health of the instrument drops to zero, the musician stops playing and the instrument has to be repaired.
- A broken instrument glows red.
- The instrument can be repaired by bringing it to the repair person, that will approach the instrument when it is close enough and repair it.
- The repair person will then move back to her marker and the instrument can be brought back to the musician.
- The UI can also interact with the objects in the real world: by dragging a flag on he musician, it is possible to change the song he will play.

# Sources

## 3D Models and Animations

- Playing people: https://ko-fi.com/s/642143592a
- Instruments: https://www.turbosquid.com/3d-models/lemmy-kilmister-s-bass-guitar-obj-free/1059195

## Music
 

