# SNKEZ-Mob-Spawning

Creating a mob spawning mechanic in unity utilizing object pooling methods. Inspiration from the game SNKEZ.

## Enemy object

- Deciding to create 4 different types of enemy, using shapes and colours to represent different enemy/mob.
- Each enemy will have different AI movements and its determined by the shape/colour of the enemy.
- The shape/colour will be determined at the beginning when the enemy object is initialized and from that it can detect what movement pattern the object will have.
- Some enemy can shoot. This is done by creating an Bullet class which if it collides with player or the wall, it will destroy itself.

## Wave Spawner

- Allows the user to add or remove waves into the game without coding. Making it easy for level desginers to manage the wave spawning mehcnaics.
- Can deternube the number of enemies per wave.
- Can determine duration between each wave when all the enemy is defeated.
