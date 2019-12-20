import { fetchYoutubeVideo } from "../service/service";

export const fetchVideo = searchText => async dispatch => {
  const searchResult = await fetchYoutubeVideo(searchText);
  let searchResultJson = await searchResult.json();
  let videos = [];
  if (searchResultJson.items) {
    let items = searchResultJson.items;
    items.forEach(function(item) {
      let video = {};
      video.id = item.id.videoId;
      video.title = item.snippet.title;
      video.description = item.snippet.description;
      video.thumbnails = item.snippet.thumbnails.high;
      videos.push(video);
    });
  }

  dispatch({ type: "FETCH_VIDEO_SUCCESS", payload: { videos } });
};

export const changeSearchText = searchText => dispatch => {
  dispatch({ type: "CHANGE_SEARCH_TEXT", payload: { searchText } });
};
