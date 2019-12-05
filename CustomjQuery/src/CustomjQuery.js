export class Elements {
  constructor(element) {
    this.elementsStorage = document.querySelectorAll(element);
  }

  addClass(classNames) {
    let classNamesArr = classNames.split(" ");
    for (let element of this.elementsStorage) {
      for (let className of classNamesArr) {
        element.classList.add(className);
      }
    }
  }

  removeClass(classNames) {
    let classNamesArr = classNames.split(" ");
    for (let element of this.elementsStorage) {
      for (let className of classNamesArr) {
        element.classList.remove(className);
      }
    }
  }

  append(...content) {
    for (let element of this.elementsStorage) {
      for (let contentUnit of content) {
        element.insertAdjacentHTML("beforeend", contentUnit);
      }
    }
  }

  text() {
    let result;
    for (let element of this.elementsStorage) {
      result += element.textContent;
    }
    return result;
  }

  attr(attributeName) {
    for (let element of this.elementsStorage) {
      if (element.getAttribute(attributeName) != null) {
        return element.getAttribute(attributeName);
      }
    }
  }

  children(selector) {
    let result = [];
    if (selector === undefined) {
      for (let element of this.elementsStorage) {
        let child = element.firstElementChild;

        if (child != undefined) {
          result.push(child);
          while ((child = child.nextElementSibling)) {
            result.push(child);
          }
        }
      }
    } else {
      for (let element of this.elementsStorage) {
        let child = element.querySelector(selector);

        if (child != undefined) {
          result.push(child);
          while ((child = child.nextElementSibling)) {
            if (child.matches(selector)) {
              result.push(child);
            }
          }
        }
      }
    }
    return result;
  }

  empty() {
    for (let element of this.elementsStorage) {
      let children = element.childNodes;
      console.log(children);
      for (let child of children) {
        console.log("child: ", child);
        element.removeChild(child);
      }
    }
  }

  css(propertyName) {
    for (let element of this.elementsStorage) {
      return getComputedStyle(element)[propertyName];
    }
  }

  click(func) {
    for (let element of this.elementsStorage) {
      element.onclick = func;
    }
  }
}

export function remove(selector) {
  let elements = document.querySelectorAll(selector);
  for (let elem of elements) {
    elem.parentNode.removeChild(elem);
  }
}

// export function $(element) {
//   return document.querySelectorAll(element);
// }

// export function addClass(classNames) {
//   for (let element of this) {
//     let classNamesArr = classNames.split(" ");
//     for (let className of classNamesArr) {
//       element.classList.add(className);
//     }
//   }
// }

// export function removeClass(classNames) {
//   let classNamesArr = classNames.split(" ");
//   for (let className of classNamesArr) {
//     this.classList.remove(className);
//   }
// }

// export function append(...content) {
//   for (let contentUnit of content) {
//     this.textContent += contentUnit;
//   }
// }

// customJquery(".block").addClass("block-red");
