import Dino from "./dino.js";
import Obstacle from "./obstacle.js";
import Background from "./background.js";
import { checkCollision, randomInt } from "./utils.js";

const canvas = document.getElementById("gameCanvas");
const ctx = canvas.getContext("2d");

const startBtn = document.getElementById("startBtn");

let groundY;
let gameSpeed = 6;
let score = 0;
let gameOver = false;
let frame = 0;
let obstacles = [];
let gameStarted = false;
let startTime;

const dino = new Dino(groundY);
const bg = new Background(canvas.width, groundY);

startBtn.addEventListener("click", () => {
  if (gameStarted) return;
  gameStarted = true;
  startBtn.style.display = "none";
  startTime = Date.now();       // ⬅️ 게임 시작 시간 저장
  gameSpeed = 5;                // ⬅️ 속도 5로 시작
  animate();
  obstacleInterval = setInterval(spawnObstacles, 1500);
});

// --- 게임 루프 ---
function animate() {
  ctx.clearRect(0, 0, canvas.width, canvas.height);

  // --- 시간에 따른 속도 증가 ---
  const elapsed = (Date.now() - startTime) / 100; // 초 단위
  const increments = Math.floor(elapsed / 10);     // 10초마다 1 증가
  gameSpeed = 5 + increments * 2;                  // 5부터 시작, 10초마다 +2

  bg.update(gameSpeed);
  bg.draw(ctx);

  dino.update();
  dino.draw(ctx, frame);

  obstacles.forEach((obs, index) => {
    obs.update(gameSpeed);
    obs.draw(ctx);

    if (checkCollision(dino, obs)) gameOver = true;

    if (obs.x + obs.width < 0) {
      obstacles.splice(index, 1);
      if (!gameOver) score++;
    }
  });

  // 점수 표시
  ctx.fillStyle = "black";
  ctx.font = "16px sans-serif";
  ctx.textAlign = "right";
  ctx.fillText("Score: " + score, canvas.width - 20, 30);

  // 게임 오버
  if (gameOver) {
    ctx.fillStyle = "red";
    ctx.font = "40px sans-serif";
    ctx.textAlign = "center";
    ctx.fillText("GAME OVER", canvas.width / 2, canvas.height / 2);
    ctx.font = "20px sans-serif";
    ctx.fillText("Score: " + score, canvas.width / 2, canvas.height / 2 + 30);
    return;
  }

  frame++;
  requestAnimationFrame(animate);
}


// --- 키 이벤트 ---
document.addEventListener("keydown", (e) => {
  if (e.code === "Space") dino.jump();
});

// --- 장애물 생성 ---
function spawnObstacles() {
  if (!gameStarted) return;
  const obs = new Obstacle(groundY, canvas.width);
  obstacles.push(obs);
}

let obstacleInterval;

// --- Start 버튼 이벤트 ---
startBtn.addEventListener("click", () => {
  if (gameStarted) return; // 중복 시작 방지
  gameStarted = true;
  startBtn.style.display = "none"; // 버튼 숨기기
  animate();
  obstacleInterval = setInterval(spawnObstacles, 1500);
});

function resizeCanvas() {
  canvas.width = window.innerWidth;
  canvas.height = Math.floor(window.innerWidth / 7);
  groundY = canvas.height;  // 땅 위치

  bg.setGroundY(groundY);        // Background 갱신
  dino.setGroundY(groundY);      // Dino 갱신
}


window.addEventListener("resize", resizeCanvas);
resizeCanvas();