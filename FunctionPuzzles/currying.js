function fun(x, y, z) {
  console.log(x, y, z);
}
function curry(f) {
  let i = [];
  function wrap() {
    return function addParam(x) {
      i.push(x);
      if (i.length === f.length) {
        return f(...i);
      }
      return wrap();
    };
  }
  return wrap();
}
