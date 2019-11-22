let arrayLib = {
  arr: ["abba", "aaa", "aaaaaa"],
  take(arr, n) {
    let isChained = false;
    if (n == undefined) {
      n = arr;
      isChained = true;
    } else {
      arrayLib.arr = arr;
    }
    let output = [];
    for (let i = 0; i < n; i++) {
      output.push(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  skip(arr, n) {
    let isChained = false;
    if (n == undefined) {
      n = arr;
      isChained = true;
    } else {
      arrayLib.arr = arr;
    }
    let output = [];
    for (let i = n; i < arrayLib.arr.length; i++) {
      output.push(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  map(arr, callback) {
    let isChained = false;
    if (callback == undefined) {
      callback = arr;
      isChained = true;
    } else {
      arrayLib.arr = arr;
    }
    let output = [];
    for (let i = 0; i < arrayLib.arr.length; i++) {
      output[i] = callback(arrayLib.arr[i]);
    }
    arrayLib.arr = output;
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  reduce(arr, callback, initialValue) {
    let isChained = false;
    if (initialValue == undefined) {
      initialValue = callback;
      callback = arr;
      isChained = true;
    } else {
      arrayLib.arr = arr;
    }
    let secondArgument = initialValue;
    for (let i = 0; i < arrayLib.arr.length; i++) {
      secondArgument = callback(arrayLib.arr[i], secondArgument);
    }
    arrayLib.arr = secondArgument;
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  filter(arr, callback) {
    let isChained = false;
    if (callback == undefined) {
      callback = arr;
      isChained = true;
    } else {
      arrayLib.arr = arr;
    }
    let output = [];
    for (let i = 0; i < arrayLib.arr.length; i++) {
      if (callback(arrayLib.arr[i])) {
        output.push(arrayLib.arr[i]);
      }
    }
    arrayLib.arr = output;
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  foreach(arr, callback) {
    let isChained = false;
    if (callback == undefined) {
      isChained = true;
      callback = arr;
    } else {
      arrayLib.arr = arr;
    }
    for (let i = 0; i < arrayLib.arr.length; i++) {
      if (arrayLib.arr[i] != null) {
        callback(arrayLib.arr[i]);
      }
    }
    if (isChained) {
      return this;
    } else {
      return arrayLib.arr;
    }
  },
  chain(arr) {
    arrayLib.arr = arr;
    return this;
  },
  value() {
    return arrayLib.arr;
  }
};

module.exports = arrayLib;
