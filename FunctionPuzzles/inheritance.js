class Shape {
  constructor(name) {
    this.name = name;
  }
  perimeter() {}
  area() {}
}

class Square extends Shape {
  constructor(name, sideLength) {
    super(name);
    this.sideLength = sideLength;
  }
  perimeter() {
    return this.sideLength * 4;
  }
  area() {
    return Math.pow(this.sideLength, 2);
  }
}

class Rectangle extends Shape {
  constructor(name, width, height) {
    super(name);
    this.width = width;
    this.height = height;
  }
  perimeter() {
    return this.width * 2 + this.height * 2;
  }
  area() {
    return this.width * this.height;
  }
}

class ShapeStore {
  constructor() {
    this.container = [];
  }
  add(shape) {
    this.container.push(shape);
  }
  RectanglePerimeter() {
    let sum = 0;
    for (let shape of this.container) {
      if (shape instanceof Rectangle) {
        sum += shape.perimeter();
      }
    }
    return sum;
  }
  SquareArea() {
    let sum = 0;
    for (let shape of this.container) {
      if (shape instanceof Square) {
        sum += shape.area();
      }
    }
    return sum;
  }
}

let q = new Square("ss", 2);

let w = new Square("sss", 1);

let a = new Rectangle("aaa", 5);

let c = new ShapeStore();

c.add(q);

c.add(w);

c.add(q);
