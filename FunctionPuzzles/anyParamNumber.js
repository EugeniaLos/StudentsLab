function multiplication(...args) {
  let result = 1;
  for (let number of args) {
    result *= number;
  }
  return result;
}
