import { fetchYoutubeVideo } from "../service/service";

export const fetchVideo = searchText => async dispatch => {
  const searchResult = await fetchYoutubeVideo(searchText);
  let searchResultJson = await searchResult.json();

  dispatch({ type: "FETCH_VIDEO_SUCCESS", payload: { video } });
};
