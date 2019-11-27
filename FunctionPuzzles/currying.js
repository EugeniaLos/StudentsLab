function fun(x, y, z) {
  console.log(x, y, z);
}

function curry(f) {
  let i = [];
  return function wrap(x) {
    i.push(x);
    if (i.length == f.length) {
      return f(...i);
    } else {
      wrap(y);
    }
  };
}
