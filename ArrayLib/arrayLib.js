let arrayLib = {
  func: [],
  arr: [],
  memo: {},
  take(arr, n) {
    if (n === undefined) {
      n = arr;
      this.func.push({ function: this.take, parameters: [this.arr, n] });
      return this;
    }
    let output = [];
    for (let i = 0; i < n; i++) {
      output.push(arr[i]);
    }
    arrayLib.arr = output;
    return output;
  },
  skip(arr, n) {
    if (n === undefined) {
      n = arr;
      this.func.push({ function: this.skip, parameters: [this.arr, n] });
      return this;
    }
    let output = [];
    for (let i = n; i < arr.length; i++) {
      output.push(arr[i]);
    }
    this.arr = output;
    return output;
  },
  map(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.map,
        parameters: [this.arr, callback]
      });
      return this;
    }
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      output[i] = callback(arr[i]);
    }
    this.arr = output;
    return output;
  },
  reduce(arr, callback, initialValue) {
    if (initialValue === undefined) {
      initialValue = callback;
      callback = arr;
      this.func.push({
        function: this.reduce,
        parameters: [this.arr, callback, initialValue]
      });
      return this;
    }
    let secondArgument = initialValue;
    for (let i = 0; i < arr.length; i++) {
      secondArgument = callback(arr[i], secondArgument);
    }
    this.arr = secondArgument;
    return secondArgument;
  },
  filter(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.filter,
        parameters: [this.arr, callback]
      });
      return this;
    }
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      if (callback(arr[i])) {
        output.push(arr[i]);
      }
    }
    this.arr = output;
    return output;
  },
  foreach(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.foreach,
        parameters: [this.arr, callback]
      });
      return this;
    }
    for (let i = 0; i < arr.length; i++) {
      if (arr[i] != null) {
        callback(arr[i]);
      }
    }
  },
  chain(arr) {
    this.arr = arr;
    return this;
  },
  value() {
    for (let obj of arrayLib.func) {
      obj.function.apply(this, obj.parameters);
    }
    return arrayLib.arr;
  },

  sum(a, b) {
    let value;
    if (a + " " + b in arrayLib.memo) {
      value = arrayLib.memo[a + " " + b];
    } else {
      value = a + b;
      arrayLib.memo[a + " " + b] = value;
    }
    return value;
  }
};

arrayLib
  .chain([13, 22, 4, 2, 5, 12])
  .map(a => a * 2)
  .filter(a => a < 10)
  .take(1)
  .value();

module.exports = arrayLib;
