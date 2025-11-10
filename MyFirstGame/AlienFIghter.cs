using Microsoft.Xna.Framework; //alien lawan
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public class AlienFighter : Enemy
    {
        /// <summary>
        /// Constructor for the Alien Fighter.
        /// </summary>
        /// <param name="texture">The sprite texture for this enemy.</param>
        /// <param name="startPosition">Its starting (X, Y) coordinates.</param>
        public AlienFighter(Texture2D texture, Vector2 startPosition)
            : base("Alien Fighter", 100, 1.5f, texture, startPosition)
        {
            // "Alien Fighter", 100 HP, 1.5f speed
            // HP value from level description [cite: 109, 42]
        }

        /// <summary>
        /// Defines the AI and movement for the Alien Fighter.
        /// This implementation makes it move down while slowly
        /// tracking the player's X-axis.
        /// </summary>
        public override void Update(GameTime gameTime, Player player)
        {
            // Basic "chase" AI
            Vector2 direction = player.Position - this.Position;
            
            // Normalize the direction vector
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            // Move towards the player's direction
            // We only apply *part* of the horizontal pull
            // and the full vertical speed.
            float horizontalMovement = direction.X * (speed / 2); // Moves horizontally at half speed
            float verticalMovement = speed; // Always moves down at full speed
            
            Vector2 newPosition = new Vector2(
                Position.X + horizontalMovement,
                Position.Y + verticalMovement
            );
            
            Position = newPosition;

            // Attack logic (like spawning projectiles) could be added here
            // e.g., if (attackTimer > 0) { ... }
        }
    }
}