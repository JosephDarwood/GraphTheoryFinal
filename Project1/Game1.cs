using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.My_Graph_Notebook;
using Project1.My_Graph_Notebook.Interface;
using System.Threading;
using System;

namespace Project1
{
    public class Game1 : Game, ButtonFunctions, IGraphQueries
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Canvas _canvas;
        private InputPanel _inputPanel;
        private OutputPanel _outputPanel;
        private MousePack myMouse;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            myMouse = new MousePack();
            myMouse.currentMouse = Mouse.GetState();
            myMouse.previousMouse = myMouse.currentMouse;
        }

        protected override void Initialize()
        {     

            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = Globals.windowWidth;
            _graphics.PreferredBackBufferHeight = Globals.windowHeight;
            _graphics.ApplyChanges();
            base.Initialize();
            this._canvas = new Canvas(this.Content, this._spriteBatch, this._graphics);
            this._inputPanel = new InputPanel(this.Content, this);
            this._outputPanel = new OutputPanel(this.Content, this);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            myMouse.currentMouse = Mouse.GetState();
            // TODO: Add your update logic here
            _canvas.Update(myMouse);
            _inputPanel.Update(myMouse);
            _outputPanel.Update(myMouse);
            base.Update(gameTime);
            myMouse.previousMouse = myMouse.currentMouse;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _canvas.Draw();
            _inputPanel.Draw(_spriteBatch);
            _outputPanel.Draw(_spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void AddVertex()
        {
            _canvas.AddNode();
        }
        public void AddEdge() 
        {
            _canvas.AddEdge();
        }
        public void RemoveVertex()
        {
            _canvas.RemoveVertex();
        }
        public void RemoveEdge()
        {
            _canvas.RemoveEdge();
        }
        public void RenameVertex(string s1, string s2)
        {

        }
        public string VertexEdgeInfo()
        {
            return _canvas.VertexEdgeInfo();
        }
        public string VertexDegreeInfo()
        {
            return _canvas.VertexDegreeInfo();
        }
        public string BridgesInfo()
        {
            return _canvas.BridgesInfo();
        }
        public string BipartiteInfo()
        {
            return _canvas.BipartiteInfo();
        }
    }
}