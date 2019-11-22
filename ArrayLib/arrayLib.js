let arrayLib = {
  arr: ["abba", "aaa", "aaaaaa"],
  take(arr, n) {
    if (n == undefined) {
      n = arr;
    } else {
      arrayLib = arr;
    }
    let output = [];
    for (let i = 0; i < n; i++) {
      output.push(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    return this;
  },
  skip(arr, n) {
    if (n == undefined) {
      n = arr;
    } else {
      arrayLib = arr;
    }
    let output = [];
    for (let i = n; i < arrayLib.arr.length; i++) {
      output.push(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    return this;
  },
  map(arr, callback) {
    if (callback == undefined) {
      callback = arr;
    } else {
      arrayLib = arr;
    }
    let output = [];
    for (let i = 0; i < arrayLib.arr.length; i++) {
      output[i] = callback(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    return this;
  },
  reduce(arr, callback, initialValue) {
    if (initialValue == undefined) {
      initialValue = callback;
      callback = arr;
    } else {
      arrayLib = arr;
    }
    let secondArgument = initialValue;
    for (let i = 0; i < arrayLib.arr.length; i++) {
      secondArgument = callback(arrayLib.arr[i], secondArgument);
    }
    arrayLib.arr = secondArgument;
    return this;
  },
  filter(arr, callback) {
    if (callback == undefined) {
      callback = arr;
    } else {
      arrayLib = arr;
    }
    let output = [];
    for (let i = 0; i < arrayLib.arr.length; i++) {
      if (callback(arrayLib.arr[i])) {
        output.push(arrayLib.arr[i]);
      }
    }
    arrayLib.arr = output;
    return this;
  },
  foreach(arr, callback) {
    if (callback == undefined) {
      callback = arr;
    } else {
      arrayLib = arr;
    }
    for (let i = 0; i < arrayLib.arr.length; i++) {
      if (arrayLib.arr[i] != null) {
        callback(arrayLib.arr[i]);
      }
    }
    return this;
  },
  chain(arr) {
    arrayLib.arr = arr;
    return this;
  },
  value() {
    return arrayLib.arr;
  }
};
