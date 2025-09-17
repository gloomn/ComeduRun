export default class Obstacle {
  constructor(groundY, canvasWidth) {
    this.types = [
      { width: 20, height: 40, color: "red" },
      { width: 30, height: 50, color: "brown" },
      { width: 10, height: 30, color: "purple" },
    ];
    const t = this.types[Math.floor(Math.random() * this.types.length)];
    this.width = t.width;
    this.height = t.height;
    this.color = t.color;
    this.x = canvasWidth;
    this.y = groundY - this.height;
  }

  update(gameSpeed) {
    this.x -= gameSpeed;
  }

  draw(ctx) {
    ctx.fillStyle = this.color;
    ctx.fillRect(this.x, this.y, this.width, this.height);
  }
}
