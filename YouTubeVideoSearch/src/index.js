import React from "react";
import ReactDOM from "react-dom";

import { Provider } from "react-redux";
import { createStore, applyMiddleware } from "redux";

import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";

import searchReducer from "./reducers/search";

import App from "./components/App";

// import { fetchYoutubeVideo } from "./service/service";

import "./styles/App.scss";
import "./styles/search-form.scss";
import "./styles/video-info.scss";
import "./styles/videos-container.scss";

const store = createStore(
  searchReducer,
  composeWithDevTools(applyMiddleware(thunk))
);

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);

// async function as() {
//   const searchResult = await fetchYoutubeVideo("лсп");
//   let searchResultJson = await searchResult.json();
//   console.log("fetch json", searchResultJson);
// }

// as();
