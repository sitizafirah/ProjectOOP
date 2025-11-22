using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyFirstGame
{
    public class Player
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        private Texture2D projectileTexture;
        private float projectileSpeed;
        private float shootCooldown;
        public Vector2 Size { get; private set; }

        private int hp;
        private float speed;
        private float fireRate;
        private int weaponLevel;
        public int WeaponLevel { get { return weaponLevel; } }

        public string Name { get; private set; }
        public int HP { get { return hp; } }

        private bool isInvincible;
        private float invincibilityTimer;
        private const float INVINCIBILITY_DURATION = 1.0f; // 1 second of invulnerability
        
        private float flashTimer;
        private const float FLASH_INTERVAL = 0.1f; // Flash every 0.1 seconds
        private bool isVisible; 

        public bool HasShield   {get ; private set; }   


        public Player(string name, Texture2D texture, Texture2D projectileTexture, Vector2 startPosition)
        {
            this.Name = name;
            this.Texture = texture;
            this.Position = startPosition;

            // Set default values from your design
            this.hp = 100;
            this.speed = 4f; // This will now mean "pixels per frame"
            this.fireRate = 0.5f;
            this.weaponLevel = 1;
            this.shootCooldown = 0f; // Player can shoot immediately

            this.projectileTexture = projectileTexture;
            this.projectileSpeed = 10.0f;
            this.Size = new Vector2(128, 128); // A 32x32 pixel square

            this.isInvincible = false;
            this.invincibilityTimer = 0f;
            this.flashTimer = 0f;
            this.isVisible = true;
            this.HasShield = false; // Player starts of with no shield
        }

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (shootCooldown > 0)
            {
                shootCooldown -= deltaTime ;
            }

            if (isInvincible)
            {
                invincibilityTimer -=  deltaTime ; 
                flashTimer -= deltaTime ;

                // Handles flashing effect 
                if (flashTimer <= 0)
                {
                    // Toggle visibility every flash interval
                    isVisible = !isVisible;
                    flashTimer = FLASH_INTERVAL ;
        
                }

                // End invincibility 
                if (invincibilityTimer <= 0)
                {
                    isInvincible = false ;
                    // Make player visible fully when invincibility wears off 
                    isVisible = true ; 
                }
            }

            KeyboardState kState = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
            {
                direction.X = -1;
            }
            if (kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
            {
                direction.X = 1;
            }

            Position += direction * speed;
            
            if (kState.IsKeyDown(Keys.Space) && shootCooldown <= 0)
            {
                shootCooldown = fireRate; 
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isVisible) {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(
                this.Texture,
                // this.Position,
                destinationRectangle,
                Color.White
            );
            }

             if (HasShield && isVisible) {
            Rectangle destinationRectangle = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            spriteBatch.Draw(
                this.Texture,
                destinationRectangle,
                Color.Blue * 0.5f // 50% transparent blue 
            );
            }
        }

        public Projectile Shoot()
        {
            Vector2 spawnPosition = new Vector2(
                this.Position.X + (this.Texture.Width / 2) - (this.projectileTexture.Width / 2),
                this.Position.Y
            );

            Vector2 direction = new Vector2(0, -1);

            return new Projectile(
                this.projectileTexture,
                spawnPosition,
                10 * weaponLevel,
                this.Name,
                direction,                  
                this.projectileSpeed
            );
        }

        public void TakeDamage(int damage)
        {
            if (HasShield)
            {
                HasShield = false;
                return;
            }

            //Check for invulnerability, proceed if no shield and not already invincible
            if (isInvincible)
            {
                return ;
            }
            this.hp -= damage;

            isInvincible = true;
            invincibilityTimer = INVINCIBILITY_DURATION ;
            flashTimer = FLASH_INTERVAL ;
            if (this.hp <= 0)
            {
                // Set a flag, e.g., IsAlive = false
            }
        }

        public void AddHealth(int value)
        {
            this.hp += value;
        }

        public void UpgradeWeapon(int value)
        {
            this.weaponLevel += value;
        }
    }
}