using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace Bombs
{
    /// <summary>
    /// Tipo principal del juego
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 textoPosicion;
        SpriteFont spriteFont;

        //Jugador
        Jugador jugador;
        TouchCollection touchCollection;
        Vector2 messagePos = Vector2.Zero;
        

        // Enemies

        Texture2D enemyTexture;
        List<Enemy> enemies;
        TimeSpan enemySpawnTime;
        TimeSpan previousSpawnTime;
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // La velocidad de fotogramas predeterminada para Windows Phone es de 30 fps.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Amplía la duración de la batería con bloqueo.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        /// <summary>
        /// Permite que el juego realice la inicialización que necesite para empezar a ejecutarse.
        /// Aquí es donde puede solicitar cualquier servicio que se requiera y cargar todo tipo de contenido
        /// no relacionado con los gráficos. Si se llama a base.Initialize, todos los componentes se enumerarán
        /// e inicializarán.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: agregue aquí su lógica de inicialización
            TouchPanel.EnabledGestures = GestureType.Tap | GestureType.DoubleTap |
                        GestureType.Hold | GestureType.HorizontalDrag |
                        GestureType.VerticalDrag | GestureType.FreeDrag |
                        GestureType.DragComplete | GestureType.Pinch |
                        GestureType.PinchComplete | GestureType.Flick;
            jugador = new Jugador();

            // Initialize the enemies list
            enemies = new List<Enemy>();

            // Set the time keepers to zero
            previousSpawnTime = TimeSpan.Zero;

            // Used to determine how fast enemy respawns
            enemySpawnTime = TimeSpan.FromSeconds(1.0f);

            // Initialize our random number generator
            random = new Random();
            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent se llama una vez por juego y permite cargar
        /// todo el contenido.
        /// </summary>
        protected override void LoadContent()
        {
            // Crea un SpriteBatch nuevo para dibujar texturas.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            jugador.PlayerTexture = Content.Load<Texture2D>("Imagen/rana");
            // Load the player resources 
            Vector2 playerPosition = new Vector2(
                GraphicsDevice.Viewport.Width  - jugador.Width,
                GraphicsDevice.Viewport.Height/2 - jugador.Height / 2);
            jugador.Initialize(Content.Load<Texture2D>("Imagen/rana"), playerPosition);
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            enemyTexture = Content.Load<Texture2D>("Imagen/animacionmina");
            
            // TODO: use this.Content para cargar aquí el contenido del juego
        }

        private void AddEnemy()
        {
            // Create the animation object
            Animation enemyAnimation = new Animation();

            // Initialize the animation with the correct animation information
            enemyAnimation.Initialize(enemyTexture, Vector2.Zero, 47, 61, 8, 30, Color.White, 1f, true);

            // Randomly generate the position of the enemy
            Vector2 position = new Vector2(GraphicsDevice.Viewport.X + enemyTexture.Width / 2, random.Next(100, GraphicsDevice.Viewport.Y - 100));

            // Create an enemy
            Enemy enemy = new Enemy();

            // Initialize the enemy
            enemy.Initialize(enemyAnimation, position);

            // Add the enemy to the active enemies list
            enemies.Add(enemy);
        }

        private void UpdateEnemies(GameTime gameTime)
{
    // Spawn a new enemy enemy every 1.5 seconds
    if (gameTime.TotalGameTime - previousSpawnTime > enemySpawnTime) 
    {
        previousSpawnTime = gameTime.TotalGameTime;

        // Add an Enemy
        AddEnemy();
    }

    // Update the Enemies
    for (int i = enemies.Count- 1; i >= 0; i--) 
    {
        enemies[i].Update(gameTime);

        if (enemies[i].Active == false)
        {
            enemies.RemoveAt(i);
        } 
    }
}
        /// <summary>
        /// UnloadContent se llama una vez por juego y permite descargar
        /// todo el contenido.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: descargue aquí todo el contenido que no pertenezca a ContentManager
        }

        /// <summary>
        /// Permite al juego ejecutar lógica para, por ejemplo, actualizar el mundo,
        /// buscar colisiones, recopilar entradas y reproducir audio.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Update(GameTime gameTime)
        {
                    

            // TODO: agregue aquí su lógica de actualización
            touchCollection = TouchPanel.GetState();
            if (touchCollection.Count >= 1)
            {
                textoPosicion.X = touchCollection[0].Position.X ;
                textoPosicion.Y = touchCollection[0].Position.Y ;
            }
            // Update the enemies
            UpdateEnemies(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Se llama cuando el juego debe realizar dibujos por sí mismo.
        /// </summary>
        /// <param name="gameTime">Proporciona una instantánea de los valores de tiempo.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            jugador.Draw(spriteBatch);
            foreach (TouchLocation touch in touchCollection)
            {
                spriteBatch.DrawString(spriteFont,
                    "ID: " + touch.Id.ToString() + " (" + (int)touch.Position.X +
                    "," + (int)touch.Position.Y + ")", textoPosicion, Color.White);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            
            // TODO: agregue aquí el código de dibujo

            base.Draw(gameTime);
        }
    }
}
