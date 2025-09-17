export default class Background {
  constructor(canvas, groundY) {
    this.canvas = canvas; // 캔버스 자체를 저장
    this.x = 0;
    this.y = groundY;
  }

  setGroundY(groundY) {
    this.y = groundY; // 외부에서 groundY 갱신 가능
  }

  update(gameSpeed) {
    this.x -= gameSpeed / 2;
    if (this.x <= -this.canvas.width) this.x = 0;
  }

  draw(ctx) {
    ctx.fillStyle = "#ddd";
    ctx.fillRect(this.x, this.y, this.canvas.width, 5);
    ctx.fillRect(this.x + this.canvas.width, this.y, this.canvas.width, 5);
  }
}
