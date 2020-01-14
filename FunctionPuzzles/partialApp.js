function G(...args) {
  for (let arg of args) {
    alert(arg);
  }
}

function F(x, G) {
  return function H(...args) {
    return G(x, ...args);
  };
}
