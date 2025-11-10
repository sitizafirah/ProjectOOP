using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyFirstGame
{
    // The enum from your design
    public enum PowerUpType
    {
        Health,
        Shield,
        WeaponUpgrade
    }
// mia buat 
    public class PowerUp
    {
        // Visuals
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }

        // Attributes from your design
        private PowerUpType type;
        private int value;

        public bool IsActive { get; set; }
        
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        // Constructor
        public PowerUp(Texture2D texture, Vector2 position, PowerUpType type, int value)
        {
            this.Texture = texture;
            this.Position = position;
            this.type = type;
            this.value = value;
            this.IsActive = true;
        }

        /// <summary>
        /// Applies the power-up effect to the player.
        /// </summary>
        public void Apply(Player player) // 
        {
            switch (type)
            {
                case PowerUpType.Health:
                    player.AddHealth(value);
                    break;
                case PowerUpType.WeaponUpgrade:
                    player.UpgradeWeapon(value);
                    break;
                case PowerUpType.Shield:
                    // player.ActivateShield(value);
                    break;
            }
            this.IsActive = false; // Deactivate after collection
        }

        /// <summary>
        /// Draws the power-up to the screen.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                this.Texture,
                this.Position,
                Color.White
            );
        }
    }
}