# Onion Ring

This is a personal project made with Unity. It is a roguelike/rythm/pokemon game combining different ideas from other popular game. The game contains many iconic video game character that you have to defeat to become the ultimate video game hero. 
You will fight your way in music battles through epic music to defeat your enemies and make it to the end. Each enemy is unique and has unique skills to bring you down.

It is important to note that this game is still **IN PROGRESS**. 
The code partly needs some refactoring and elements can be improved. I always try to do things at the best of my knowledge and everyday I try to improve this project.
Most of the gameplay is done, but being only a programmer, not many texture/materials and art stuff are done.

![OnionRing](https://github.com/Vespucci3101/OnionRing/assets/71458452/5239e6e5-f339-40fc-938e-ada1d7a5d35f)

## Gameplay Features

### Slay the Spire style map

The final big part of this game is the map. It is working pretty much the same way as the one in Slay the Spire. 
You start from the bottom and make your way up to the boss of the stage. Their is specific paths already define for the player to take.
The player then choose the next point he wants to take to continue his way through the map.
The map has many different nodes, such as the enemy node, the boss node, the shop node, the treasure node, the elite node and the rest node.
Doing the map generation was actually quite a challenge, because it had to be procedural.
It is key for the map to be different every time to bring new strategy to the game.
The first step for the map is to generate points with the **Poisson Disk Sampling** which allows generation of points with a certain distance.
The second step is to generate triangles between these points and use the **Delaunay Triangulation** to make sure the triangles have the best representation possible.
Finally, the last step is to use a **AStar algorithm** to find the fastest path from the start to the end. 
We repeat this step three times to have more paths and each time we generate a path, we remove some points to make sure the paths aren't the same.

![Map](https://github.com/Vespucci3101/OnionRing/assets/71458452/5d0c8c07-eb30-4f77-a904-e32415118464)

![Map2](https://github.com/Vespucci3101/OnionRing/assets/71458452/d61d0ba3-91e1-400c-86a8-7b65d30b4454)

### Rythm gameplay

This part of the game is pretty simple, just like Guitar Hero or any rythm game, buttons appear at the button of the sreen and move up. 
Depending on the timing of the player input, the player score increases on three possible level, which are perfect, good and bad.
Realising this part was pretty simple, only a couple of scripts and UI elements for the buttons.

![Rythm](https://github.com/Vespucci3101/OnionRing/assets/71458452/07f19d9e-b091-41d0-af0e-7e701b220169)

### Pokemon style battle

This section is more complicated than the last one. Each song has a battle at the end, where you fight an enemy. 
The player has attacks and the enemy too. When selecting an attack, the player has to play a rythm part to "execute" the attack and depending on the results (Perfect, God or Bad) the attack will have greater or lower damage.
When the enemy is attacking, the player has to defend the same way and can block the enemy's damage.
Each attack has a different button pattern and depending on the strength of it, the attack will be harder or easier.
Finally, doing this part was also pretty simple, couple of scripts and a lot of UI elements.
The hardest part was to synchronise the song with the battle and making the battle phases in the right order with the right UI elements to show.

![Fight](https://github.com/Vespucci3101/OnionRing/assets/71458452/4a78a43f-f5e9-4f6a-a40a-d242696f4143)

### Useful Links

Here are some links that helped me.

- Poisson Disk Sampling : https://github.com/SebLague/Poisson-Disc-Sampling
- Delaunay Triangulation : https://www.habrador.com/tutorials/math/11-delaunay/
- A* Algorithm : https://www.youtube.com/watch?v=alU04hvz6L4
- Slay the Spire map generation : https://steamcommunity.com/sharedfiles/filedetails/?id=2830078257
