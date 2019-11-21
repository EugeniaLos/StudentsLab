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
  reduce(arr, callback, initialValue) {},
  filter(array, callback) {},
  foreach(array, callback) {}
};
