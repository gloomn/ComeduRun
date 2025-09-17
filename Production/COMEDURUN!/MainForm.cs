using COMEDURUN_.Timers;
using COMEDURUN_.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace COMEDURUN_
{
    public partial class MainForm : Form
    {
        // Definition
        private PictureBox COM;
        private List<PictureBox> obstacles = new List<PictureBox>();
        private List<Image> obstacleImages = new List<Image>();
        private Label scoreLabel;
        private Label gameOverLabel;
        private Label gameOverLabelScore;
        private GameTimer gameTimer;
        private Button restartButton;
        private Button goToStartButton;

        private bool jumping = false;
        private int jumpSpeed = 0;
        private int jumpForce = 30;
        private int gravity = 3;
        private int score = 0;
        private int obstacleSpeed = 5;
        private int groundLevel = 765;
        private int maxSpeed = 60;
        DateTime startTime;

        private WebSocketServer wsServer;
        private Random random = new Random();

        // Initialize Component
        public MainForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            startPanel.Show();
            gamePanel.Hide();

            this.KeyPreview = true;
            this.KeyDown += KeyIsDown;
            

            StartWebSocketServer();
        }

        // Start web socket server
        private void StartWebSocketServer()
        {
            wsServer = new WebSocketServer(8080);
            wsServer.AddWebSocketService<PoseBehavior>("/pose", () =>
            {
                var behavior = new PoseBehavior();
                behavior.OnPoseReceived = HandlePoseMessage;
                return behavior;
            });
            wsServer.Start();
            Console.WriteLine("WebSocket server started at ws://localhost:8080/pose/");
        }

        // Get current pose from teachable machine web socket
        private void HandlePoseMessage(string poseName)
        {
            Console.WriteLine("WebSocket에서 메시지 수신: " + poseName);

            if (gameTimer != null)
            {

                if (poseName == "jump" && !jumping && COM.Top >= groundLevel)
                {
                    // UI 스레드에서 점프 실행
                    this.Invoke(new MethodInvoker(delegate
                    {
                        jumping = true;
                        jumpForce = 12;
                        jumpSpeed = -30;
                    }));
                }
            }
        }

        // Set up game
        private void setupGame()
        {
            COM = new PictureBox
            {
                Image = Image.FromFile("Assets/sampleCOM.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Height = 100,
                Width = (int)(100 * ((float)Image.FromFile("Assets/sampleCOM.png").Width /
                                     Image.FromFile("Assets/sampleCOM.png").Height)),
                Left = 100,
                BackColor = Color.Transparent,
                Top = groundLevel
            };

            gameBackground.Controls.Add(COM);

            obstacleImages.Add(Image.FromFile("Assets/obstacleA.png"));
            obstacleImages.Add(Image.FromFile("Assets/obstacleB.png"));
            obstacleImages.Add(Image.FromFile("Assets/obstacleC.png"));

            spawnObstacles();

            scoreLabel = new Label()
            {
                Text = "Score: 0",
                Font = new Font("Showcard Gothic", 20, FontStyle.Regular),
                AutoSize = true,
                BackColor = Color.Transparent,
                Top = 10,
                Left = 1650
            };
            gameBackground.Controls.Add(scoreLabel);
            scoreLabel.BringToFront();

            gameOverLabel = new Label()
            {
                Text = "Game Over",
                Font = new Font("Showcard Gothic", 40, FontStyle.Regular),
                AutoSize = true,
                BackColor = Color.Transparent,
                Top = 600,
                Left = 780
            };

            gameOverLabelScore = new Label()
            {
                Text = score.ToString(),
                Font = new Font("Showcard Gothic", 40, FontStyle.Regular),
                AutoSize = true,
                BackColor = Color.Transparent,
                Top = 700,
                Left = 780
            };

            restartButton = new Button()
            {
                Text = "Restart Game",
                Top = 800,
                Left = 780
            };

            goToStartButton = new Button()
            {
                Text = "Start Menu",
                Top = 800,
                Left = 1000
            };

            gameBackground.Controls.Add(gameOverLabel);
            gameBackground.Controls.Add(gameOverLabelScore);
            gameBackground.Controls.Add(restartButton);
            gameBackground.Controls.Add(goToStartButton);

            gameOverLabel.Hide();
            gameOverLabelScore.Hide();
            restartButton.Hide();
            goToStartButton.Hide();

            gameTimer = new GameTimer(20, gameLoop);
            gameTimer.Tick += gameTimerEvent;
            gameTimer.Start();

            restartButton.Click += restartButton_Click;
            goToStartButton.Click += goToStartButton_Click;

            startTime = DateTime.Now;
            obstacleSpeed = 5;
        }

        // Spawn obstacles function
        private void spawnObstacles()
        {
            var obs = new PictureBox
            {
                Size = new Size(100, 100),
                Top = groundLevel,
                Left = 1920,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = obstacleImages[random.Next(obstacleImages.Count)],
                BackColor = Color.Transparent
            };
            obstacles.Add(obs);
            gameBackground.Controls.Add(obs);
        }

        // Game loop event
        private void gameLoop(object sender, EventArgs e)
        {
            if (jumping)
            {
                COM.Top += jumpSpeed;
                jumpSpeed += gravity;
                jumpForce--;
                if (jumpForce <= 0) jumping = false;
            }
            else
            {
                if (COM.Top < groundLevel)
                {
                    jumpSpeed += gravity;
                    COM.Top += jumpSpeed;
                    if (COM.Top >= groundLevel)
                    {
                        COM.Top = groundLevel;
                        jumpSpeed = 0;
                        jumpForce = 12;
                    }
                }
            }

            COM.Top += jumpSpeed;
            if (COM.Top >= groundLevel)
            {
                COM.Top = groundLevel;
                jumpSpeed = 0;
                jumping = false;
                jumpForce = 12;
            }

            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                var obs = obstacles[i];
                obs.Left -= obstacleSpeed;

                if (obs.Left < -obs.Width)
                {
                    gameBackground.Controls.Remove(obs);
                    obstacles.RemoveAt(i);

                    score++;
                    scoreLabel.Text = $"Score: {score}";


                    spawnObstacles();
                }

                //When colliding = true
                if (CollisionHandler.isColliding(COM, obs))
                {
                    gameTimer.Stop();
                    gameTimer.Dispose();
                    gameTimer = null;

                    gameOverLabelScore.Show();
                    gameOverLabel.Show();
                    restartButton.Show();
                    goToStartButton.Show();

                    gameOverLabel.BringToFront();
                    gameOverLabelScore.BringToFront();
                    restartButton.BringToFront();
                    goToStartButton.BringToFront();
                    return;
                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            gamePanel.Show();
            startPanel.Hide();
            if (gameTimer == null)
                setupGame();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            // 기존 게임 요소 정리
            foreach (var obs in obstacles)
                gameBackground.Controls.Remove(obs);
            obstacles.Clear();

            gameBackground.Controls.Remove(COM);

            // Score reset
            score = 0;

            // Hide game over elements
            gameOverLabel.Hide();
            gameOverLabelScore.Hide();
            restartButton.Hide();
            goToStartButton.Hide();

            // New game start
            setupGame();
        }

        private void goToStartButton_Click(object sender, EventArgs e)
        {
            // Hide game over elements
            gameOverLabel.Hide();
            gameOverLabelScore.Hide();
            restartButton.Hide();
            goToStartButton.Hide();

            // Panel conversion
            startPanel.Show();
            gamePanel.Hide();

            // Game object reset
            foreach (var obs in obstacles)
                gameBackground.Controls.Remove(obs);
            obstacles.Clear();
            if (COM != null) gameBackground.Controls.Remove(COM);

            gameTimer?.Stop();
            gameTimer?.Dispose();
            gameTimer = null;
        }


        //When pressed space key
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (InputHandler.isJumpKeys(e.KeyCode) && !jumping && COM.Top >= groundLevel)
            {
                jumping = true;
                jumpForce = 12;
                jumpSpeed = -30;
            }
        }

        // Obstacle speed changes by time
        private void gameTimerEvent(object sender, EventArgs e)
        {
            double elapsedSeconds = (DateTime.Now - startTime).TotalSeconds;
            int speedUp = (int)elapsedSeconds;
            obstacleSpeed = Math.Min(5 + speedUp, maxSpeed);

            foreach (var obs in obstacles)
                obs.Left -= (int)obstacleSpeed;
        }

        // Close web socket
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            wsServer?.Stop();
        }
    }

    // Web socket class
    public class PoseBehavior : WebSocketBehavior
    {
        public Action<string> OnPoseReceived;

        protected override void OnMessage(MessageEventArgs e)
        {
            OnPoseReceived?.Invoke(e.Data);
        }
    }
}
