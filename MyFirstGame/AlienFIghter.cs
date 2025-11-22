using Microsoft.Xna.Framework; //alien lawan
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    public class AlienFighter : Enemy
    {
        private GameManager gameManager;
        private float shootTimer = 0f;
        private float shootCooldown = 2f;
        /// <summary>
        /// Constructor for the Alien Fighter.mm
        /// </summary>
        /// <param name="texture">The sprite texture for this enemy.</param>
        /// <param name="startPosition">Its starting (X, Y) coordinates.</param>
        public AlienFighter(Texture2D texture, Vector2 startPosition, GameManager gm)
            : base("Alien Fighter", 100, 1.5f, texture, startPosition)
        {
            this.gameManager = gm;
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
            shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (shootTimer >= shootCooldown)
            {
                Shoot();
                shootTimer = 0f;
            }

            // Attack logic (like spawning projectiles) could be added here
            // e.g., if (attackTimer > 0) { ... } meowmeow
        }
        
        private void Shoot()
        {
            Vector2 bulletStartPos = new Vector2(Position.X + (Texture.Width / 2) - 5, Position.Y + Texture.Height);
            Vector2 bulletDirection = new Vector2(0, 5f);
            Projectile bullet = new Projectile(texture: gameManager.EnemyProjectileTexture, startPosition: bulletStartPos, damage: 10, owner: "Enemy", direction: bulletDirection, speed: 5f);
            gameManager.EnemyProjectile.Add(bullet);
        }
    }
}