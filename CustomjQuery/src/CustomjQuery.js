export function addClass(classNames) {
  let classNamesArr = classNames.split(" ");
  for (let className of classNamesArr) {
    this.classList.add(className);
  }
}

export function removeClass(classNames) {
  let classNamesArr = classNames.split(" ");
  for (let className of classNamesArr) {
    this.classList.remove(className);
  }
}

export function append(...content) {
  for (let contentUnit of content) {
    this.textContent += contentUnit;
  }
}

// customJquery(".block").addClass("block-red");
