let arrayLib = {
  take(arr, n) {
    let output = [];
    for (let i = 0; i < n; i++) {
      output.push(arr[i]);
    }
    return output;
  },
  skip(arr, n) {
    let output = [];
    for (let i = n; i < arr.length; i++) {
      output.push(arr[i]);
    }
    return output;
  },
  map(arr, callback) {
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      output[i] = callback(arr[i]);
    }
    return output;
  },
  reduce(arr, callback, initialValue) {
    let secondArgument = initialValue;
    for (let i = 0; i < arr.length; i++) {
      secondArgument = callback(arr[i], secondArgument);
    }
    return secondArgument;
  },
  filter(arr, callback) {
    let output = [];
    for (let i = 0; i < arr.length; i++) {
      if (callback(arr[i])) {
        output.push(arr[i]);
      }
    }
    return output;
  },
  foreach(arr, callback) {
    for (let i = 0; i < arr.length; i++) {
      if (arr[i] != null) {
        callback(arr[i]);
      }
    }
  }
};
