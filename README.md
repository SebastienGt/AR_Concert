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

The registration of the environment is done via markers (stored in a custom Vuforia database). For this application, seven diferent markers have been designed:

- Three markers for the musicians
- Three markers for their instruments
- One marker for the music shop

The world reference system is defined by the device.

# Available interactions

- The player can tap on a musician and they will start playing if their instrument is within range 
	- A range check is carried out on tap
	- An animation switch is made on tap, if the check is succesful
	- The markers are moved around in the real world to to move players and instruments
	- A check also makes sure the timestamps of the multitracks match
- Every instrument has a 10% probability of breaking every second 
	- This is carried out through a check in the `FixedUpdate` method
- When an instrument breaks, the musician stops playing and glows red
	- This is carried out by adding a shader
- A broken instrument has to be taken to the shop to be fixed. It can be taken away by tapping on the musician
- When an instrument is taken away, it spawns on the marker again and the player has to:
	1. take it to the instrument shop
	2. tap on the shop, which immediately fixes the instrument
	3. take it back to the musician 
- The difference between a functioning and broken instrument is a shader and that you can't play broken instruments
	- A check that the instrument is functioning is carried out before every click

# Sources
## 3D Models and Animations

- Playing people:
- Instruments: 
- Music shop:

## Music
 

