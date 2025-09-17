export default class Dino {
  constructor(groundY) {
    this.x = 50;
    this.y = groundY - 40;
    this.width = 40;
    this.height = 40;
    this.groundY = groundY;
    this.dy = 0;
    this.gravity = 0.6;
    this.jumpForce = 12;
    this.grounded = true;
    this.step = 0; // 달리기 애니메이션
  }

  setGroundY(groundY) {
    this.groundY = groundY;
    if (this.grounded) {
      this.y = this.groundY - this.height;
    }
  }

  jump() {
    if (this.grounded) {
      this.dy = -this.jumpForce;
      this.grounded = false;
    }
  }

  update() {
    this.dy += this.gravity;
    this.y += this.dy;
    if (this.y + this.height >= this.groundY) {
      this.y = this.groundY - this.height;
      this.dy = 0;
      this.grounded = true;
    }
  }

  draw(ctx, frame) {
    ctx.fillStyle = "green";
    // 달리기 애니메이션
    if (this.grounded) {
      if (frame % 10 === 0) this.step = (this.step + 1) % 2;
      ctx.fillRect(
        this.x,
        this.y + this.step * 2,
        this.width,
        this.height - this.step * 2
      );
    } else {
      ctx.fillRect(this.x, this.y, this.width, this.height);
    }
  }
}
