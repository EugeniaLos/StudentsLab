import "./index.css";
import { addClass, removeClass, append } from "./CustomjQuery";

let elem = document.body.firstElementChild;
addClass.call(elem, "blue line");

append.call(elem, " I am blue", " and underlined");
