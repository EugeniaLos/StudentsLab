import React from "react";
import ReactDOM from "react-dom";
import App from "./components/App";
import "./styles/App.css";
import "./styles/search.scss";
import { fetchYoutubeVideo } from "./service/service";

ReactDOM.render(<App />, document.getElementById("root"));

async function as() {
  const searchResult = await fetchYoutubeVideo("javascript");
  let searchResultJson = await searchResult.json();
  console.log("fetch json", searchResultJson);
}

as();
