const initialState = {
  videos: [],
  searchText: ""
};

const searchReducer = (state = initialState, { type, payload }) => {
  switch (type) {
    case "FETCH_VIDEO_SUCCESS": {
      const { videos } = payload;

      return {
        ...state,
        videos
      };
    }

    case "CHANGE_SEARCH_TEXT": {
      const { searchText } = payload;

      return {
        ...state,
        searchText
      };
    }

    default:
      return state;
  }
};

export default searchReducer;
