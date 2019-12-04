export function addClass(...classNames) {
  for (let className of classNames) {
    this.classList.add(className);
  }
}
