function produce(F) {
  let cache = {};
  return function G(n) {
    if (n in cache) {
      return cache[n];
    } else {
      let result = F(n);
      cache[n] = result;
      return result;
    }
  };
}
