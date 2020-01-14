const arrayLib = require("./arrayLib");

test("takes first two elements of array", () => {
  expect(arrayLib.take(["apple", "pear", "orange"], 2)).toEqual([
    "apple",
    "pear"
  ]);
});

test("returns array without two first elements", () => {
  expect(arrayLib.skip(["apple", "pear", "orange"], 2)).toEqual(["orange"]);
});

test("returns array with doubled values", () => {
  expect(arrayLib.map([1, 2, 3], a => a * 2)).toEqual([2, 4, 6]);
});

test("returns sum of all elements", () => {
  expect(arrayLib.reduce([1, 2, 3], (a, b) => a + b, 0)).toEqual(6);
});

test("returns elements with more than 4 characters", () => {
  expect(
    arrayLib.filter(["apple", "pear", "orange"], word => word.length > 4)
  ).toEqual(["apple", "orange"]);
});

test("returns first doubled elements less than 10", () => {
  expect(
    arrayLib
      .chain([13, 22, 4, 2, 5, 12])
      .map(a => a * 2)
      .filter(a => a < 10)
      .take(1)
      .value()
  ).toEqual([8]);
});
