const initialState = {
  video: null,
  searchText: "fff"
};

export const searchReducer = (state = initialState, { type, payload }) => {
  switch (type) {
    case "FETCH_VIDEO_SUCCESS": {
      const { video } = payload;

      return {
        ...state,
        video
      };
    }

    default:
      return state;
  }
};
