import { fetchYoutubeVideo } from "../service/service";

export const fetchVideo = searchText => async dispatch => {
  const searchResult = await fetchYoutubeVideo(searchText);
  const searchResultJson = await searchResult.json();
  const videos = [];
  if (searchResultJson.items) {
    const items = searchResultJson.items;
    items.forEach(function(item) {
      const video = {
        id: item.id.videoId,
        title: item.snippet.title,
        description: item.snippet.description,
        thumbnails: item.snippet.thumbnails.high
      };
      videos.push(video);
    });
  }

  dispatch({ type: "FETCH_VIDEO_SUCCESS", payload: { videos } });
};

export const changeSearchText = searchText => dispatch => {
  dispatch({ type: "CHANGE_SEARCH_TEXT", payload: { searchText } });
};
