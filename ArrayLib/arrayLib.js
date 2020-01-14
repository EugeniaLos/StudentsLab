let arrayLib = {
  func: [],
  innerArr: [],
  memo: {},
  take(arr, n) {
    if (n === undefined) {
      n = arr;
      this.func.push({
        function: this.take,
        parameters: [n]
      });
      return this;
    }
    let output = [];
    for (let i = 0; i < n; i++) {
      output.push(arr[i]);
    }
    this.innerArr = output;
    return output;
  },
  skip(arr, n) {
    if (n === undefined) {
      n = arr;
      this.func.push({
        function: this.skip,
        parameters: [n]
      });
      return this;
    }
    let output = [];
    for (let i = n; i < arr.length; i++) {
      output.push(arr[i]);
    }
    this.innerArr = output;
    return output;
  },
  map(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.map,
        parameters: [callback]
      });
      return this;
    }
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      output[i] = callback(arr[i]);
    }
    this.innerArr = output;
    return output;
  },
  reduce(arr, callback, initialValue) {
    if (initialValue === undefined) {
      initialValue = callback;
      callback = arr;
      this.func.push({
        function: this.reduce,
        parameters: [callback, initialValue]
      });
      return this;
    }
    let secondArgument = initialValue;
    for (let i = 0; i < arr.length; i++) {
      secondArgument = callback(arr[i], secondArgument);
    }
    this.innerArr = secondArgument;
    return secondArgument;
  },
  filter(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.filter,
        parameters: [callback]
      });
      return this;
    }
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      if (callback(arr[i])) {
        output.push(arr[i]);
      }
    }
    this.innerArr = output;
    return output;
  },
  foreach(arr, callback) {
    if (callback === undefined) {
      callback = arr;
      this.func.push({
        function: this.foreach,
        parameters: [callback]
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
    this.innerArr = arr;
    return this;
  },
  value() {
    for (let obj of this.func) {
      obj.function.call(this, this.innerArr, ...obj.parameters);
    }
    return this.innerArr;
  },

  sum(a, b) {
    let value;
    if (a + " " + b in this.memo) {
      value = this.memo[a + " " + b];
    } else {
      value = a + b;
      this.memo[a + " " + b] = value;
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
